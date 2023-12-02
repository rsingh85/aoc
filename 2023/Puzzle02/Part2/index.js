const _ = require('underscore');
const readFileSyncIntoString = require('../../Core/ReadFileSyncIntoString')
const input = readFileSyncIntoString('./data.txt').split('\n')

const mapToGame = (game) => {
    const gameSplit = game.split(':');
    const setsSplit = gameSplit[1].split(';');

    return { 
        number: Number(gameSplit[0].split(' ')[1]),
        sets: setsSplit.map(s => s.split(',')
            .map(cube => ({ 
                count: Number(cube.trim().split(' ')[0]),
                colour: cube.trim().split(' ')[1]
            })))
    }
}

const games = input
    .map(g => mapToGame(g))

const powers = []

for (let g = 0; g < games.length; g++) {
    const game = games[g]
    let redCounts = [], greenCounts = [], blueCounts = []

    for (var s = 0; s < game.sets.length; s++) {
        const set = game.sets[s]

        set.filter(s => s.colour === 'red').forEach(s => redCounts.push(s.count))
        set.filter(s => s.colour === 'green').forEach(s => greenCounts.push(s.count))
        set.filter(s => s.colour === 'blue').forEach(s => blueCounts.push(s.count))
    }

    const maxRedCount = _.sortBy(_.filter(redCounts)).at(-1)
    const maxGreenCount = _.sortBy(_.filter(greenCounts)).at(-1)
    const maxBluCount = _.sortBy(_.filter(blueCounts)).at(-1)

    powers.push(maxRedCount * maxGreenCount * maxBluCount)
}

console.log(powers.reduce((acc, curr) => acc + curr))