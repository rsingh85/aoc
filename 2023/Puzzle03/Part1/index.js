const readFileSyncIntoString = require('../../Core/ReadFileSyncIntoString')
const input = readFileSyncIntoString('./data.txt').split('\n')

const isDigit = (char) => char >= '0' && char <= '9'

const isPart = (r, c) => {
    const top = r - 1 >= 0 ? input[r - 1].charAt(c) : '.'

    if (top && !isDigit(top) && top !== '.')
        return true

    const topRight = r - 1 >= 0 && c + 1 < input[r - 1].length ? input[r - 1].charAt(c + 1) : '.'

    if (!isDigit(topRight) && topRight !== '.')
        return true

    const right = c + 1 < input[r].length ? input[r].charAt(c + 1) : '.'

    if (!isDigit(right) && right !== '.')
        return true

    const bottomRight = r + 1 < input.length && c + 1 < input[r + 1].length ? input[r + 1].charAt(c + 1) : '.'
    
    if (!isDigit(bottomRight) && bottomRight !== '.')
        return true
    
    const bottom = r + 1 < input.length ? input[r + 1].charAt(c) : '.'

    if (!isDigit(bottom) && bottom !== '.')
        return true

    const bottomLeft = r + 1 < input.length && c - 1 >= 0 ? input[r + 1].charAt(c - 1) : '.'

    if (!isDigit(bottomLeft) && bottomLeft !== '.')
        return true

    const left = c - 1 >= 0 ? input[r].charAt(c - 1) : '.'

    if (!isDigit(left) && left !== '.')
        return true

    const topLeft = r - 1 >= 0 && c - 1 >= 0 ? input[r - 1].charAt(c - 1) : '.'

    if (!isDigit(topLeft) && topLeft !== '.')
        return true

    return false
}

const partNumbers = []

for (let r = 0; r < input.length; r++) {
    let currentNumber = ''
    let currentNumberIsPart = false

    for (let c = 0; c < input[r].length; c++) {
        const current = input[r].charAt(c)

        if (isDigit(current)) {
            currentNumber += current

            if (!currentNumberIsPart) {
                currentNumberIsPart = isPart(r, c)
            }
        }
        
        if (!isDigit(current) || c == input[r].length - 1) {
            if (currentNumberIsPart) {
                partNumbers.push(Number(currentNumber))
            }

            currentNumber = ''
            currentNumberIsPart = false
        }
    }
}

console.log(partNumbers.reduce((acc, curr) => acc + curr))