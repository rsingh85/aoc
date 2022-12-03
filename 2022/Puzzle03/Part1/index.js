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
  const ascii = item.charCodeAt(0);
  return item === item.toUpperCase() ?
     ascii - 65 + 27 : ascii - 97 + 1;
};

const sum = rucksacks
    .map((r) => getCommon(r)[0])
    .map((c) => getPriority(c))
    .reduce((acc, curr) => acc += curr);

console.log(sum);
