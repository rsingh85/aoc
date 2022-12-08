/* eslint-disable max-len */
const readFileSyncIntoArray = require('../../Core/ReadFileSyncIntoArray');
const trees = readFileSyncIntoArray('./data.txt');
const grid = trees.map((t) => [...t]);
const treeVisibiityMap = new Map();

const computeVisibility = function(r, c) {
  const treeHeight = grid[r][c];

  let visibleFromTop = 0;
  for (let row = r - 1; row >= 0; row--) {
    visibleFromTop++;
    if (Number(grid[row][c]) >= treeHeight) {
      break;
    }
  }

  let visibleFromRight = 0;
  for (let col = c + 1; col < grid[r, c].length; col++) {
    visibleFromRight++;
    if (Number(grid[r][col]) >= treeHeight) {
      break;
    }
  }

  let visibleFromBottom = 0;
  for (let row = r + 1; row < grid[r].length; row++) {
    visibleFromBottom++;
    if (Number(grid[row][c]) >= treeHeight) {
      break;
    }
  }

  let visibleFromLeft = 0;
  for (let col = c - 1; col >= 0; col--) {
    visibleFromLeft++;
    if (Number(grid[r][col]) >= treeHeight) {
      break;
    }
  }

  treeVisibiityMap.set(
      `${r},${c}`,
      visibleFromTop * visibleFromRight * visibleFromBottom * visibleFromLeft,
  );
};

for (let r = 1; r < grid.length - 1; r++) {
  for (let c = 1; c < grid[r].length - 1; c++) {
    computeVisibility(r, c);
  }
}

const results = [...treeVisibiityMap.keys()].map((k) => treeVisibiityMap.get(k));
console.log(Math.max(...results));
