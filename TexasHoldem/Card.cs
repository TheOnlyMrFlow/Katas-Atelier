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

        public Card(String cardAsString)
        {
            if (cardAsString.Length != 2)
                throw new Exception("A card is represented by 2 characters");

            char faceChar = cardAsString[0];
            char suitChar = cardAsString[1];

            Face face;
            Suit suit;


            if (faceChar >= '1' && faceChar <= '9')
                face = (Face) Char.GetNumericValue(faceChar);

            else if (faceChar == 'T')
                face = Face.Ten;

            else if (faceChar == 'J')
                face = Face.Jack;

            else if (faceChar == 'Q')
                face = Face.Queen;

            else if (faceChar == 'K')
                face = Face.King;

            else if (faceChar == 'A')
                face = Face.Ace;

            else
                throw new Exception("Unknown card face");

            if (suitChar == 'c')
                suit = Suit.Clubs;

            else if (suitChar == 'd')
                suit = Suit.Diamonds;

            else if (suitChar == 'h')
                suit = Suit.Hearts;

            else if (suitChar == 's')
                suit = Suit.Spades;

            else
                throw new Exception("Unknown card suit");

            this.Face = face;
            this.Suit = suit;

        }

        public int CompareTo(Card other)
        {
            return this.Face - other.Face;
        }
    }


}
