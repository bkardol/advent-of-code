namespace Day7
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    internal class Hand : IComparable<Hand>
    {
        private readonly int[] cards;

        public int Bid { get; }

        public bool HasFiveOfAKind { get; private set; }

        public bool HasFourOfAKind { get; private set; }

        public bool HasFullHouse { get; private set; }

        public bool HasThreeOfAKind { get; private set; }

        public bool HasTwoPair { get; private set; }

        public bool HasOnePair { get; private set; }

        public Hand(string cards, string bid, bool partOne)
        {
            this.cards = cards.Select(card =>
            {
                return card switch
                {
                    'T' => 10,
                    'J' => partOne ? 11 : 1,
                    'Q' => 12,
                    'K' => 13,
                    'A' => 14,
                    '*' => 15,
                    _ => int.Parse(card.ToString()),
                };
            }).ToArray();
            this.Bid = int.Parse(bid);
            this.ProcessHand();
        }

        private void ProcessHand()
        {
            var amountOfJokers = 0;
            var matchCounter = new Dictionary<int, int>();
            foreach (var card in cards)
            {
                if (card == 1)
                {
                    ++amountOfJokers;
                }
                else
                {
                    if (matchCounter.ContainsKey(card))
                        ++matchCounter[card];
                    else
                        matchCounter.Add(card, 1);
                }
            }

            HasFiveOfAKind = matchCounter.Count <= 1;
            if (HasFiveOfAKind) return;

            HasFourOfAKind = matchCounter.Values.Any((v) => v + amountOfJokers == 4);
            if (HasFourOfAKind) return;

            HasFullHouse = matchCounter.Values.OrderByDescending(v => v).Take(2).Sum() + amountOfJokers >= 5;
            if (HasFullHouse) return;

            HasThreeOfAKind = matchCounter.Values.Any((v) => v + amountOfJokers == 3);
            if (HasThreeOfAKind) return;

            HasTwoPair = matchCounter.Values.OrderByDescending(v => v).Take(2).Sum() + amountOfJokers >= 4;
            if (HasTwoPair) return;

            HasOnePair = matchCounter.Values.Any((v) => v + amountOfJokers == 2);
        }

        public int CompareTo(Hand other)
        {
            var values = new[] { HasFiveOfAKind, HasFiveOfAKind, HasFourOfAKind, HasFullHouse, HasThreeOfAKind, HasTwoPair, HasOnePair };
            var otherValues = new[] { other.HasFiveOfAKind, other.HasFiveOfAKind, other.HasFourOfAKind, other.HasFullHouse, other.HasThreeOfAKind, other.HasTwoPair, other.HasOnePair };
            for (int i = 0; i < values.Length; i++)
            {
                var compared = values[i].CompareTo(otherValues[i]);
                if (compared != 0)
                    return compared;
                else if (values[i])
                    return this.CompareByIndividualCard(other);
            }
            return this.CompareByIndividualCard(other);
        }

        private int CompareByIndividualCard(Hand other)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                var compared = cards[i].CompareTo(other.cards[i]);
                if (compared != 0)
                    return compared;
            }
            return 0;
        }
    }
}
