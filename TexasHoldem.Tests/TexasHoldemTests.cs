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


        public static IEnumerable<object[]> GetCardSetsToCompare()
        {
            yield return new object[] {
                                    new Card ("Ah") as CardSet,
                                    new Card ("Th") as CardSet,
                                    Comparison.Greater
            };

            yield return new object[] {
                                    new Card ("2h") as CardSet,
                                    new Card ("8d") as CardSet,
                                    Comparison.Lesser
            };

            yield return new object[] {
                                    new Card ("Td") as CardSet,
                                    new Card ("Th") as CardSet,
                                    Comparison.Equal
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

            yield return new object[] {
                                    CardSet.BuildRoyalFlush( new Card[] {
                                        new Card("Kd"),
                                        new Card("Qd"),
                                        new Card("Ad"),
                                        new Card("Jd"),
                                        new Card("Td")
                                    }),
                                    CardSet.BuildRoyalFlush(new Card[] {
                                        new Card("Ac"),
                                        new Card("Kc"),
                                        new Card("Qc"),
                                        new Card("Jc"),
                                        new Card("Tc")
                                    }),
                                    Comparison.Equal
            };



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




        public static IEnumerable<object[]> GetHandOfDeals()
        {

            yield return new object[] {
                                    "Tc 9s Ks 8h 9d 3c 4d",
                                    new Hand(
                                        new CardSet[] {
                                            CardSet.BuildPair(
                                                new Card[]
                                                {
                                                    new Card("9s"),
                                                    new Card("9d")
                                                }
                                            ),
                                            new Card("8h"),
                                            new Card("Ks"),
                                            new Card("Tc")
                                        }
                                    ),
                                    new HashSet<Card>
                                    {
                                        new Card("4d"),
                                        new Card("3c")
                                    }
            };

            yield return new object[] {
                                    "Kc 9s Ks Kd 9d 3c 6d",
                                    new Hand(
                                        new CardSet[] {
                                            CardSet.BuildFullHouse(
                                                CardSet.BuildThreeOfAKind(
                                                    new Card[]
                                                    {
                                                        new Card("Kc"),
                                                        new Card("Ks"),
                                                        new Card("Kd")
                                                    }
                                                ),
                                                CardSet.BuildPair(
                                                    new Card[]
                                                    {
                                                        new Card("9s"),
                                                        new Card("9d")
                                                    }
                                                )
                                            )
                                        }
                                    ),
                                    new HashSet<Card>()
            };

            yield return new object[] {
                                    "Jd Qd Td 3c Kd 3d 9d",
                                    new Hand(
                                        new CardSet[] {
                                            CardSet.BuildStraightFlush(
                                                new Card[]
                                                    {
                                                        new Card("9d"),
                                                        new Card("Td"),
                                                        new Card("Kd"),
                                                        new Card("Jd"),
                                                        new Card("Qd")
                                                    }
                                            )
                                        }
                                    ),
                                    new HashSet<Card>()
            };

            yield return new object[] {
                                    "Jd Qd Td Ad Kd 3c 3d",
                                    new Hand(
                                        new CardSet[] {
                                            CardSet.BuildRoyalFlush(
                                                new Card[]
                                                    {
                                                        new Card("Ad"),
                                                        new Card("Td"),
                                                        new Card("Kd"),
                                                        new Card("Jd"),
                                                        new Card("Qd")
                                                    }
                                            )
                                        }
                                    ),
                                    new HashSet<Card>()
            };

        }

        [Theory]
        [MemberData(nameof(GetHandOfDeals))]
        public void hands_of_deal(String dealAsString, Hand expectedHand, ISet<Card> expectedUnused)
        {
            Player d = new Player(dealAsString.Split(' '));

            HashSet<Card> unused;

            Hand computedHands = d.GetHand(out unused);

            Assert.True(computedHands.CompareTo(expectedHand) == 0);
            Assert.True(unused.SetEquals(expectedUnused));

        }

}
}
