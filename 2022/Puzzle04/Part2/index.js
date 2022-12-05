const _ = require('underscore');
const readFileSyncIntoArray = require('../../Core/ReadFileSyncIntoArray');
const pairs = readFileSyncIntoArray('./data.txt');

const overlaps = (pair) => {
  const pairSplit = pair.split(',');
  const rangeA =
    _.range(
        Number(pairSplit[0].split('-')[0]),
        Number(pairSplit[0].split('-')[1]) + 1);
  const rangeB =
    _.range(
        Number(pairSplit[1].split('-')[0]),
        Number(pairSplit[1].split('-')[1]) + 1);

  return rangeA.some((v) => rangeB.includes(v)) ||
    rangeB.some((v) => rangeA.includes(v));
};

const count = pairs.filter((p) => overlaps(p)).length;

console.log(count);
