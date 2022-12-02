const readFileSyncIntoArray = require('../../Core/ReadFileSyncIntoArray');
const input = readFileSyncIntoArray('./data.txt');

const mapToObjects = function(round) {
  const roundSplit = round.split(' ');
  const choiceToCharDict = {
    'A': 'R', 'X': 'R', 'B': 'P', 'Y': 'P', 'C': 'S', 'Z': 'S',
  };
  return {
    opponent: choiceToCharDict[roundSplit[0]],
    me: choiceToCharDict[roundSplit[1]],
    getMyScore: function() {
      const scoreForChoiceDict = {
        'R': 1, 'P': 2, 'S': 3,
      };

      if (this.me === this.opponent) {
        return 3 + scoreForChoiceDict[this.me];
      } else if (
        (this.me == 'R' && this.opponent == 'S') ||
            (this.me == 'S' && this.opponent == 'P') ||
                (this.me == 'P' && this.opponent == 'R')) {
        return 6 + scoreForChoiceDict[this.me];
      }
      return scoreForChoiceDict[this.me];
    },
  };
};

const score =
    input.map((r) => mapToObjects(r))
        .map((o) => o.getMyScore())
        .reduce((acc, curr) => acc += curr);

console.log(score);
