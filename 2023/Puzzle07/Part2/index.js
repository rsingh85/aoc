const readFileSyncIntoString = require('../../Core/ReadFileSyncIntoString')
const input = readFileSyncIntoString('./data.txt').split('\n')

const mapToHand = (line) => {
    return {
        cards: [...line.split(' ')[0]],
        originalCards: [...line.split(' ')[0]],
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
            return this.cards.every(c => c == this.cards[0])
        },
        isFourOfAKind: function() {
            // Four of a kind, where four cards have the same label and one card has a different label: AA8AA
            return this.cards.filter(c => c == this.cards[0]).length === 4 ||
                this.cards.filter(c => c == this.cards[1]).length === 4
        },
        isFullHouse: function() {
            // Full house, where three cards have the same label, and the remaining two cards share a different label: 23332
            if (this.isThreeOfAKind()) return false
            
            for (let i = 0; i < this.cards.length; i++) {
                const card = this.cards[i]

                if (this.cards.filter(c => c == card).length === 3) {
                    const otherTwoCards = this.cards.filter(c => c !== card)

                    if (!this.cards.filter(c => c == card).includes(otherTwoCards[0]) &&
                            !this.cards.filter(c => c == card).includes(otherTwoCards[1]))
                        return true
                }
            }

            return false
        },
        isThreeOfAKind: function() {
            // Three of a kind, where three cards have the same label, and the remaining two cards are each different from any other card in the hand: TTT98
            for (let i = 0; i < this.cards.length; i++) {
                const card = this.cards[i]

                if (this.cards.filter(c => c == card).length === 3) {
                    const otherTwoCards = this.cards.filter(c => c != card)

                    if (this.cards.filter(c => c == otherTwoCards[0]).length === 1 && 
                            this.cards.filter(c => c == otherTwoCards[1]).length)
                        return true
                }
            }

            return false
        },
        isTwoPair: function() {
            // Two pair, where two cards share one label, two other cards share a second label, and the remaining card has a third label: 23432
            const pairChars = []
            
            for (let i = 0; i < this.cards.length; i++) {
                const card = this.cards[i]

                if (this.cards.filter(c => c == card).length === 2) {
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
            for (let i = 0; i < this.cards.length; i++) {
                const card = this.cards[i]

                if (this.cards.filter(c => c == card).length === 2) {
                    const otherThreeCards = this.cards.filter(c => c != card)

                    if (new Set(otherThreeCards).size === 3)
                        return true
                }
            }

            return false
        },
        isHighCard: function() {
            // High card, where all cards' labels are distinct: 23456
            return new Set(this.cards).size === this.cards.length
        }
    }
}

const getMostFrequentCard = (hand) => {
    const cardFrequency = {}
    
    hand.cards
        .filter(c => c != 'J')
        .forEach(c => cardFrequency[c] = (cardFrequency[c] || 0) + 1)
    
    return Object.keys(cardFrequency)
        .reduce((a, b) => cardFrequency[a] > cardFrequency[b] ? a : b, hand.cards[0])
}

const processWildcard = (hand) => {
    const mostFrequentCard = getMostFrequentCard(hand)

    if (hand.cards.includes('J')) {
        for (let i = 0; i < hand.cards.length; i++) {
            const card = hand.cards[i]

            if (card == 'J') 
                hand.cards[i] = mostFrequentCard
        }
    }

    return hand
}

const hands = input
    .map(l => mapToHand(l))
    .map(h => processWildcard(h))

hands.sort((handA, handB) => {
    const handASortRank = handA.getSortRank()
    const handBSortRank = handB.getSortRank()

    if (handASortRank < handBSortRank)
        return -1

    if (handASortRank > handBSortRank)
        return 1

    const dict = {
        'A': 13, 'K': 12,
        'Q': 11, 'J': 0,
        'T': 9, '9': 8,
        '8': 7, '7': 6,
        '6': 5, '5': 4,
        '4': 3, '3': 2,
        '2': 1
    }

    for (let i = 0; i < handA.originalCards.length; i++) {
        if (dict[handA.originalCards[i]] === dict[handB.originalCards[i]])
            continue
        
        if (dict[handA.originalCards[i]] < dict[handB.originalCards[i]])
            return -1
        
        if (dict[handA.originalCards[i]] > dict[handB.originalCards[i]])
            return 1
    }
    return 0
})

let total = 0

for (let i = 0; i < hands.length; i++) {
    total += hands[i].bid * (i + 1)
}

console.log(total)