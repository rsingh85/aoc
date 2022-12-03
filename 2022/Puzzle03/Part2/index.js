const _ = require('underscore');
const readFileSyncIntoArray = require('../../Core/ReadFileSyncIntoArray');
const rucksacks = readFileSyncIntoArray('./data.txt');

const getPriority = function(item) {
  return item === item.toUpperCase() ?
    item.charCodeAt(0) - 65 + 27 : item.charCodeAt(0) - 97 + 1;
};

const commons = [];

for (let i = 0; i < rucksacks.length; i += 3) {
  commons.push(
      _.intersection(rucksacks[i], rucksacks[i + 1], rucksacks [i + 2])[0],
  );
}

const sum = commons
    .map((c) => getPriority(c))
    .reduce((acc, curr) => acc += curr);

console.log(sum);
