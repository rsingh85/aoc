const readFileIntoArray = require("../../Core/ReadFileIntoArray");
const input = readFileIntoArray("./data.txt");

let currentElfCalories = 0;
let elfCalories = [];

for (let i = 0; i < input.length; i++) {
    const calories = input[i];

    currentElfCalories += 
        calories === "" ? 0 : Number(calories);

    if (calories === "" ||  i == input.length - 1) {
        elfCalories.push(currentElfCalories);
        currentElfCalories = 0;
    }
}

elfCalories.sort(function(a, b) { return b - a } );
console.log(elfCalories[0] + elfCalories[1] + elfCalories[2]);