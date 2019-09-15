using System;
using Xunit;
using System.Linq;

namespace Bowling.Tests
{
    public class BowlingTests
    {
        [Fact]
        public void only_miss()
        {
            Game game = new Game();

            for (int i = 0; i < 20; i++)
            {
                game.Roll(0);
            }

            Assert.Equal(0, game.Score());

        }

        [Fact]
        public void common_rolls()
        {
            Game game = new Game();

            int[] rolls = { 6, 2, 4, 4, 2, 1, 5, 4, 6, 3, 0, 8, 3, 6, 2, 2, 5, 1, 0, 2 };
            int expected = rolls.Sum();

            foreach (int roll in rolls)
            {
                game.Roll(roll);
            }

            Assert.Equal(expected, game.Score());

        }

        
        [Fact]
        public void throws_error_if_impossible_frame()
        {
            Game game = new Game();

            // this sequence of rolls is impossible since the 3rd and 4th rolls sum up to 11.
            int[] rolls = { 6, 2, 4, 7, 2, 1, 5, 4, 6, 3, 0, 8, 3, 6, 2, 2, 5, 1, 0, 1 };
            int expected = rolls.Sum();

            Assert.Throws<ImpossibleFrameException>(() =>
            {
                foreach (int roll in rolls)
                {
                    game.Roll(roll);
                }

            });

        }




        [Fact]
        public void one_spare_at_middle()
        {
            Game game = new Game();

            int[] rolls = { 6, 2, 4, 4, 2, 1, 5, 5, 6, 3, 0, 8, 3, 6, 2, 2, 5, 1, 0, 1 };
            int expected = 72;

            foreach (int roll in rolls)
            {
                game.Roll(roll);
            }

            Assert.Equal(expected, game.Score());

        }

        [Fact]
        public void two_spares_in_a_row_at_middle()
        {
            Game game = new Game();

            int[] rolls = { 6, 2, 4, 4, 2, 1, 5, 5, 6, 4, 2, 6, 3, 6, 2, 2, 5, 1, 0, 1 };
            int expected = 75;

            foreach (int roll in rolls)
            {
                game.Roll(roll);
            }

            Assert.Equal(expected, game.Score());

        }

        [Fact]
        public void end_with_a_spare()
        {
            Game game = new Game();

            int[] rolls = { 6, 2, 4, 4, 2, 1, 4, 3, 5, 2, 2, 6, 3, 6, 2, 2, 5, 1, 7, 3, 5 };
            int expected = 75;

            foreach (int roll in rolls)
            {
                game.Roll(roll);
            }

            Assert.Equal(expected, game.Score());
        }


        [Fact]
        public void only_spares()
        {
            Game game = new Game();

            int[] rolls = { 5, 5, 3, 7, 4, 6, 0, 10, 3, 7, 4, 6, 5, 5, 1, 9, 2, 8, 2, 8, 9};
            int expected = 133;

            foreach (int roll in rolls)
            {
                game.Roll(roll);
            }

            Assert.Equal(expected, game.Score());
        }



        [Fact]
        public void one_strike_at_middle()
        {
            Game game = new Game();

            int[] rolls = { 6, 2, 4, 4, 2, 1, 10, 6, 3, 0, 8, 3, 6, 2, 2, 5, 1, 0, 1 };
            int expected = 75;

            foreach (int roll in rolls)
            {
                game.Roll(roll);
            }

            Assert.Equal(expected, game.Score());
        }



        [Fact]
        public void two_strikes_in_a_row_at_middle()
        {
            Game game = new Game();

            int[] rolls = { 6, 2, 4, 4, 2, 1, 10, 10, 2, 6, 3, 6, 2, 2, 5, 1, 0, 1 };
            int expected = 87;

            foreach (int roll in rolls)
            {
                game.Roll(roll);
            }

            Assert.Equal(expected, game.Score());
        }

        [Fact]
        public void end_with_a_strike()
        {
            Game game = new Game();

            int[] rolls = { 6, 2, 4, 4, 2, 1, 4, 3, 5, 2, 2, 6, 3, 6, 2, 2, 5, 1, 10, 5, 3 };
            int expected = 78;

            foreach (int roll in rolls)
            {
                game.Roll(roll);
            }

            Assert.Equal(expected, game.Score());
        }

        [Fact]
        public void only_strikes()
        {
            Game game = new Game();

            int[] rolls = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10};
            int expected = 300;

            foreach (int roll in rolls)
            {
                game.Roll(roll);
            }

            Assert.Equal(expected, game.Score());
        }

        [Fact]
        public void only_spares_and_strikes()
        {
            Game game = new Game();

            int[] rolls = { 10, 5, 5, 10, 10, 10, 3, 7, 10, 10, 0, 10, 10, 4, 6 };
            int expected = 213;

            foreach (int roll in rolls)
            {
                game.Roll(roll);
            }

            Assert.Equal(expected, game.Score());
        }



    }
}
