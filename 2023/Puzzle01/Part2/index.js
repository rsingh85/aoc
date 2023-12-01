const _ = require('underscore');
const readFileSyncIntoString = require('../../Core/ReadFileSyncIntoString')
const input = readFileSyncIntoString('./data.txt').split('\n')

const insertAt = (str, sub, pos) => 
    `${str.slice(0, pos)}${sub}${str.slice(pos)}`;

const replaceFirstAndLastWords = (str) => {
    const infoDict = [
        { find: 'one', replace: '1', fi: -1, li: -1 },
        { find: 'two', replace: '2', fi: -1, li: -1 },
        { find: 'three', replace: '3', fi: -1, li: -1 },
        { find: 'four', replace: '4', fi: -1, li: -1 },
        { find: 'five', replace: '5', fi: -1, li: -1 },
        { find: 'six', replace: '6', fi: -1, li: -1 },
        { find: 'seven', replace: '7', fi: -1, li: -1 },
        { find: 'eight', replace: '8', fi: -1, li: -1 },
        { find: 'nine', replace: '9', fi: -1, li: -1 },
    ]

    infoDict.forEach(o => {
        o.fi = str.indexOf(o.find);
        o.li = str.lastIndexOf(o.find);
    })

    const firstWordInfo = 
        _.sortBy(_.filter(infoDict, o => o.fi > -1), 'fi').at(0)

    const lastWordInfo = 
        _.sortBy(_.filter(infoDict, o => o.li > -1), 'li').at(-1)

    if (firstWordInfo)
        str = insertAt(str, firstWordInfo.replace, firstWordInfo.fi)

    if (lastWordInfo)
        str = insertAt(str, lastWordInfo.replace, lastWordInfo.li + (firstWordInfo ? 1 : 0))

    return str;
};

const sum = input
    .map(l => replaceFirstAndLastWords(l))
    .map(l => l.replaceAll(/[^\d]/g, ''))
    .map(n => Number(n[0] + n[n.length - 1]))
    .reduce((acc, curr) => acc + curr)

console.log(sum)