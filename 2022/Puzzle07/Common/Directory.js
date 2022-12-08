/* eslint-disable require-jsdoc */
class Directory {
  constructor(name = 'Unnamed', parent = undefined, children = []) {
    this.name = name;
    this.parent = parent;
    this.children = children;
  }

  getType() {
    return 'Directory';
  }

  addChild(item) {
    this.children.push(item);
  }

  getTotalFileSize() {
    return this.computeTotalFileSize(this.children);
  }

  computeTotalFileSize(dirChildren) {
    let sum = 0;

    for (const child of dirChildren) {
      if (child.getType() === 'File') {
        sum += child.size;
      } else if (child.getType() === 'Directory') {
        sum += this.computeTotalFileSize(child.children);
      }
    }

    return sum;
  }
}

module.exports = Directory;
