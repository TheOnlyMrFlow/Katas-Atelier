using System;
using System.Collections.Generic;
using System.Text;

namespace TexasHoldem
{

    public enum Face
    {
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
        Ace = 14
    }

    public enum Suit
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }

    public class Card : IComparable<Card>
    {

        public Face Face { get; set; }
        public Suit Suit { get; set; }
        
        public Card(Face face, Suit suit)
        {
            this.Face = face;
            this.Suit = suit;
        }

        public int CompareTo(Card other)
        {
            return 0;
        }
    }


}
