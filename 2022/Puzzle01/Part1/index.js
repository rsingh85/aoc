const readFileIntoArray = require("../../Core/ReadFileIntoArray");
const input = readFileIntoArray("./data.txt");

let max = 0;
let currentElfCalories = 0;

for (let i = 0; i < input.length; i++) {
    const calories = input[i];

    if (calories === "" ||  i == input.length - 1) {
        max = (currentElfCalories > max) ? currentElfCalories : max;

        currentElfCalories = 0;
    }

    currentElfCalories += Number(calories);
}

console.log(max);