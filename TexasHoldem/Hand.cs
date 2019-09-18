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
            Straight = 4,
            Flush = 5,
            FullHouse = 6,
            FourOfAkind = 7,
            StraightFlush = 8,
            RoyalFlush = 9
        }

        public HandLabel Label { get; private set; }

        public Face Highest { get; private set; }

        public Hand(Face highest, HandLabel label)
        {
            Highest = highest;
            Label = label;
        }

        public bool Equals(Hand other)
        {
            return Label == other.Label && Highest == other.Highest;
        }


    }
}
