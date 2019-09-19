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

        [Fact]
        public void card_equality()
        {
            Card a = new Card("Jh");
            Card b = new Card("Jh");
            Assert.Equal(a, b);
        }

        [Fact]
        public void hand_equality()
        {
            Hand a = new Hand(new HashSet<Card> { new Card("Jh"), new Card("Jc") }, Hand.HandLabel.Pair);
            Hand b = new Hand(new HashSet<Card> { new Card("Jh"), new Card("Jc") }, Hand.HandLabel.Pair);
            Assert.Equal(a, b);
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
            // high card
            yield return new object[] { "Td Jh 9s Kc Ah", new List<Hand> {
                new Hand(new HashSet<Card> { new Card("Ah") }, Hand.HandLabel.HighCard)
            } };

            // one pair
            yield return new object[] { "Td Jh Ts Kc Ah", new List<Hand> {
                new Hand(new HashSet<Card> { new Card("Ah") }, Hand.HandLabel.HighCard),
                new Hand(new HashSet<Card> { new Card("Td"), new Card("Ts") }, Hand.HandLabel.Pair)
            } };

            //two pairs
            yield return new object[] { "Td Kh Ts Kc Ah", new List<Hand> {
                new Hand(new HashSet<Card> { new Card("Ah") }, Hand.HandLabel.HighCard),
                new Hand(new HashSet<Card> { new Card("Kh"), new Card("Kc") }, Hand.HandLabel.Pair),
                new Hand(new HashSet<Card> { new Card("Td"), new Card("Ts") }, Hand.HandLabel.Pair)
            } };

            // one three of a kind
            yield return new object[] { "Td Jh Ts Kc Tc", new List<Hand> {
                new Hand(new HashSet<Card> { new Card("Kc") }, Hand.HandLabel.HighCard),
                new Hand(new HashSet<Card> { new Card("Td"), new Card("Ts"), new Card("Tc") }, Hand.HandLabel.ThreeOfAkind)
            } };

            // one fur of a kind
            yield return new object[] { "Td Jh Ts Th Tc", new List<Hand> {
                new Hand(new HashSet<Card> { new Card("Jh") }, Hand.HandLabel.HighCard),
                new Hand(new HashSet<Card> { new Card("Td"), new Card("Ts"), new Card("Tc"), new Card("Th") }, Hand.HandLabel.FourOfAkind)
            } };

            // one full house
            yield return new object[] { "Td Kh Ts Kc Kd", new List<Hand> {
                new Hand(new HashSet<Card> { new Card("Kh"), new Card("Kd"), new Card("Kc") }, Hand.HandLabel.HighCard),
                new Hand(new HashSet<Card> {
                    new Card("Td"),
                    new Card("Kh"),
                    new Card("Ts"),
                    new Card("Kc"),
                    new Card("Kd")
                }, Hand.HandLabel.FullHouse),
            } };
        }

        [Theory]
        [MemberData(nameof(GetHandsOfDeals))]
        public void hands_of_deal (String dealAsString, List<Hand> expectedHands)
        {
            Deal d = new Deal(dealAsString.Split(' '));

            List<Hand> computedHands = d.GetHands();

            Assert.Equal(expectedHands.Count, computedHands.Count);

            for (int i = 0; i < expectedHands.Count; i++)
            {
                Assert.Contains(computedHands[i], expectedHands);
            }

        }

    }
}
