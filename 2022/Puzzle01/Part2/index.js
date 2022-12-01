const readFileSyncIntoString = require("../../Core/ReadFileSyncIntoString");
const input = readFileSyncIntoString("./data.txt").split("\n\n");

const calories = input
  .map((c) => c.split("\n").map(Number))
  .map((arr) => arr.reduce((acc, curr) => acc + curr, 0));

calories.sort(function (a, b) {
  return b - a;
});
console.log(calories[0] + calories[1] + calories[2]);
