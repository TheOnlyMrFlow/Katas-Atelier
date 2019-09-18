using System;

namespace TexasHoldem
{
    public class Game
    {
        public enum Hands
        {
            None = 0,
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

        public Game() { }

        public string[] Result()
        {
            return new string[0];
        }

    }
}
