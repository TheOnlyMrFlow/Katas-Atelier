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

        [Fact]
        public void string_to_card_set()
        {
            Assert.True(
                Card.StringToCardSet("2d 4d 7d 9d Ad").SetEquals(
                    new HashSet<Card>
                    {
                        new Card("2d"),
                        new Card("4d"),
                        new Card("7d"),
                        new Card("9d"),
                        new Card("Ad")
                    }
                ));
        }


        public static IEnumerable<object[]> GetCardsToCompare()
        {
            yield return new object[] { new Card(Face.Two, Suit.Clubs), new Card(Face.Ace, Suit.Diamonds), Comparison.Lesser };
            yield return new object[] { new Card(Face.Three, Suit.Spades), new Card(Face.Two, Suit.Diamonds), Comparison.Greater };
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

        public static IEnumerable<object[]> GetCardsStringAndObject()
        {
            yield return new object[] { "2c", new Card(Face.Two, Suit.Clubs) };
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
                new Card("2p");
            });

            Assert.Throws<Exception>(() =>
            {
                new Card("Id");
            });
        }


        public static IEnumerable<object[]> GetValuablesToCompare()
        {
            yield return new object[] {
                                    new Card ("Ah"),
                                    new Card ("Th"),
                                    Comparison.Greater
            };

            yield return new object[] {
                                    new Card ("2h"),
                                    new Card ("8d"),
                                    Comparison.Lesser
            };

            yield return new object[] {
                                    new Card ("Td"),
                                    new Card ("Th"),
                                    Comparison.Equal
            };

            yield return new object[] {
                                    new Pair(Card.StringToCardSet("3s 3h")),
                                    new Pair(Card.StringToCardSet("4s 4d")),
                                    Comparison.Lesser };

            yield return new object[] {
                                    new Pair(Card.StringToCardSet("5d 5s")),
                                    new Pair(Card.StringToCardSet("4s 4h")),
                                    Comparison.Greater };

            yield return new object[] {
                                    new Pair(Card.StringToCardSet("3s 3h")),
                                    new Pair(Card.StringToCardSet("3c 3d")),
                                    Comparison.Equal };

            yield return new object[] {
                                    new TwoPairs(
                                        new Pair(Card.StringToCardSet("3s 3h")),
                                        new Pair(Card.StringToCardSet("4s 4h"))
                                    ),
                                    new Quadruple(Card.StringToCardSet("3c 3d 3h 3s")),
                                    Comparison.Lesser };

            yield return new object[] {
                                    new FullHouse(
                                        new Triple(Card.StringToCardSet("3s 3h 3d")),
                                        new Pair(Card.StringToCardSet("2h 2c"))
                                    ),
                                    new FullHouse(
                                        new Triple(Card.StringToCardSet("3s 3h 3d")),
                                        new Pair(Card.StringToCardSet("2d 2s"))
                                    ),
                                    Comparison.Equal
            };

            yield return new object[] {
                                    new FullHouse(
                                        new Triple(Card.StringToCardSet("3s 3h 3d")),
                                        new Pair(Card.StringToCardSet("2h 2c"))
                                    ),
                                    new FullHouse(
                                        new Triple(Card.StringToCardSet("4s 4h 4d")),
                                        new Pair(Card.StringToCardSet("2d 2s"))
                                    ),
                                    Comparison.Lesser
            };

            yield return new object[] {
                                    new FullHouse(
                                        new Triple(Card.StringToCardSet("5s 5h 5d")),
                                        new Pair(Card.StringToCardSet("2h 2c"))
                                    ),
                                    new FullHouse(
                                        new Triple(Card.StringToCardSet("3s 3h 3d")),
                                        new Pair(Card.StringToCardSet("2d 2s"))
                                    ),
                                    Comparison.Greater
            };

            yield return new object[] {
                                    new FullHouse(
                                        new Triple(Card.StringToCardSet("3s 3h 3d")),
                                        new Pair(Card.StringToCardSet("4h 4c"))
                                    ),
                                    new FullHouse(
                                        new Triple(Card.StringToCardSet("3s 3h 3d")),
                                        new Pair(Card.StringToCardSet("2d 2s"))
                                    ),
                                    Comparison.Greater
            };

            yield return new object[] {
                                    new FullHouse(
                                        new Triple(Card.StringToCardSet("3s 3h 3d")),
                                        new Pair(Card.StringToCardSet("4h 4c"))
                                    ),
                                    new FullHouse(
                                        new Triple(Card.StringToCardSet("3s 3h 3d")),
                                        new Pair(Card.StringToCardSet("5d 5s"))
                                    ),
                                    Comparison.Lesser
            };

            yield return new object[] {
                                   new Flush(Card.StringToCardSet("2d 4d 7d 9d Ad")),
                                   new Straight(Card.StringToCardSet("3s 4d 5c 6s 7h")),
                                    Comparison.Greater
            };

            yield return new object[] {
                                   new Straight(Card.StringToCardSet("2d 3c 4d 5s 6h")),
                                   new Straight(Card.StringToCardSet("3s 4d 5c 6s 7h")),
                                    Comparison.Lesser
            };


            yield return new object[] {
                                   new Straight(Card.StringToCardSet("4d 5c 6d 7s 8h")),
                                   new Straight(Card.StringToCardSet("3s 4d 5c 6s 7h")),
                                    Comparison.Greater
            };

            yield return new object[] {
                                   new Straight(Card.StringToCardSet("3d 4c 5d 6s 7h")),
                                   new Straight(Card.StringToCardSet("3s 4d 5c 6s 7h")),
                                    Comparison.Equal
            };

            yield return new object[] {
                                   new RoyalFlush(Card.StringToCardSet("Kd Qd Ad Jd Td")),
                                   new RoyalFlush(Card.StringToCardSet("Ks Qs As Js Ts")),
                                    Comparison.Equal
            };



        }

        [Theory]
        [MemberData(nameof(GetValuablesToCompare))]
        public void compare_two_valuables(IValuable a, IValuable b, Comparison comparison)
        {
            switch (comparison)
            {
                case Comparison.Greater:
                    Assert.True(a.CompareTo(b) > 0);
                    Assert.True(b.CompareTo(a) < 0);
                    break;
                case Comparison.Lesser:
                    Assert.True(a.CompareTo(b) < 0);
                    Assert.True(b.CompareTo(a) > 0);
                    break;
                case Comparison.Equal:
                    Assert.True(a.CompareTo(b) == 0);
                    break;

            }
        }

        public static IEnumerable<object[]> GetHandOfPlayer()
        {

            yield return new object[] {
                                    "Tc 9s Ks 8h 9d 3c 4d",
                                    new Hand(new IValuable[] {
                                        new Pair(Card.StringToCardSet("9s 9d")),
                                        new Card("8h"),
                                        new Card("Ks"),
                                        new Card("Tc")
                                    }),
                                    Card.StringToCardSet("4d 3c")
            };

            yield return new object[] {
                                    "Kc 9s Ks Kd 9d 3c 6d",
                                    new Hand(new IValuable[] {
                                        new FullHouse(
                                            new Triple(Card.StringToCardSet("Kc Ks Kd")),
                                            new Pair(Card.StringToCardSet("9s 9d"))
                                        )
                                    }),
                                    Card.StringToCardSet("3c 6d")
            };

            yield return new object[] {
                                    "4d 2d Ks Kd 9d 3d 6d",
                                    new Hand(new IValuable[] {
                                        new Flush(Card.StringToCardSet("9d 3d 6d Kd 4d"))
                                    }),
                                    Card.StringToCardSet("2d Ks")
            };

            yield return new object[] {
                                    "Jd Qd Td 3c Kd 3d 9d",
                                    new Hand(new IValuable[] {
                                        new StraightFlush(Card.StringToCardSet("9d Td Kd Jd Qd"))
                                    }),
                                    Card.StringToCardSet("3c 3d")
            };

            yield return new object[] {
                                    "Jd Qd Td Ad Kd 3c 3d",
                                    new Hand(new IValuable[] {
                                        new RoyalFlush(Card.StringToCardSet("Ad Td Kd Jd Qd"))
                                    }),
                                    Card.StringToCardSet("3c 3d")
            };


        }

        [Theory]
        [MemberData(nameof(GetHandOfPlayer))]
        public void hand_of_player(String dealAsString, Hand expectedHand, ISet<Card> expectedUnused)
        {
            Player d = new Player(dealAsString.Split(' '));


            Hand computedHands = d.Hand;
            ISet<Card> unused = d.UnusedCards;

            Assert.True(computedHands.CompareTo(expectedHand) == 0);
            Assert.True(unused.SetEquals(expectedUnused));

        }

}
}
