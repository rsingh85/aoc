const readFileSyncIntoArray = require('../../Core/ReadFileSyncIntoArray');
const rucksacks = readFileSyncIntoArray('./data.txt');
const _ = require('underscore');

const getPriority = function(characterItem) {
  const asciiDec = characterItem.charCodeAt(0);
  return characterItem === characterItem.toUpperCase() ?
     asciiDec - 65 + 27 : asciiDec - 97 + 1;
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
