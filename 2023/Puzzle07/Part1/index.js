const readFileSyncIntoString = require('../../Core/ReadFileSyncIntoString')
const input = readFileSyncIntoString('./data.txt').split('\n')

const mapToHand = (line) => {
    return {
        hand: [...line.split(' ')[0]],
        bid: Number(line.split(' ')[1]),
        getSortRank: function() {
            if (this.isFiveOfAKind())
                return 7
            else if (this.isFourOfAKind())
                return 6
            else if (this.isFullHouse()) 
                return 5
            else if (this.isThreeOfAKind())
                return 4
            else if (this.isTwoPair())
                return 3
            else if (this.isOnePair())
                return 2
            else if (this.isHighCard())
                return 1

            return -1
        },
        isFiveOfAKind: function() {
            // Five of a kind, where all five cards have the same label: AAAAA
            return this.hand.every(c => c === this.hand[0])
        },
        isFourOfAKind: function() {
            // Four of a kind, where four cards have the same label and one card has a different label: AA8AA
            return this.hand.filter(c => c === this.hand[0]).length === 4 ||
                this.hand.filter(c => c === this.hand[1]).length === 4
        },
        isFullHouse: function() {
            // Full house, where three cards have the same label, and the remaining two cards share a different label: 23332
            for (let i = 0; i < this.hand.length; i++) {
                const card = this.hand[i]

                if (this.hand.filter(c => c === card).length === 3) {
                    const otherTwoCards = this.hand.filter(c => c !== card)

                    if (otherTwoCards.length === 2)
                        return true
                }
            }

            return false
        },
        isThreeOfAKind: function() {
            // Three of a kind, where three cards have the same label, and the remaining two cards are each different from any other card in the hand: TTT98
            for (let i = 0; i < this.hand.length; i++) {
                const card = this.hand[i]

                if (this.hand.filter(c => c === card).length === 3) {
                    const otherTwoCards = this.hand.filter(c => c !== card)

                    if (new Set(otherTwoCards).length === 2)
                        return true
                }
            }

            return false
        },
        isTwoPair: function() {
            // Two pair, where two cards share one label, two other cards share a second label, and the remaining card has a third label: 23432
            const pairChars = []
            
            for (let i = 0; i < this.hand.length; i++) {
                const card = this.hand[i]

                if (this.hand.filter(c => c === card).length === 2) {
                    if (!pairChars.includes(card)) {
                        pairChars.push(card)
                    }
                }
            }

            if (pairChars.length === 2) {
                return true
            }

            return false
        },
        isOnePair: function() {
            // One pair, where two cards share one label, and the other three cards have a different label from the pair and each other: A23A4
            for (let i = 0; i < this.hand.length; i++) {
                const card = this.hand[i]

                if (this.hand.filter(c => c === card).length === 2) {
                    const otherThreeCards = this.hand.filter(c => c !== card)

                    if (new Set(otherThreeCards).length === 3)
                        return true
                }
            }

            return false
        },
        isHighCard: function() {
            // High card, where all cards' labels are distinct: 23456
            return new Set(this.hand).length === this.hand.length
        }
    }
}

const hands = input.map(l => mapToHand(l))

hands.sort((cardA, cardB) => {
    const cardASortRank = cardA.getSortRank()
    const cardBSortRank = cardB.getSortRank()

    if (cardASortRank < cardBSortRank)
        return -1

    if (cardASortRank > cardBSortRank)
        return 1

    const dict = {
        'A': 13, 'K': 12,
        'Q': 11, 'J': 10,
        'T': 9, '9': 8,
        '8': 7, '7': 6,
        '6': 5, '5': 4,
        '4': 3, '3': 2,
        '2': 1
    }

    // todo: compare card by card
    for (let i = 0; i < cardA.hand.length; i++) {
        if (dict[cardA.hand[i]] === dict[cardB.hand[i]])
            continue
        
        if (dict[cardA.hand[i]] < dict[cardB.hand[i]])
            return -1
        
        if (dict[cardA.hand[i]] > dict[cardB.hand[i]])
            return 1
    }
    return 0
})

let total = 0

for (let i = 0; i < hands.length; i++) {
    total += hands[i].bid * (i + 1)
}

console.log(total)