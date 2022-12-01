const readFileIntoArray = require("../../Core/ReadFileIntoArray");
const input = readFileIntoArray("./data.txt");

let currentMaxElfCalories = 0;
let currentElfCalories = 0;

for (let i = 0; i < input.length; i++) {
    const calories = input[i];

    currentElfCalories += 
        calories === "" ? 
            0 : Number(calories);

    if (calories === "" ||  i == input.length - 1) {
        currentMaxElfCalories = 
            Math.max(currentElfCalories, currentMaxElfCalories)

        currentElfCalories = 0;
    }
}

console.log(currentMaxElfCalories);