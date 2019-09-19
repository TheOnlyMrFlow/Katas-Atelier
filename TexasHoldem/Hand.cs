using System;
using System.Collections.Generic;
using System.Text;

namespace TexasHoldem
{
    public class Hand : IEquatable<Hand>
    {
        public enum HandLabel
        {
            HighCard = 0,
            Pair = 1,
            TwoPairs = 2,
            ThreeOfAkind = 3,
            Straight = 4, // Five cards in numerical order, but not of the same suit.
            Flush = 5, // Five cards, all in one suit, but not in numerical order.
            FullHouse = 6, // A pair plus three of a kind in the same hand.
            FourOfAkind = 7,
            StraightFlush = 8, // Five cards in a row, all in the same suit.
            RoyalFlush = 9 // Ten, Jack, Queen, King, Ace all in the same suit.
        }

        public HandLabel Label { get; private set; }

        public ISet<Card> Cards { get; private set; }

        public Hand(ISet<Card> cards, HandLabel label)
        {
            Cards = cards;
            Label = label;
        }

        public bool Equals(Hand other)
        {
            return Label == other.Label && Cards.SetEquals(other.Cards);
        }


    }
}
