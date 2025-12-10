const { contextBridge, ipcRenderer } = require('electron');

contextBridge.exposeInMainWorld('contentApi', {
  getContentRoot: () => ipcRenderer.invoke('content:getRoot'),
  getContentTree: () => ipcRenderer.invoke('content:getTree'),
  readFile: (relativePath) =>
    ipcRenderer.invoke('content:readFile', relativePath),
  saveFile: (relativePath, content) =>
    ipcRenderer.invoke('content:saveFile', { relativePath, content }),
  createFile: (directoryPath, fileName, template) =>
    ipcRenderer.invoke('content:createFile', {
      directoryPath,
      fileName,
      template
    })
});
