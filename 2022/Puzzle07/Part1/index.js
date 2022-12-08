const readFileSyncIntoArray = require('../../Core/ReadFileSyncIntoArray');
const Directory = require('../Common/Directory');
const File = require('../Common/File');
const commands = readFileSyncIntoArray('./data.txt');

const dirMap = new Map([['/', new Directory('/', [])]]);
let currentDir = dirMap.get('/');
let listMode = false;

const processChangeDirectory = function(dirName) {
  const dirPath = getDirectoryPath(dirName);

  if (dirName === '..' && currentDir.name !== '/') {
    currentDir = currentDir.parent;
  } else if (!dirMap.has(dirPath)) {
    dirMap.set(dirPath, new Directory(dirPath, currentDir, []));
    currentDir = dirMap.get(dirPath);
  } else if (dirMap.has(dirPath)) {
    currentDir = dirMap.get(dirPath);
  }
};

const getDirectoryPath = function(dirName) {
  return dirName === '/' ? '/' : `${currentDir.name}/${dirName}`;
};

const processDirectoryDiscovery = function(dirName) {
  const dirPath = getDirectoryPath(dirName);
  if (!dirMap.has(dirPath)) {
    const dir = new Directory(dirPath, currentDir, []);
    dirMap.set(dirPath, dir);
    currentDir.addChild(dir);
  }
};

const processFileDiscovery = function(fileName, fileSize) {
  const file = new File(fileName, currentDir, fileSize);
  currentDir.addChild(file);
};

for (const cmd of commands) {
  if (cmd.startsWith('$ cd')) {
    processChangeDirectory(cmd.split(' ')[2]);
    listMode = false;
  } else if (cmd.startsWith('$ ls')) {
    listMode = true;
  } else if (listMode && cmd.startsWith('dir')) {
    processDirectoryDiscovery(cmd.split(' ')[1]);
  } else if (listMode) {
    processFileDiscovery(cmd.split(' ')[1], Number(cmd.split(' ')[0]));
  }
}

const result =
  [...dirMap.keys()]
      .map((k) => dirMap.get(k))
      .map((d) => d.getTotalFileSize())
      .filter((s) => s <= 100000)
      .reduce((acc, curr) => acc += curr);

console.log(result);
