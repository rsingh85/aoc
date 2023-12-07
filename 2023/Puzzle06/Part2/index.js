const readFileSyncIntoString = require('../../Core/ReadFileSyncIntoString')
const input = readFileSyncIntoString('./data.txt').split('\n')

const times = input[0].split(':')[1].trim().split(/\s+/).map(n => Number(n))
const distances = input[1].split(':')[1].trim().split(/\s+/).map(n => Number(n))
const races = []

for (let i = 0; i < times.length; i++) {
    races.push({
        time: times[i],
        distance: distances[i]
    })
}

const countWaysToWin = (race) => {
    let wins = 0;

    for (let holdTime = 0; holdTime <= race.time; holdTime++) {
        const speed = holdTime
        const distanceTravelled = (race.time - holdTime) * speed
        wins += distanceTravelled > race.distance ? 1 : 0
    }

    return wins;
}

const waysToWin = []

for (let i = 0; i < races.length; i++) {
    const race = races[i]
    waysToWin.push(countWaysToWin(race)) 
}

console.log(waysToWin.reduce((prev, curr) => prev * curr))