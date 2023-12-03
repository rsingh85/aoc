const readFileSyncIntoString = require('../../Core/ReadFileSyncIntoString')
const input = readFileSyncIntoString('./data.txt').split('\n')

const isDigit = (char) => char >= '0' && char <= '9'

const isPotentialGear = (r, c) => {
    const top = r - 1 >= 0 ? input[r - 1].charAt(c) : '.'
    if (top == '*') return { result: true, gearLocR: r - 1, gearLocC: c }

    const topRight = r - 1 >= 0 && c + 1 < input[r - 1].length ? input[r - 1].charAt(c + 1) : '.'
    if (topRight == '*') return { result: true, gearLocR: r - 1, gearLocC: c + 1 }

    const right = c + 1 < input[r].length ? input[r].charAt(c + 1) : '.'
    if (right == '*') return { result: true, gearLocR: r, gearLocC: c + 1}

    const bottomRight = r + 1 < input.length && c + 1 < input[r + 1].length ? input[r + 1].charAt(c + 1) : '.'
    if (bottomRight == '*') return { result: true, gearLocR: r + 1, gearLocC: c + 1}
    
    const bottom = r + 1 < input.length ? input[r + 1].charAt(c) : '.'
    if (bottom == '*') return { result: true, gearLocR: r + 1, gearLocC: c }

    const bottomLeft = r + 1 < input.length && c - 1 >= 0 ? input[r + 1].charAt(c - 1) : '.'
    if (bottomLeft == '*') return { result: true, gearLocR: r + 1, gearLocC: c - 1}

    const left = c - 1 >= 0 ? input[r].charAt(c - 1) : '.'
    if (left == '*') return { result: true, gearLocR: r, gearLocC: c - 1}

    const topLeft = r - 1 >= 0 && c - 1 >= 0 ? input[r - 1].charAt(c - 1) : '.'
    if (topLeft == '*') return { result: true, gearLocR: r - 1, gearLocC: c - 1}

    return { result: false }
}

const potentialGears = []

for (let r = 0; r < input.length; r++) {
    let currentNumber = ''
    let currentNumberIsPotentialGearResult = {}
    let currentNumberIsPotentialGear = false

    for (let c = 0; c < input[r].length; c++) {
        const current = input[r].charAt(c)

        if (isDigit(current)) {
            currentNumber += current

            if (!currentNumberIsPotentialGear) {
                currentNumberIsPotentialGearResult = isPotentialGear(r, c)

                currentNumberIsPotentialGear = currentNumberIsPotentialGearResult.result
            }
        }
        
        if (!isDigit(current) || c == input[r].length - 1) {
            if (currentNumberIsPotentialGear) {
                potentialGears.push({
                        number: Number(currentNumber),
                        gearLocR: currentNumberIsPotentialGearResult.gearLocR,
                        gearLocC: currentNumberIsPotentialGearResult.gearLocC,
                    }
                )
            }

            currentNumber = ''
            currentNumberIsPotentialGear = false
        }
    }
}

const gearRatios = []

for (let i = 0; i < potentialGears.length; i++) {
    const currentPotentialGear = potentialGears[i]

    if (currentPotentialGear.paired !== undefined)
        continue;

    const associatedGear = 
        potentialGears.filter(g => 
            g.number !==  currentPotentialGear.number &&
            g.gearLocR === currentPotentialGear.gearLocR &&
            g.gearLocC === currentPotentialGear.gearLocC &&
            g.paired === undefined)
    
    if (associatedGear.length === 1) {
        currentPotentialGear.paired = true
        associatedGear[0].paired = true

        gearRatios.push(currentPotentialGear.number * associatedGear[0].number)
    }    
}

console.log(gearRatios.reduce((acc, curr) => acc + curr))