const readFileSyncIntoString = require('../../Core/ReadFileSyncIntoString')
const input = readFileSyncIntoString('./data.txt').split('\n')

const seeds = input[0].split(':')[1].trim().split(' ').map(s => Number(s))

const mappers = {
    'seed-to-soil': [], 
    'soil-to-fertilizer': [], 
    'fertilizer-to-water': [],
    'water-to-light': [],
    'light-to-temperature': [],
    'temperature-to-humidity': [],
    'humidity-to-location': []
}

const parseMap = (mapName) => {
    for (let i = input.indexOf(`${mapName} map:`) + 1; i < input.length && input[i] !== ''; i++) {
        const numbers = input[i].split(' ').map(n => Number(n))
        const mapping = {
            destRangeStart: numbers[0],
            destRangeEnd: numbers[0] + (numbers[2] - 1),
            srcRangeStart: numbers[1],
            srcRangeEnd: numbers[1] + (numbers[2] - 1),
            rangeLength: numbers[2]
        }

        mappers[mapName].push(mapping)
    }
}

parseMap('seed-to-soil')
parseMap('soil-to-fertilizer')
parseMap('fertilizer-to-water')
parseMap('water-to-light')
parseMap('light-to-temperature')
parseMap('temperature-to-humidity')
parseMap('humidity-to-location')

const applyMap = (mapName, sourceVal) => {
    for (var i = 0; i < mappers[mapName].length; i++) {
        const mapping = mappers[mapName][i]

        if (sourceVal >= mapping.srcRangeStart && sourceVal <= mapping.srcRangeEnd) {
            const offset = sourceVal - mapping.srcRangeStart
            return mapping.destRangeStart + offset 
        }
    }
    return sourceVal
}

let minLocation = -1;

for (let i = 0; i < seeds.length; i++) {
    const seed = seeds[i]

    const soil = applyMap('seed-to-soil', seed)
    const fertilizer = applyMap('soil-to-fertilizer', soil)
    const water = applyMap('fertilizer-to-water', fertilizer)
    const light = applyMap('water-to-light', water)
    const temp = applyMap('light-to-temperature', light)
    const humidity = applyMap('temperature-to-humidity', temp)
    const location = applyMap('humidity-to-location', humidity)

    minLocation = (minLocation === -1) ? location : Math.min(minLocation, location)
}

console.log(minLocation)