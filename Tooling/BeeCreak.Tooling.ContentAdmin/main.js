const { app, BrowserWindow, ipcMain } = require('electron');
const path = require('path');
const fs = require('fs');

const fsp = fs.promises;

const CONTENT_ROOT = path.resolve(
  __dirname,
  '..',
  '..',
  'Game',
  'BeeCreak.Game',
  'Content'
);

const SKIPPED_DIRECTORIES = new Set(['bin', 'obj']);
const TEXT_EXTENSIONS = new Set([
  '.json',
  '.as',
  '.asc',
  '.bbs',
  '.bts',
  '.game',
  '.mgcb',
  '.erec',
  '.eref',
  '.crec',
  '.cref',
  '.spritesheet',
  '.spritefont',
  '.tref'
]);

const ensureWithinContent = (relativePath = '') => {
  const normalized = relativePath
    ? relativePath.split(/[\\/]+/).join(path.sep)
    : '';
  const absolutePath = path.resolve(CONTENT_ROOT, normalized);
  const allowedPrefix = CONTENT_ROOT.endsWith(path.sep)
    ? CONTENT_ROOT
    : `${CONTENT_ROOT}${path.sep}`;

  if (
    absolutePath !== CONTENT_ROOT &&
    !absolutePath.startsWith(allowedPrefix)
  ) {
    throw new Error('Path is outside of the BeeCreak content directory');
  }

  return absolutePath;
};

const toRendererPath = (absolutePath) => {
  const relative = path.relative(CONTENT_ROOT, absolutePath);
  return relative.split(path.sep).join('/');
};

const isEditableFile = (fullPath) => {
  const ext = path.extname(fullPath).toLowerCase();
  return TEXT_EXTENSIONS.has(ext);
};

const readDirectoryNode = async (
  absolutePath,
  relativePath = '',
  displayName = null
) => {
  const entries = await fsp.readdir(absolutePath, { withFileTypes: true });
  const nodes = [];

  for (const entry of entries) {
    if (entry.name.startsWith('.')) {
      continue;
    }

    if (entry.isDirectory()) {
      if (SKIPPED_DIRECTORIES.has(entry.name)) {
        continue;
      }

      const childAbsolute = path.join(absolutePath, entry.name);
      const childRelative = relativePath
        ? `${relativePath}/${entry.name}`
        : entry.name;
      nodes.push(
        await readDirectoryNode(childAbsolute, childRelative, entry.name)
      );
    } else if (entry.isFile()) {
      const fileAbsolute = path.join(absolutePath, entry.name);
      const fileRelative = relativePath
        ? `${relativePath}/${entry.name}`
        : entry.name;
      nodes.push({
        type: 'file',
        name: entry.name,
        path: fileRelative,
        editable: isEditableFile(fileAbsolute)
      });
    }
  }

  nodes.sort((a, b) => {
    if (a.type !== b.type) {
      return a.type === 'directory' ? -1 : 1;
    }
    return a.name.localeCompare(b.name);
  });

  return {
    type: 'directory',
    name: displayName || relativePath || 'Content',
    path: relativePath,
    children: nodes
  };
};

const createWindow = () => {
  const win = new BrowserWindow({
    width: 1200,
    height: 800,
    webPreferences: {
      preload: path.join(__dirname, 'preload.js'),
      contextIsolation: true,
      nodeIntegration: false
    },
    title: 'BeeCreak Content Admin'
  });

  win.loadFile(path.join(__dirname, 'renderer', 'index.html'));
};

app.whenReady().then(() => {
  createWindow();

  app.on('activate', () => {
    if (BrowserWindow.getAllWindows().length === 0) {
      createWindow();
    }
  });
});

app.on('window-all-closed', () => {
  if (process.platform !== 'darwin') {
    app.quit();
  }
});

ipcMain.handle('content:getRoot', async () => CONTENT_ROOT);

ipcMain.handle('content:getTree', async () => {
  return readDirectoryNode(CONTENT_ROOT, '', 'Content');
});

ipcMain.handle('content:readFile', async (_event, relativePath) => {
  const fullPath = ensureWithinContent(relativePath);
  const stats = await fsp.stat(fullPath);
  if (!stats.isFile()) {
    throw new Error('Requested path is not a file');
  }

  return fsp.readFile(fullPath, 'utf8');
});

ipcMain.handle(
  'content:saveFile',
  async (_event, { relativePath, content }) => {
    if (typeof content !== 'string') {
      throw new Error('Invalid file content');
    }

    const fullPath = ensureWithinContent(relativePath);
    await fsp.writeFile(fullPath, content, 'utf8');
    return { ok: true };
  }
);

ipcMain.handle(
  'content:createFile',
  async (_event, { directoryPath = '', fileName, template = '' }) => {
    if (!fileName || /[\\/]/.test(fileName)) {
      throw new Error('File name must not contain path separators');
    }

    const baseDirectory = ensureWithinContent(directoryPath || '');
    const newFilePath = path.join(baseDirectory, fileName);
    ensureWithinContent(
      path.relative(CONTENT_ROOT, newFilePath)
    );

    try {
      await fsp.access(newFilePath, fs.constants.F_OK);
      throw new Error('File already exists');
    } catch (err) {
      if (err.code !== 'ENOENT') {
        throw err;
      }
    }

    await fsp.mkdir(path.dirname(newFilePath), { recursive: true });
    await fsp.writeFile(newFilePath, template, 'utf8');

    const relative = toRendererPath(newFilePath);
    return { path: relative };
  }
);
