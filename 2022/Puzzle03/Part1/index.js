const _ = require('underscore');
const readFileSyncIntoArray = require('../../Core/ReadFileSyncIntoArray');
const rucksacks = readFileSyncIntoArray('./data.txt');

const getPriority = function(characterItem) {
  const ascii = characterItem.charCodeAt(0);
  return characterItem === characterItem.toUpperCase() ?
     ascii - 65 + 27 : ascii - 97 + 1;
};

let sum = 0;

for (const rucksack of rucksacks) {
  const common =
    _.intersection(
        [...rucksack.substring(0, rucksack.length / 2)],
        [...rucksack.substring(rucksack.length / 2, rucksack.length)],
    )[0];

  sum += getPriority(common);
}

console.log(sum);
