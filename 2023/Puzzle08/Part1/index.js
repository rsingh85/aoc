const readFileSyncIntoString = require('../../Core/ReadFileSyncIntoString')
const input = readFileSyncIntoString('./data.txt').split('\n')


const mapToNode = (line) => {
    return  {
        name: line.split(' = ')[0],
        leftNodeName: line.split(' = ')[1].split(',')[0].substring(1),
        rightNodeName: line.split(' = ')[1].split(', ')[1].substring(0, line.split(' = ')[1].split(', ')[1].length - 1)
    }
}


const instructions = input[0].split('')
const nodes = input.slice(2).map(l => mapToNode(l))

let insPointer = 0
let steps = 0
let currentNode = nodes.find(n => n.name === 'AAA')

while (currentNode.name !== 'ZZZ') {
    steps++
    const currentIns = instructions[insPointer]

    switch (currentIns) {
        case 'L':
            currentNode = nodes.find(n => n.name === currentNode.leftNodeName)
            break
        case 'R':
            currentNode = nodes.find(n => n.name === currentNode.rightNodeName)
            break
    }

    insPointer = (insPointer + 1) % instructions.length
}

console.log(steps)