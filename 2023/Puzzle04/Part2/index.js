const _ = require('underscore')
const readFileSyncIntoString = require('../../Core/ReadFileSyncIntoString')
const input = readFileSyncIntoString('./data.txt').split('\n')

const mapToObject = (line) => {
    const baseSplit = line.split(':')

    return {
        cardNumber: Number(baseSplit[0].split(' ')[1]),
        winningNumbers: baseSplit[1].split('|')[0].trim().split(/\s\s?/).map(n => Number(n)),
        myNumbers: baseSplit[1].split('|')[1].trim().split(/\s\s?/).map(n => Number(n)),
        instanceCount: 1,
        getMatchCount: function() { return _.intersection(this.winningNumbers, this.myNumbers).length },
    }
}

const cards = input.map(l => mapToObject(l))
let totalCards = 0

for (let i = 0; i < cards.length; i++) {
    const currentCard = cards[i]
    const currentCardMatchCount = currentCard.getMatchCount()

    totalCards += currentCard.instanceCount

    for (let j = i + 1; (j < cards.length) && (j < (i + 1 + currentCardMatchCount)); j++) {
        const copiedCard = cards[j]
        copiedCard.instanceCount += currentCard.instanceCount 
    }
}

console.log(totalCards)