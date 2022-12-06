const _ = require('underscore');
const readFileSyncIntoString = require('../../Core/ReadFileSyncIntoString');
const data = readFileSyncIntoString('./data.txt');
const scanLength = 14;

for (let pointer = scanLength; pointer <= data.length; pointer++) {
  const segment = [...data.slice(pointer - scanLength, pointer)];
  if (_.uniq(segment).length === scanLength) {
    console.log(pointer);
    break;
  }
}
