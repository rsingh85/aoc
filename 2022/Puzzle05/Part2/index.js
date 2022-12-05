const _ = require('underscore');
const readFileSyncIntoArray = require('../../Core/ReadFileSyncIntoArray');
const instructions = readFileSyncIntoArray('./data.txt');

const stacksMap = new Map([
  [1, [...'WBDNCFJ']], [2, [...'PZVQLST']], [3, [...'PZBGJT']],
  [4, [...'DTLJZBHC']], [5, [...'GVBJS']], [6, [...'PSQ']],
  [7, [...'BVDFLMPN']], [8, [...'PSMFBDLR']], [9, [...'VDTR']],
]);

for (const instruction of instructions) {
  const splitIns = instruction.split(' ');
  const moveCount = Number(splitIns[1]);
  const fromStack = Number(splitIns[3]);
  const toStack = Number(splitIns[5]);
  const cratesToMove = [];

  _.range(0, moveCount).forEach(() => {
    cratesToMove.push(stacksMap.get(fromStack).pop());
  });

  stacksMap.get(toStack).push(...cratesToMove.reverse());
}

const topCrates =
  [...stacksMap.keys()]
      .reduce((acc, curr) => acc += stacksMap.get(curr).pop(), '');

console.log(topCrates);
