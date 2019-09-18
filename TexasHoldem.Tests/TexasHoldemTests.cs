using System;
using System.Collections.Generic;
using Xunit;

namespace TexasHoldem.Tests
{
    public class TexasHoldemTests
    {
        public enum Comparison
        {
            Greater,
            Lesser,
            Equal
        }
        public static IEnumerable<object[]> GetCardsToCompare()
        {
            yield return new object[] { new Card(Face.One, Suit.Clubs), new Card(Face.Ace, Suit.Diamonds), Comparison.Lesser };
            yield return new object[] { new Card(Face.Two, Suit.Spades), new Card(Face.One, Suit.Diamonds), Comparison.Greater };
            yield return new object[] { new Card(Face.Jack, Suit.Spades), new Card(Face.Jack, Suit.Clubs), Comparison.Equal };
        }

        //[Theory]
        //[MemberData(nameof(GetNumbers))]
        //public void AllNumbers_AreOdd_WithMemberData(C a, int b, int c, int d)
        //{
        //    Assert.True(a == 5);
        //    Assert.True(b == 1);
        //    Assert.True(c == 3);
        //    Assert.True(d == 9);
        //}


        [Theory]
        [MemberData(nameof(GetCardsToCompare))]
        public void compare_two_cards(Card a, Card b, Comparison comparison)
        {
            switch (comparison)
            {
                case Comparison.Greater:
                    Assert.True(a.CompareTo(b) > 0);
                    break;
                case Comparison.Lesser:
                    Assert.True(a.CompareTo(b) < 0);
                    break;
                case Comparison.Equal:
                    Assert.True(a.CompareTo(b) == 0);
                    break;

            }

        }
    }
}
