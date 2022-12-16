
const readFileSyncIntoArray = require('../../Core/ReadFileSyncIntoArray');
const mapInstruction = (ins) => {
  const op = ins.split(' ')[0];
  return {
    'op': op,
    'args': op === 'addx' ? Number(ins.split(' ')[1]) : undefined,
    'executionCycles': op === 'addx' ? 2 : 1,
    'executedCycles': 0,
  };
};

const instructions =
  readFileSyncIntoArray('./data.txt')
      .map(mapInstruction);

let pointer = 0;
let cycle = 0;
let xRegister = 1;
let signalStrengthSum = 0;
const targetCycles = [20, 60, 100, 140, 180, 220];

while (true) {
  if (pointer === instructions.length) {
    break;
  }

  cycle++;

  if (targetCycles.includes(cycle)) {
    signalStrengthSum += cycle * xRegister;
  }

  const instruction = instructions[pointer];
  instruction.executedCycles++;

  if (instruction.executedCycles === instruction.executionCycles) {
    switch (instruction.op) {
      case 'addx': xRegister += instruction.args; break;
      case 'noop': break;
    }

    pointer++;
  }
}

console.log(signalStrengthSum);
