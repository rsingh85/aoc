const lcm = require('compute-lcm')
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
let pathStepsToEnd = {}
let currentNodes = nodes.filter(n => n.name.endsWith('A'))

while (Object.keys(pathStepsToEnd).length < currentNodes.length) {
    steps++
    const currentIns = instructions[insPointer]

    // console.log(`Steps: ${steps}, Current Ins: ${currentIns}, CurrentNodes Length: ${currentNodes.length}, CurrentNodes Ending With Z: ${currentNodes.filter(cn => cn.name.endsWith('Z')).length}`)

    switch (currentIns) {
        case 'L':
            currentNodes = currentNodes.map(cn => nodes.find(n => n.name === cn.leftNodeName))
            break
        case 'R':
            currentNodes = currentNodes.map(cn => nodes.find(n => n.name === cn.rightNodeName))
            break
    }

    for (let i = 0; i < currentNodes.length; i++){
        const currentNode = currentNodes[i]
        
        if (currentNode.name.endsWith('Z') && !pathStepsToEnd[currentNode.name]) {
            pathStepsToEnd[currentNode.name] = steps
        }
    }

    insPointer = (insPointer + 1) % instructions.length
}

console.log(lcm(Object.values(pathStepsToEnd)))