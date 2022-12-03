const _ = require('underscore');
const readFileSyncIntoArray = require('../../Core/ReadFileSyncIntoArray');
const rucksacks = readFileSyncIntoArray('./data.txt');

const getCommon = function(rucksack) {
  return _.intersection(
      [...rucksack.substring(0, rucksack.length / 2)],
      [...rucksack.substring(rucksack.length / 2, rucksack.length)],
  );
};

const getPriority = function(item) {
  return item === item.toUpperCase() ?
    item.charCodeAt(0) - 65 + 27 : item.charCodeAt(0) - 97 + 1;
};

const sum = rucksacks
    .map((r) => getCommon(r)[0])
    .map((c) => getPriority(c))
    .reduce((acc, curr) => acc += curr);

console.log(sum);
