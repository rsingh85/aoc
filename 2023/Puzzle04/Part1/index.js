const readFileSyncIntoString = require('../../Core/ReadFileSyncIntoString')
const input = readFileSyncIntoString('./data.txt').split('\n')

const mapToObject = (line) => {
    const baseSplit = line.split(':')

    return {
        cardNumber: Number(baseSplit[0].split(' ')[1]),
        winningNumbers: baseSplit[1].split('|')[0].trim().split(/\s\s?/).map(n => Number(n)),
        myNumbers: baseSplit[1].split('|')[1].trim().split(/\s\s?/).map(n => Number(n))
    }
}

const cards = input.map(l => mapToObject(l))
const cardPoints = [];

for (let i = 0; i < cards.length; i++) {
    const card = cards[i]
    let points = 0

    for (let m = 0; m < card.myNumbers.length; m++) {
        const myNumber = card.myNumbers[m]

        if (card.winningNumbers.includes(myNumber)) {
            points = (points === 0) ? 1 : points * 2
        }
    }

    cardPoints.push(points)
}

console.log(cardPoints.reduce((acc, curr) => acc + curr))