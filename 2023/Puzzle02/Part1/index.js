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

const redLimit = 12, greenLimit = 13, blueLimit = 14

const games = input
    .map(g => mapToGame(g))

const possibleGames = []

for (let g = 0; g < games.length; g++) {
    const game = games[g]
    let pass = true

    for (var s = 0; s < game.sets.length; s++) {
        const set = game.sets[s]

        pass &= set.filter(s => s.colour === 'red').every(s => s.count <= redLimit)
        pass &= set.filter(s => s.colour === 'green').every(s => s.count <= greenLimit)
        pass &= set.filter(s => s.colour === 'blue').every(s => s.count <= blueLimit)
    }

    if (pass)
        possibleGames.push(game.number)
}

console.log(possibleGames.reduce((acc, curr) => acc + curr))