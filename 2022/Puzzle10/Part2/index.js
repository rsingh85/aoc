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
let crtDisplay = '';
const pixelsPerCrtRow = 40;

while (pointer < instructions.length) {
  cycle++;

  crtDisplay +=
    ([xRegister - 1, xRegister, xRegister + 1].includes(cycle - 1)) ? '#' : '.';

  if (cycle % pixelsPerCrtRow === 0) {
    crtDisplay += '\n';
    cycle -= pixelsPerCrtRow;
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

console.log(crtDisplay);
