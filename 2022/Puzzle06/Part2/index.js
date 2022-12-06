const _ = require('underscore');
const readFileSyncIntoString = require('../../Core/ReadFileSyncIntoString');
const data = readFileSyncIntoString('./data.txt');
const scanLength = 14;

for (let pointer = scanLength; pointer < data.length; pointer++) {
  if (_.uniq(data.slice(pointer - scanLength, pointer)).length === scanLength) {
    console.log(pointer);
    break;
  }
}
