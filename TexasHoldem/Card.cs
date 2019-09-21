﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TexasHoldem
{

    public enum Face
    {
        One = 1, // only used when Ace takes the value of one
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

    [Serializable]
    public class Card : CardSet, IComparable<Card>, IComparable<CardSet>, IEquatable<Card>
    {

        private static readonly Dictionary<char, Suit> charToSuit
            = new Dictionary<char, Suit>
                {
                    { 'c', Suit.Clubs },
                    { 'd', Suit.Diamonds },
                    { 'h', Suit.Hearts },
                    { 's', Suit.Spades },
                };

        private static readonly Dictionary<Suit, char> suitToChar
            = charToSuit.ToDictionary((i) => i.Value, (i) => i.Key);


        private static readonly Dictionary<char, Face> charToFace
            = new Dictionary<char, Face>
                {
                    { '1', Face.One },
                    { '2', Face.Two },
                    { '3', Face.Three },
                    { '4', Face.Four },
                    { '5', Face.Five },
                    { '6', Face.Six },
                    { '7', Face.Seven },
                    { '8', Face.Eight },
                    { '9', Face.Nine },
                    { 'T', Face.Ten },
                    { 'J', Face.Jack },
                    { 'Q', Face.Queen },
                    { 'K', Face.King },
                    { 'A', Face.Ace },
                };

        private static readonly Dictionary<Face, char> faceToChar
            = charToFace.ToDictionary((i) => i.Value, (i) => i.Key);

        public Face Face { get; set; }
        public Suit Suit { get; set; }
        
        public Card(Face face, Suit suit)
            :base(CardSetLabel.SingleCard, new CardSet[0])
        {
            this.Face = face;
            this.Suit = suit;
        }

        public Card(String cardAsString)
            : base(CardSetLabel.SingleCard, new CardSet[0])
        {
            if (cardAsString.Length != 2)
                throw new Exception("A card is represented by 2 characters");

            char faceChar = cardAsString[0];
            char suitChar = cardAsString[1];

            Face face;
            Suit suit;


            if (!charToFace.TryGetValue(faceChar, out face))
                throw new Exception("Unknown card face");

            if (!charToSuit.TryGetValue(suitChar, out suit))
                throw new Exception("Unknown card suit");


            this.Face = face;
            this.Suit = suit;

        }

        public int CompareTo(Card other)
        {
            return this.Face - other.Face;
        }

        public int CompareTo(CardSet other)
        {
            var otherAsCard = other as Card;

            if (otherAsCard != null)
            {
                return this.CompareTo(otherAsCard);
            }
            else
            {
                return -1;
            }

        }



        public bool Equals(Card other)
        {
            return this.Face == other.Face && this.Suit == other.Suit;
        }

        override
        public String ToString()
        {
            char faceChar;
            char suitChar;

            if (!faceToChar.TryGetValue(this.Face, out faceChar))
                throw new Exception();

            if (!suitToChar.TryGetValue(this.Suit, out suitChar))
                throw new Exception();

            return "" + faceChar + suitChar;

        }

        public override int GetHashCode()
        {
            return (int) this.Face ^ (int) this.Suit;
        }
    }


}
