const _ = require('underscore');
const readFileSyncIntoArray = require('../../Core/ReadFileSyncIntoArray');
const input = readFileSyncIntoArray('./data.txt');
const monkeys = [];

for (let i = 0; i < input.length; i += 7) {
  monkeys.push({
    'items': [...input[i + 1].trimEnd().split(': ')[1].split(',').map(Number)],
    'op': input[i + 2].split('new = ')[1].trimEnd(),
    'divisibleBy': Number(input[i + 3].trimEnd().split(' ')[5]),
    'successMonkey': Number(input[i + 4].trimEnd().split(' ')[9]),
    'failMonkey': Number(input[i + 5].trimEnd().split(' ')[9]),
    'inspectionCount': 0,
  });
}

const commonMultiple =
  monkeys
    .map((m) => m.divisibleBy)
    .reduce((acc, curr) => acc *= curr);

for (let round = 1; round <= 10000; round++) {
  for (let monkey = 0; monkey < monkeys.length; monkey++) {
    const currentMonkey = monkeys[monkey];

    while (currentMonkey.items.length > 0) {
      const item = currentMonkey.items.shift();
      const worryLevel =
        Math.floor(
          eval(currentMonkey.op.replaceAll('old', item)) % commonMultiple
        );

      currentMonkey.inspectionCount++;

      if (worryLevel % currentMonkey.divisibleBy === 0) {
        monkeys[currentMonkey.successMonkey].items.push(worryLevel);
      } else {
        monkeys[currentMonkey.failMonkey].items.push(worryLevel);
      }
    }
  }
}

const sortedInspectionCounts =
  monkeys.map((m) => m.inspectionCount)
    .sort((a, b) => b - a);

console.log(sortedInspectionCounts[0] * sortedInspectionCounts[1]);
