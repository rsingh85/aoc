const _ = require('underscore');
const readFileSyncIntoArray = require('../../Core/ReadFileSyncIntoArray');
const motions = readFileSyncIntoArray('./data.txt');
const headPosition = {r: 0, c: 0};
const tailPositions = [];

for (let i = 0; i < 9; i++) {
  tailPositions.push({r: 0, c: 0});
}

const visitedUniquePositionsByTail = new Set(['0,0']);

const print = (title) => {
  console.log(title);
  console.log();

  let grid = '';
  const maxR = Math.max(headPosition.r, ...tailPositions.map((t) => t.r))+1;
  const minR = Math.min(headPosition.r, ...tailPositions.map((t) => t.r));
  const maxC = Math.max(headPosition.c, ...tailPositions.map((t) => t.c))+1;
  const minC = Math.min(headPosition.c, ...tailPositions.map((t) => t.c));

  //console.log({maxR, minR, maxC, minC});

  for (let r = maxR; r >= minR; r--) {
    for (let c = minC; c < maxC; c++) {
      let char = '.';

      for (let i = tailPositions.length - 1; i >= 0; i--) {
        if (tailPositions[i].r === r && tailPositions[i].c === c) {
          char = `${(i + 1)}`;
        }
      }

      if (headPosition.r === r && headPosition.c === c) {
        char = 'H';
      }

      grid += char;
    }
    grid += '\n';
  }
  console.log(grid);
};

print('== Initial State ==');

for (const motion of motions) {
  const direction = motion.split(' ')[0];
  const steps = Number(motion.split(' ')[1]);
  let currentHeadPosition = headPosition;

  for (let i = 0; i < steps; i++) {
    switch (direction) {
      case 'U': currentHeadPosition.r++; break;
      case 'R': currentHeadPosition.c++; break;
      case 'D': currentHeadPosition.r--; break;
      case 'L': currentHeadPosition.c--; break;
    }

    for (let i = 0; i < tailPositions.length; i++) {
      if (i > 0) {
        currentHeadPosition = tailPositions[i - 1];
      }

      const surroundingHeadPositions = [
        `${currentHeadPosition.r},${currentHeadPosition.c}`,
        `${currentHeadPosition.r + 1},${currentHeadPosition.c}`,
        `${currentHeadPosition.r + 1},${currentHeadPosition.c + 1}`,
        `${currentHeadPosition.r},${currentHeadPosition.c + 1}`,
        `${currentHeadPosition.r - 1},${currentHeadPosition.c + 1}`,
        `${currentHeadPosition.r - 1},${currentHeadPosition.c}`,
        `${currentHeadPosition.r - 1},${currentHeadPosition.c - 1}`,
        `${currentHeadPosition.r},${currentHeadPosition.c - 1}`,
        `${currentHeadPosition.r + 1},${currentHeadPosition.c - 1}`,
      ];

      const tailPosition = tailPositions[i];

      if (surroundingHeadPositions
          .findIndex((v) => v === `${tailPosition.r},${tailPosition.c}`) > -1) {
        continue;
      }

      // is the head and tail in the same row?
      if (tailPosition.r === currentHeadPosition.r &&
          (direction === 'R' || direction === 'L')) {
        tailPosition.r += currentHeadPosition.r > tailPosition.r ? 1 : -1;
      }

      // is the head and tail in the same column?
      if (tailPosition.c === currentHeadPosition.c &&
            (direction === 'U' || direction === 'D')) {
        tailPosition.c += currentHeadPosition.c > tailPosition.c ? 1 : -1;
      }

      const possibleNewTailDiagonalPositions = [
        `${tailPosition.r + 1},${tailPosition.c + 1}`, // top right
        `${tailPosition.r - 1},${tailPosition.c + 1}`, // bottom right
        `${tailPosition.r - 1},${tailPosition.c - 1}`, // bottom left
        `${tailPosition.r + 1},${tailPosition.c - 1}`, // top left
      ];

      const newTailPosition =
        _.intersection(
            possibleNewTailDiagonalPositions,
            surroundingHeadPositions)[0];

      tailPosition.r = Number(newTailPosition.split(',')[0]);
      tailPosition.c = Number(newTailPosition.split(',')[1]);

      if (i === tailPositions.length - 1) {
        visitedUniquePositionsByTail.add(`${tailPosition.r},${tailPosition.c}`);
      }
    }

    print(`== ${motion} ==`);
  }
}

console.log(visitedUniquePositionsByTail.size);
