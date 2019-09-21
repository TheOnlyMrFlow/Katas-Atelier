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


        public static IEnumerable<object[]> GetCardSetsToCompare()
        {
            yield return new object[] {
                                    new Card ("Ah"),
                                    new Card ("Th"),
                                    Comparison.Greater
            };

            yield return new object[] {
                                    CardSet.BuildPair( new Card[] {
                                        new Card("2d"),
                                        new Card("2s")
                                    }),
                                    CardSet.BuildPair(new Card[] {
                                        new Card("4s"),
                                        new Card("4d")
                                    }),
                                    Comparison.Lesser };

            yield return new object[] {
                                    CardSet.BuildPair( new Card[] {
                                        new Card("5d"),
                                        new Card("5s")
                                    }),
                                    CardSet.BuildPair(new Card[] {
                                        new Card("4s"),
                                        new Card("4d")
                                    }),
                                    Comparison.Greater };

            yield return new object[] {
                                    CardSet.BuildPair( new Card[] {
                                        new Card("2d"),
                                        new Card("2s")
                                    }),
                                    CardSet.BuildPair(new Card[] {
                                        new Card("2h"),
                                        new Card("2c")
                                    }),
                                    Comparison.Equal };

            yield return new object[] {
                                    CardSet.BuildFullHouse( 
                                        CardSet.BuildThreeOfAKind( new Card[] {
                                            new Card("3h"),
                                            new Card("3c"),
                                            new Card("3d")
                                        }),
                                        CardSet.BuildPair( new Card[] {
                                            new Card("2h"),
                                            new Card("2c"),

                                        })
                                    ),
                                   CardSet.BuildFullHouse(
                                        CardSet.BuildThreeOfAKind( new Card[] {
                                            new Card("3h"),
                                            new Card("3c"),
                                            new Card("3s")
                                        }),
                                        CardSet.BuildPair( new Card[] {
                                            new Card("2h"),
                                            new Card("2c"),

                                        })
                                    ),
                                    Comparison.Equal };

            yield return new object[] {
                                    CardSet.BuildFullHouse(
                                        CardSet.BuildThreeOfAKind( new Card[] {
                                            new Card("3h"),
                                            new Card("3c"),
                                            new Card("3d")
                                        }),
                                        CardSet.BuildPair( new Card[] {
                                            new Card("2h"),
                                            new Card("2c"),

                                        })
                                    ),
                                   CardSet.BuildFullHouse(
                                        CardSet.BuildThreeOfAKind( new Card[] {
                                            new Card("4h"),
                                            new Card("4c"),
                                            new Card("4s")
                                        }),
                                        CardSet.BuildPair( new Card[] {
                                            new Card("2h"),
                                            new Card("2c"),

                                        })
                                    ),
                                    Comparison.Lesser };

            yield return new object[] {
                                    CardSet.BuildFullHouse(
                                        CardSet.BuildThreeOfAKind( new Card[] {
                                            new Card("5h"),
                                            new Card("5c"),
                                            new Card("5d")
                                        }),
                                        CardSet.BuildPair( new Card[] {
                                            new Card("2h"),
                                            new Card("2c"),

                                        })
                                    ),
                                   CardSet.BuildFullHouse(
                                        CardSet.BuildThreeOfAKind( new Card[] {
                                            new Card("3h"),
                                            new Card("3c"),
                                            new Card("3s")
                                        }),
                                        CardSet.BuildPair( new Card[] {
                                            new Card("2h"),
                                            new Card("2c"),

                                        })
                                    ),
                                    Comparison.Greater };

            yield return new object[] {
                                    CardSet.BuildFullHouse(
                                        CardSet.BuildThreeOfAKind( new Card[] {
                                            new Card("3h"),
                                            new Card("3c"),
                                            new Card("3d")
                                        }),
                                        CardSet.BuildPair( new Card[] {
                                            new Card("4h"),
                                            new Card("4c"),

                                        })
                                    ),
                                   CardSet.BuildFullHouse(
                                        CardSet.BuildThreeOfAKind( new Card[] {
                                            new Card("3h"),
                                            new Card("3c"),
                                            new Card("3s")
                                        }),
                                        CardSet.BuildPair( new Card[] {
                                            new Card("2h"),
                                            new Card("2c"),

                                        })
                                    ),
                                    Comparison.Greater };

            yield return new object[] {
                                    CardSet.BuildFullHouse(
                                        CardSet.BuildThreeOfAKind( new Card[] {
                                            new Card("3h"),
                                            new Card("3c"),
                                            new Card("3d")
                                        }),
                                        CardSet.BuildPair( new Card[] {
                                            new Card("4h"),
                                            new Card("4c"),

                                        })
                                    ),
                                   CardSet.BuildFullHouse(
                                        CardSet.BuildThreeOfAKind( new Card[] {
                                            new Card("3h"),
                                            new Card("3c"),
                                            new Card("3s")
                                        }),
                                        CardSet.BuildPair( new Card[] {
                                            new Card("5h"),
                                            new Card("5c"),

                                        })
                                    ),
                                    Comparison.Lesser };

            yield return new object[] {
                                    CardSet.BuildFlush( new Card[] {
                                        new Card("2d"),
                                        new Card("4d"),
                                        new Card("7d"),
                                        new Card("9d"),
                                        new Card("Ad")
                                    }),
                                    CardSet.BuildStraight(new Card[] {
                                        new Card("3s"),
                                        new Card("4d"),
                                        new Card("5h"),
                                        new Card("6d"),
                                        new Card("7d")
                                    }),
                                    Comparison.Greater };

            yield return new object[] {
                                    CardSet.BuildStraight( new Card[] {
                                        new Card("2s"),
                                        new Card("3d"),
                                        new Card("4h"),
                                        new Card("5d"),
                                        new Card("6s")
                                    }),
                                    CardSet.BuildStraight(new Card[] {
                                        new Card("3s"),
                                        new Card("4d"),
                                        new Card("5h"),
                                        new Card("6d"),
                                        new Card("7d")
                                    }),
                                    Comparison.Lesser };

            yield return new object[] {
                                    CardSet.BuildStraight( new Card[] {
                                        new Card("4s"),
                                        new Card("5d"),
                                        new Card("6h"),
                                        new Card("7c"),
                                        new Card("8s")
                                    }),
                                    CardSet.BuildStraight(new Card[] {
                                        new Card("3s"),
                                        new Card("4d"),
                                        new Card("5h"),
                                        new Card("6d"),
                                        new Card("7d")
                                    }),
                                    Comparison.Greater };

            yield return new object[] {
                                    CardSet.BuildStraight( new Card[] {
                                        new Card("3d"),
                                        new Card("4s"),
                                        new Card("5c"),
                                        new Card("6s"),
                                        new Card("7h")
                                    }),
                                    CardSet.BuildStraight(new Card[] {
                                        new Card("3s"),
                                        new Card("4d"),
                                        new Card("5h"),
                                        new Card("6d"),
                                        new Card("7d")
                                    }),
                                    Comparison.Equal };

            

        }

        [Theory]
        [MemberData(nameof(GetCardSetsToCompare))]
        public void cardset_comparison(CardSet a, CardSet b, Comparison comparison)
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
        public void cards_sorted()
        {
            Deal d = new Deal(new String[] { "Kh", "Td", "Ah", "Th", "8d" });

            for (int i = 0; i < d.Cards.Count - 1; i++)
                Assert.True(d.Cards[i].CompareTo(d.Cards[i + 1]) <= 0);
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


        //public static IEnumerable<object[]> GetHandsOfDeals()
        //{
        //    // high card
        //    yield return new object[] { "Td Jh 9s Kc Ah", new List<CardSet> {
        //        new CardSet(new HashSet<Card> { new Card("Ah") }, CardSet.Label.SingleCard)
        //    } };

        //    // one pair
        //    yield return new object[] { "Td Jh Ts Kc Ah", new List<CardSet> {
        //        new CardSet(new HashSet<Card> { new Card("Ah") }, CardSet.Label.SingleCard),
        //        new CardSet(new HashSet<Card> { new Card("Td"), new Card("Ts") }, CardSet.Label.Pair)
        //    } };

        //    //two pairs
        //    yield return new object[] { "Td Kh Ts Kc Ah", new List<CardSet> {
        //        new CardSet(new HashSet<Card> { new Card("Ah") }, CardSet.Label.SingleCard),
        //        new CardSet(new HashSet<Card> { new Card("Kh"), new Card("Kc") }, CardSet.Label.Pair),
        //        new CardSet(new HashSet<Card> { new Card("Td"), new Card("Ts") }, CardSet.Label.Pair)
        //    } };

        //    // one three of a kind
        //    yield return new object[] { "Td Jh Ts Kc Tc", new List<CardSet> {
        //        new CardSet(new HashSet<Card> { new Card("Kc") }, CardSet.Label.SingleCard),
        //        new CardSet(new HashSet<Card> { new Card("Td"), new Card("Ts"), new Card("Tc") }, CardSet.Label.ThreeOfAkind)
        //    } };

        //    // one fur of a kind
        //    yield return new object[] { "Td Jh Ts Th Tc", new List<CardSet> {
        //        new CardSet(new HashSet<Card> { new Card("Jh") }, CardSet.Label.SingleCard),
        //        new CardSet(new HashSet<Card> { new Card("Td"), new Card("Ts"), new Card("Tc"), new Card("Th") }, CardSet.Label.FourOfAkind)
        //    } };

        //    // one full house
        //    yield return new object[] { "Td Kh Ts Kc Kd", new List<CardSet> {
        //        new CardSet(new HashSet<Card> { new Card("Kh"), new Card("Kd"), new Card("Kc") }, CardSet.Label.SingleCard),
        //        new CardSet(new HashSet<Card> {
        //            new Card("Td"),
        //            new Card("Kh"),
        //            new Card("Ts"),
        //            new Card("Kc"),
        //            new Card("Kd")
        //        }, CardSet.Label.FullHouse),
        //    } };

        //    // one basic flush
        //    yield return new object[] { "Td Kd 2d 3d 7d", new List<CardSet> {
        //        new CardSet(new HashSet<Card> { new Card("Kd") }, CardSet.Label.SingleCard),
        //        new CardSet(new HashSet<Card> {
        //            new Card("Td"),
        //            new Card("2d"),
        //            new Card("3d"),
        //            new Card("Kd"),
        //            new Card("7d")
        //        }, CardSet.Label.Flush),
        //    } };


        //    // one basic straight
        //    yield return new object[] { "3s 4h 5c 6d 7c", new List<CardSet> {
        //        new CardSet(new HashSet<Card> { new Card("7c") }, CardSet.Label.SingleCard),
        //        new CardSet(new HashSet<Card> {
        //            new Card("3s"),
        //            new Card("5c"),
        //            new Card("6d"),
        //            new Card("4h"),
        //            new Card("7c")
        //        }, CardSet.Label.Straight),
        //    } };

        //    // one basic straight that starts with an ace
        //    yield return new object[] { "Ad 2c 3s 4h 5c", new List<CardSet> {
        //        new CardSet(new HashSet<Card> { new Card("Ad") }, CardSet.Label.SingleCard),
        //        new CardSet(new HashSet<Card> {
        //            new Card("3s"),
        //            new Card("5c"),
        //            new Card("1d"),
        //            new Card("4h"),
        //            new Card("2c")
        //        }, CardSet.Label.Straight),
        //    } };

        //    // one straight flush
        //    yield return new object[] { "3d 4d 5d 6d 7d", new List<CardSet> {
        //        new CardSet(new HashSet<Card> { new Card("7d") }, CardSet.Label.SingleCard),
        //        new CardSet(new HashSet<Card> {
        //            new Card("3d"),
        //            new Card("5d"),
        //            new Card("6d"),
        //            new Card("4d"),
        //            new Card("7d")
        //        }, CardSet.Label.StraightFlush),
        //    } };

        //    // one royal flush
        //    yield return new object[] { "Qd Ad Kd Td Jd", new List<CardSet> {
        //        new CardSet(new HashSet<Card> { new Card("Ad") }, CardSet.Label.SingleCard),
        //        new CardSet(new HashSet<Card> {
        //            new Card("Qd"),
        //            new Card("Kd"),
        //            new Card("Jd"),
        //            new Card("Ad"),
        //            new Card("Td")
        //        }, CardSet.Label.RoyalFlush),
        //    } };


        //}

        //[Theory]
        //[MemberData(nameof(GetHandsOfDeals))]
        //public void hands_of_deal (String dealAsString, List<CardSet> expectedHands)
        //{
        //    Deal d = new Deal(dealAsString.Split(' '));

        //    List<CardSet> computedHands = d.GetHand();

        //    Assert.Equal(expectedHands.Count, computedHands.Count);

        //    for (int i = 0; i < expectedHands.Count; i++)
        //    {
        //        Assert.Contains(computedHands[i], expectedHands);
        //    }

        //}

    }
}
