const readFileSyncIntoString = require('../../Core/ReadFileSyncIntoString');
const input = readFileSyncIntoString('./data.txt').split('\n\n');

const calories = input
    .map((c) => c.split('\n').map(Number))
    .map((arr) => arr.reduce((acc, curr) => acc + curr));

console.log(Math.max(...calories));
