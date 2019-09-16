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

        [Theory]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 5, 4, 6, 3, 0, 8, 3, 6, 2, 2, 5, 1, 0, 2 }, 66)]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 3, 4, 6, 3, 0, 0, 3, 6, 2, 6, 2, 1, 0, 9 }, 64)]
        public void common_rolls(int[] rolls, int expected)
        {
            Game game = new Game();

            
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




        [Theory]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 5, 5, 6, 3, 0, 8, 3, 6, 2, 2, 5, 1, 0, 1 }, 72)]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 5, 4, 6, 3, 0, 8, 3, 6, 2, 2, 5, 5, 0, 1 }, 69)]
        public void one_spare_at_middle(int [] rolls, int expected)
        {
            Game game = new Game();

            foreach (int roll in rolls)
            {
                game.Roll(roll);
            }

            Assert.Equal(expected, game.Score());

        }

        [Theory]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 5, 5, 6, 4, 2, 6, 3, 6, 2, 2, 5, 1, 0, 1 }, 75)]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 5, 3, 3, 4, 2, 6, 3, 6, 8, 2, 5, 5, 0, 1 }, 77)]
        public void two_spares_in_a_row_at_middle(int[] rolls, int expected)
        {
            Game game = new Game();


            foreach (int roll in rolls)
            {
                game.Roll(roll);
            }

            Assert.Equal(expected, game.Score());

        }

        [Theory]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 4, 3, 5, 2, 2, 6, 3, 6, 2, 2, 5, 1, 7, 3, 5 }, 75)]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 4, 3, 5, 2, 2, 6, 3, 6, 2, 2, 5, 5, 7, 3, 5 }, 86)]
        public void end_with_a_spare(int[] rolls, int expected)
        {
            Game game = new Game();

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



        [Theory]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 10, 6, 3, 0, 8, 3, 6, 2, 2, 5, 1, 0, 1 }, 75)]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 5, 4, 6, 3, 0, 8, 3, 6, 2, 2, 10, 0, 1 }, 70)]
        public void one_strike_at_middle(int[] rolls, int expected)
        {
            Game game = new Game();

            foreach (int roll in rolls)
            {
                game.Roll(roll);
            }

            Assert.Equal(expected, game.Score());
        }



        [Theory]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 4, 3, 6, 3, 0, 8, 3, 6, 10, 10, 5, 1 }, 99)]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 10, 10, 2, 6, 3, 6, 2, 2, 5, 1, 0, 1 }, 87)]
        public void two_strikes_in_a_row_at_middle(int[] rolls, int expected)
        {
            Game game = new Game();

            foreach (int roll in rolls)
            {
                game.Roll(roll);
            }

            Assert.Equal(expected, game.Score());
        }

        [Theory]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 4, 3, 5, 2, 2, 6, 3, 6, 2, 2, 5, 1, 10, 5, 3 }, 78)]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 4, 3, 5, 2, 2, 6, 3, 6, 2, 2, 10, 10, 5, 3 }, 97)]
        public void end_with_a_strike(int[] rolls, int expected)
        {
            Game game = new Game();

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
