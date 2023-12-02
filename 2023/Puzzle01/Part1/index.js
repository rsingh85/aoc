const readFileSyncIntoString = require('../../Core/ReadFileSyncIntoString')
const input = readFileSyncIntoString('./data.txt').split('\n')

const sum = input
    .map(l => l.replaceAll(/[^\d]/g, ''))
    .map(n => Number(n.at(0) + n.at(-1)))
    .reduce((acc, curr) => acc + curr)

console.log(sum)