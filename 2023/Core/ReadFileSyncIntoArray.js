const fs = require('fs');

module.exports = function(filePath) {
  try {
    const data = fs.readFileSync('./data.txt', 'utf8');
    return data.split('\n');
  } catch (err) {
    console.error(err);
  }
};
