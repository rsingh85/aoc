const _ = require('underscore')
const readFileSyncIntoString = require('../../Core/ReadFileSyncIntoString')
const input = readFileSyncIntoString('./data.txt').split('\n')

const insertAt = (str, sub, pos) => 
    `${str.slice(0, pos)}${sub}${str.slice(pos)}`

const replaceFirstAndLastNumberWords = (str) => {
    const infoDict = [
        { find: 'one', replace: '1', fi: str.indexOf('one'), li: str.lastIndexOf('one') },
        { find: 'two', replace: '2', fi:  str.indexOf('two'), li: str.lastIndexOf('two') },
        { find: 'three', replace: '3', fi: str.indexOf('three'), li: str.lastIndexOf('three') },
        { find: 'four', replace: '4', fi: str.indexOf('four'), li: str.lastIndexOf('four') },
        { find: 'five', replace: '5', fi: str.indexOf('five'), li: str.lastIndexOf('five') },
        { find: 'six', replace: '6', fi: str.indexOf('six'), li: str.lastIndexOf('six') },
        { find: 'seven', replace: '7' ,fi: str.indexOf('seven'), li: str.lastIndexOf('seven') },
        { find: 'eight', replace: '8' ,fi: str.indexOf('eight'), li: str.lastIndexOf('eight') },
        { find: 'nine', replace: '9', fi: str.indexOf('nine'), li: str.lastIndexOf('nine') },
    ]

    const firstWordInfo = 
        _.sortBy(_.filter(infoDict, o => o.fi > -1), 'fi').at(0)

    const lastWordInfo = 
        _.sortBy(_.filter(infoDict, o => o.li > -1), 'li').at(-1)

    if (firstWordInfo)
        str = insertAt(str, firstWordInfo.replace, firstWordInfo.fi)

    if (lastWordInfo)
        str = insertAt(str, lastWordInfo.replace, lastWordInfo.li + (firstWordInfo ? 1 : 0))

    return str
}

const sum = input
    .map(l => replaceFirstAndLastNumberWords(l))
    .map(l => l.replaceAll(/[^\d]/g, ''))
    .map(n => Number(n.at(0) + n.at(-1)))
    .reduce((acc, curr) => acc + curr)

console.log(sum)