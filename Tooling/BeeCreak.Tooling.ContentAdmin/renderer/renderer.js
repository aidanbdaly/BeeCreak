const treeContainer = document.getElementById('treeContainer');
const rootPathLabel = document.getElementById('rootPath');
const refreshBtn = document.getElementById('refreshBtn');
const newFileBtn = document.getElementById('newFileBtn');
const editorTitle = document.getElementById('editorTitle');
const editorPath = document.getElementById('editorPath');
const editorStatus = document.getElementById('editorStatus');
const editor = document.getElementById('editor');
const saveBtn = document.getElementById('saveBtn');

const state = {
  tree: null,
  expanded: new Set(['']),
  selectedPath: null,
  activeDirectory: '',
  currentFile: null,
  lastContent: '',
  isEditable: false,
  isDirty: false
};

const TEMPLATES = {
  '.json': '{\n  \n}\n',
  '.as': '{\n  "id": "",\n  "texture": "",\n  "data": []\n}\n',
  '.asc': '{\n  "id": "",\n  "animations": []\n}\n',
  '.bts': '{\n  "id": "",\n  "font": "",\n  "text": {}\n}\n',
  '.spritesheet': '{\n  "id": "",\n  "texture": "",\n  "data": {}\n}\n',
  '.spritefont': '<?xml version="1.0"?>\n<XnaContent>\n  <Asset Type="Graphics:FontDescription">\n    <FontName></FontName>\n    <Size>12</Size>\n  </Asset>\n</XnaContent>\n',
  '.erec': '{\n  "id": "",\n  "components": []\n}\n',
  '.eref': '{\n  "id": "",\n  "entity": "",\n  "transform": {}\n}\n',
  '.crec': '{\n  "id": "",\n  "cells": []\n}\n',
  '.cref': '{\n  "id": "",\n  "cell": ""\n}\n',
  '.tref': '{\n  "id": "",\n  "tiles": []\n}\n',
  '.game': '{\n  "id": "",\n  "scenes": []\n}\n',
  '.bbs': '{\n  "id": "",\n  "data": {}\n}\n',
  '.mgcb': '# new MGCB file\n'
};

const confirmDiscardChanges = (nextPath) => {
  if (!state.isDirty) {
    return true;
  }

  if (state.currentFile && state.currentFile.path === nextPath) {
    return true;
  }

  return window.confirm(
    'You have unsaved changes. Continue and discard them?'
  );
};

const expandDirectoryChain = (dirPath) => {
  if (!dirPath) {
    state.expanded.add('');
    return;
  }

  const parts = dirPath.split('/');
  let cursor = '';
  for (const part of parts) {
    cursor = cursor ? `${cursor}/${part}` : part;
    state.expanded.add(cursor);
  }
};

const formatError = (err) =>
  err?.message || err?.toString?.() || 'Something went wrong';

const getParentPath = (targetPath) => {
  if (!targetPath) {
    return '';
  }

  const parts = targetPath.split('/');
  parts.pop();
  return parts.join('/');
};

const findNode = (node, targetPath) => {
  if (!node) {
    return null;
  }

  if (node.path === targetPath) {
    return node;
  }

  if (node.children) {
    for (const child of node.children) {
      const match = findNode(child, targetPath);
      if (match) {
        return match;
      }
    }
  }

  return null;
};

const setStatus = (message, kind = 'info') => {
  editorStatus.textContent = message;
  editorStatus.classList.toggle('status-error', kind === 'error');
  editorStatus.classList.toggle('status-warning', kind === 'warning');
};

const setDirtyState = (isDirty) => {
  state.isDirty = isDirty;
  const shouldEnable = Boolean(state.currentFile && state.isEditable && isDirty);
  saveBtn.disabled = !shouldEnable;
  editorTitle.classList.toggle('dirty-indicator', shouldEnable);
};

const handleEditorInput = () => {
  if (!state.isEditable) {
    return;
  }
  setDirtyState(editor.value !== state.lastContent);
};

const toggleDirectory = (path) => {
  const key = path || '';
  if (state.expanded.has(key)) {
    state.expanded.delete(key);
  } else {
    state.expanded.add(key);
  }
  renderTree();
};

const createTreeRow = (node, depth = 0) => {
  const row = document.createElement('div');
  row.classList.add('tree-row');
  if (node.type === 'file' && !node.editable) {
    row.classList.add('non-editable');
  }
  if (state.selectedPath === node.path) {
    row.classList.add('selected');
  }

  row.style.paddingLeft = `${depth * 14 + 8}px`;

  const chevron = document.createElement('span');
  chevron.className = 'chevron';

  if (node.type === 'directory') {
    const expanded = state.expanded.has(node.path || '');
    chevron.textContent = expanded ? 'v' : '>';
  }

  row.appendChild(chevron);

  const icon = document.createElement('span');
  icon.className = 'file-icon';
  icon.textContent = node.type === 'directory' ? 'd' : 'f';
  row.appendChild(icon);

  const label = document.createElement('span');
  label.textContent = node.name || 'Content';
  row.appendChild(label);

  row.addEventListener('click', async (event) => {
    event.stopPropagation();
    if (node.type === 'directory') {
      state.selectedPath = node.path || '';
      state.activeDirectory = node.path || '';
      toggleDirectory(node.path || '');
    } else {
      if (!confirmDiscardChanges(node.path)) {
        renderTree();
        return;
      }

      state.selectedPath = node.path;
      state.activeDirectory = getParentPath(node.path);
      await openFile(node);
    }
  });

  return row;
};

const renderTreeNode = (node, depth = 0) => {
  const wrapper = document.createElement('div');
  wrapper.appendChild(createTreeRow(node, depth));

  if (
    node.type === 'directory' &&
    node.children &&
    state.expanded.has(node.path || '')
  ) {
    for (const child of node.children) {
      wrapper.appendChild(renderTreeNode(child, depth + 1));
    }
  }

  return wrapper;
};

const renderTree = () => {
  treeContainer.textContent = '';

  if (!state.tree) {
    const message = document.createElement('p');
    message.className = 'panel-placeholder';
    message.textContent = 'Content tree is unavailable.';
    treeContainer.appendChild(message);
    return;
  }

  treeContainer.appendChild(renderTreeNode(state.tree));
};

const loadTree = async () => {
  treeContainer.innerHTML =
    '<p class="panel-placeholder">Loading content tree...</p>';
  try {
    const tree = await window.contentApi.getContentTree();
    state.tree = tree;
    const previouslySelected = state.selectedPath;
    renderTree();

    if (previouslySelected) {
      const node = findNode(state.tree, previouslySelected);
      if (node && node.type === 'file') {
        state.selectedPath = node.path;
      }
    }
  } catch (err) {
    treeContainer.innerHTML = '';
    const message = document.createElement('p');
    message.className = 'panel-placeholder';
    message.textContent = `Failed to load tree: ${formatError(err)}`;
    treeContainer.appendChild(message);
  }
};

const loadRootPath = async () => {
  try {
    const root = await window.contentApi.getContentRoot();
    rootPathLabel.textContent = root;
  } catch (err) {
    rootPathLabel.textContent = 'Unavailable';
  }
};

const openFile = async (node) => {
  editorTitle.textContent = node.name;
  editorPath.textContent = node.path;
  editor.value = '';
  editor.disabled = true;
  setDirtyState(false);

  if (!node.editable) {
    state.currentFile = node;
    state.isEditable = false;
    setStatus('This file type cannot be edited via Content Admin.', 'warning');
    return;
  }

  try {
    const contents = await window.contentApi.readFile(node.path);
    state.currentFile = node;
    state.isEditable = true;
    state.lastContent = contents;
    editor.value = contents;
    editor.disabled = false;
    editor.focus();
    setStatus('File loaded. Apply your changes and save.');
    setDirtyState(false);
  } catch (err) {
    state.currentFile = null;
    state.isEditable = false;
    editor.disabled = true;
    setStatus(`Failed to load file: ${formatError(err)}`, 'error');
  }
};

const saveCurrentFile = async () => {
  if (!state.currentFile || !state.isEditable || !state.isDirty) {
    return;
  }

  try {
    await window.contentApi.saveFile(state.currentFile.path, editor.value);
    state.lastContent = editor.value;
    setDirtyState(false);
    setStatus('File saved successfully.');
  } catch (err) {
    setStatus(`Save failed: ${formatError(err)}`, 'error');
  }
};

const getTemplateForFileName = (fileName) => {
  const ext = fileName.includes('.') ? fileName.slice(fileName.lastIndexOf('.')).toLowerCase() : '';
  return TEMPLATES[ext] || '';
};

const createNewFile = async () => {
  const baseDirectory = state.activeDirectory || '';
  const promptLabel = baseDirectory
    ? `Enter a name for the new file inside "${baseDirectory}":`
    : 'Enter a name for the new file inside the content root:';
  const fileName = window.prompt(promptLabel);

  if (!fileName) {
    return;
  }

  const trimmedName = fileName.trim();
  if (!trimmedName) {
    setStatus('File name cannot be empty.', 'warning');
    return;
  }

  try {
    expandDirectoryChain(baseDirectory);
    const template = getTemplateForFileName(trimmedName);
    const info = await window.contentApi.createFile(
      baseDirectory,
      trimmedName,
      template
    );
    setStatus(`Created ${trimmedName}.`);
    await loadTree();
    const newPath =
      info?.path ||
      (baseDirectory ? `${baseDirectory}/${trimmedName}` : trimmedName);
    const newNode = findNode(state.tree, newPath);
    if (newNode) {
      state.selectedPath = newNode.path;
      state.activeDirectory = getParentPath(newNode.path);
      if (newNode.type === 'file') {
        await openFile(newNode);
      } else {
        renderTree();
      }
    }
  } catch (err) {
    setStatus(`Unable to create file: ${formatError(err)}`, 'error');
  }
};

const init = async () => {
  await loadRootPath();
  await loadTree();
};

refreshBtn.addEventListener('click', loadTree);
newFileBtn.addEventListener('click', createNewFile);
saveBtn.addEventListener('click', saveCurrentFile);
editor.addEventListener('input', handleEditorInput);

window.addEventListener('beforeunload', (event) => {
  if (state.isDirty) {
    event.preventDefault();
    event.returnValue = '';
  }
});

window.addEventListener('DOMContentLoaded', init);
