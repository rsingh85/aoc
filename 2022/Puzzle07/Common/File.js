/* eslint-disable require-jsdoc */
class File {
  constructor(name = 'Unnamed', parent = undefined, size = 0) {
    this.name = name;
    this.parent = parent;
    this.size = size;
  }

  getType() {
    return 'File';
  }
}

module.exports = File;
