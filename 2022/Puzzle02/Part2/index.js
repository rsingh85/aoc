const readFileSyncIntoArray = require('../../Core/ReadFileSyncIntoArray');
const input = readFileSyncIntoArray('./data.txt');

const mapToObjects = function(round) {
  const roundSplit = round.split(' ');
  const choiceToCharDict = {
    'A': 'R', 'X': 'R', 'B': 'P', 'Y': 'P', 'C': 'S', 'Z': 'S',
  };
  const choiceToWinCharDict = {
    'R': 'P', 'S': 'R', 'P': 'S',
  };
  const choiceToLoseCharDict = {
    'R': 'S', 'S': 'P', 'P': 'R',
  };

  const decideMyChoice = function(opponentChoice, intendedResult) {
    if (intendedResult === 'Y') {
      return opponentChoice;
    } else if (intendedResult === 'X') {
      return choiceToLoseCharDict[opponentChoice];
    }
    return choiceToWinCharDict[opponentChoice];
  };

  return {
    opponent: choiceToCharDict[roundSplit[0]],
    me: decideMyChoice(choiceToCharDict[roundSplit[0]], roundSplit[1]),
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
