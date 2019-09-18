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

        public static IEnumerable<object[]> GetCardsStringAndObject()
        {
            yield return new object[] { "1c", new Card(Face.One, Suit.Clubs) };
            yield return new object[] { "Ad", new Card(Face.Ace, Suit.Diamonds) };
            yield return new object[] { "Jh", new Card(Face.Jack, Suit.Hearts) };
            
        }

        [Theory]
        [MemberData(nameof(GetCardsStringAndObject))]
        public void card_to_string_vice_versa(String cardString, Card cardObject)
        {
            Assert.Equal(cardObject, new Card(cardString));
            Assert.Equal(cardString, cardObject.ToString());
        }

        [Fact]
        public void incorrect_card_string_throw_error()
        {
            Assert.Throws<Exception>(() =>
            {
                new Card("1p");
            });

            Assert.Throws<Exception>(() =>
            {
                new Card("Id");
            });
        }


        public static IEnumerable<object[]> GetHandsOfDeals()
        {
            yield return new object[] { "Td Jh 9s Kc Ah", new List<Hand> {
                new Hand(Face.Ace, Hand.HandLabel.HighCard)
            } };
        }

        [Theory]
        [MemberData(nameof(GetHandsOfDeals))]
        public void hands_of_deal (String dealAsString, List<Hand> expectedHands)
        {
            Deal d = new Deal(dealAsString.Split(' '));
            Assert.Equal(expectedHands, d.GetHands());
        }

    }
}
