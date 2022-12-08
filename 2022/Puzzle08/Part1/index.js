const readFileSyncIntoArray = require('../../Core/ReadFileSyncIntoArray');
const trees = readFileSyncIntoArray('./data.txt');
const grid = trees.map((t) => [...t]);
const edgeVisibility = ((grid.length * 2) + (grid[0].length * 2)) - 4;
let isVisible = 0;

const isVisibleFromAnyEdge = function(r, c) {
  const treeHeight = grid[r][c];

  let visibleFromTop = true;
  for (let row = r - 1; row >= 0; row--) {
    if (Number(grid[row][c]) >= treeHeight) {
      visibleFromTop = false;
      break;
    }
  }

  if (visibleFromTop) return true;

  let visibleFromRight = true;
  for (let col = c + 1; col < grid[r, c].length; col++) {
    if (Number(grid[r][col]) >= treeHeight) {
      visibleFromRight = false;
      break;
    }
  }

  if (visibleFromRight) return true;

  let visibleFromBottom = true;
  for (let row = r + 1; row < grid[r].length; row++) {
    if (Number(grid[row][c]) >= treeHeight) {
      visibleFromBottom = false;
      break;
    }
  }

  if (visibleFromBottom) return true;

  let visibleFromLeft = true;
  for (let col = c - 1; col >= 0; col--) {
    if (Number(grid[r][col]) >= treeHeight) {
      visibleFromLeft = false;
      break;
    }
  }

  if (visibleFromLeft) return true;

  return false;
};

for (let r = 1; r < grid.length - 1; r++) {
  for (let c = 1; c < grid[r].length - 1; c++) {
    isVisible += isVisibleFromAnyEdge(r, c) ? 1 : 0;
  }
}

console.log(edgeVisibility + isVisible);
