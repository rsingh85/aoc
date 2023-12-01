const fs = require('fs');

module.exports = function(filePath) {
  try {
    return fs.readFileSync('./data.txt', 'utf8');
  } catch (err) {
    console.error(err);
  }
};
