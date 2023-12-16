const readFileSyncIntoString = require('../../Core/ReadFileSyncIntoString')
const input = readFileSyncIntoString('./data.txt').split('\n')

const histories = input.map(l => l.trim().split(' ').map(n => Number(n))) 

const getNextSequence = (nums) => {
    const sequence = []
    for (let i = 0; i < nums.length - 1; i++) {
        const diff = nums[i + 1] - nums[i]
        sequence.push(diff)
    }
    return sequence.length === 0 ? [0] : sequence
}

const extrapolateFromSequences = (sequences) => {
    let val = 0;
    for (let i = 0; i < sequences.length; i++) {
        val += sequences[i].at(-1)
    }
    return val
}

let extrapolatedValueSum = 0

for (let i = 0; i < histories.length; i++) {
    const history = histories[i]
    const sequences = [history]
    let foundZeroSequence = false

    while (!foundZeroSequence) {
        const nextSequence = getNextSequence(sequences.at(-1))

        sequences.push(nextSequence)
        foundZeroSequence = nextSequence.every(n => n === 0)      
    }
    
    extrapolatedValueSum += extrapolateFromSequences(sequences)
}

console.log(extrapolatedValueSum)