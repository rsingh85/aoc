const _ = require('underscore');
const readFileSyncIntoArray = require('../../Core/ReadFileSyncIntoArray');
const motions = readFileSyncIntoArray('./data.txt');
const headPosition = {r: 0, c: 0};
const tailPosition = {r: 0, c: 0};
const visitedUniquePositionsByTail = new Set(['0,0']);

for (const motion of motions) {
  const direction = motion.split(' ')[0];
  const steps = Number(motion.split(' ')[1]);

  for (let i = 0; i < steps; i++) {
    switch (direction) {
      case 'U': headPosition.r++; break;
      case 'R': headPosition.c++; break;
      case 'D': headPosition.r--; break;
      case 'L': headPosition.c--; break;
    }

    const surroundingHeadPositions = [
      `${headPosition.r},${headPosition.c}`, // overlap
      `${headPosition.r + 1},${headPosition.c}`, // top
      `${headPosition.r + 1},${headPosition.c + 1}`, // top right
      `${headPosition.r},${headPosition.c + 1}`, // right
      `${headPosition.r - 1},${headPosition.c + 1}`, // bottom right
      `${headPosition.r - 1},${headPosition.c}`, // bottom
      `${headPosition.r - 1},${headPosition.c - 1}`, // bottom left
      `${headPosition.r},${headPosition.c - 1}`, // left
      `${headPosition.r + 1},${headPosition.c - 1}`, // top left
    ];

    if (surroundingHeadPositions
        .findIndex((v) => v === `${tailPosition.r},${tailPosition.c}`) > -1) {
      continue;
    }

    // is the head and tail in the same row?
    if (tailPosition.r === headPosition.r &&
         (direction === 'R' || direction === 'L')) {
      tailPosition.r += headPosition.r > tailPosition.r ? 1 : -1;
    }

    // is the head and tail in the same column?
    if (tailPosition.c === headPosition.c &&
          (direction === 'U' || direction === 'D')) {
      tailPosition.c += headPosition.c > tailPosition.c ? 1 : -1;
    }

    const possibleNewTailDiagonalPositions = [
      `${tailPosition.r + 1},${tailPosition.c + 1}`, // top right
      `${tailPosition.r - 1},${tailPosition.c + 1}`, // bottom right
      `${tailPosition.r - 1},${tailPosition.c - 1}`, // bottom left
      `${tailPosition.r + 1},${tailPosition.c - 1}`, // top left
    ];

    const newTailPosition =
      _.intersection(
          possibleNewTailDiagonalPositions, surroundingHeadPositions)[0];

    tailPosition.r = Number(newTailPosition.split(',')[0]);
    tailPosition.c = Number(newTailPosition.split(',')[1]);

    visitedUniquePositionsByTail.add(`${tailPosition.r},${tailPosition.c}`);
  }
}

console.log(visitedUniquePositionsByTail.size);
