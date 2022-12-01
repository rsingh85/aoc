const readFileSyncIntoString = require("../../Core/ReadFileSyncIntoString");
const input = readFileSyncIntoString("./data.txt").split("\n\n");

const calories = input
  .map((e) => e.split("\n").map(Number))
  .map((arr) => arr.reduce((acc, curr) => acc + curr, 0));

console.log(Math.max(...calories));
