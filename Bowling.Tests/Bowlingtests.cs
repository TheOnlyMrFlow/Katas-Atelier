using System;
using Xunit;
using System.Linq;

namespace Bowling.Tests
{
    public class BowlingTests
    {
        public Game MakeAGame(int[] rolls)
        {
            Game game = new Game();

            foreach (int roll in rolls)
                game.Roll(roll);

            return game;
        }

        [Theory]
        [InlineData(new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 0)]
        public void only_miss(int[] rolls, int expected)
        {
            Assert.Equal(expected, MakeAGame(rolls).Score());
        }

        [Theory]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 5, 4, 6, 3, 0, 8, 3, 6, 2, 2, 5, 1, 0, 2 }, 66)]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 3, 4, 6, 3, 0, 0, 3, 6, 2, 6, 2, 1, 0, 9 }, 64)]
        public void common_rolls(int[] rolls, int expected)
        {
            Assert.Equal(expected, MakeAGame(rolls).Score());
        }

        [Theory]
        [InlineData(new int[] { 6, 2, 4, 7, 2, 1, 5, 4, 6, 3, 0, 8, 3, 6, 2, 2, 5, 1, 0, 1 })]
        public void throws_error_if_impossible_frame(int[] rolls)
        {
            Assert.Throws<ImpossibleFrameException>(() => { MakeAGame(rolls); });
        }

        [Theory]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 5, 5, 6, 3, 0, 8, 3, 6, 2, 2, 5, 1, 0, 1 }, 72)]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 5, 4, 6, 3, 0, 8, 3, 6, 2, 2, 5, 5, 0, 1 }, 69)]
        public void one_spare_at_middle(int [] rolls, int expected)
        {
            Assert.Equal(expected, MakeAGame(rolls).Score());
        }

        [Theory]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 5, 5, 6, 4, 2, 6, 3, 6, 2, 2, 5, 1, 0, 1 }, 75)]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 5, 3, 3, 4, 2, 6, 3, 6, 8, 2, 5, 5, 0, 1 }, 77)]
        public void two_spares_in_a_row_at_middle(int[] rolls, int expected)
        {
            Assert.Equal(expected, MakeAGame(rolls).Score());
        }

        [Theory]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 4, 3, 5, 2, 2, 6, 3, 6, 2, 2, 5, 1, 7, 3, 5 }, 75)]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 4, 3, 5, 2, 2, 6, 3, 6, 2, 2, 5, 5, 7, 3, 5 }, 86)]
        public void end_with_a_spare(int[] rolls, int expected)
        {
            Assert.Equal(expected, MakeAGame(rolls).Score());
        }

        [Theory]
        [InlineData(new int[] { 5, 5, 3, 7, 4, 6, 0, 10, 3, 7, 4, 6, 5, 5, 1, 9, 2, 8, 2, 8, 9 }, 133)]
        public void only_spares(int[]rolls, int expected)
        {
            Assert.Equal(expected, MakeAGame(rolls).Score());
        }

        [Theory]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 10, 6, 3, 0, 8, 3, 6, 2, 2, 5, 1, 0, 1 }, 75)]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 5, 4, 6, 3, 0, 8, 3, 6, 2, 2, 10, 0, 1 }, 70)]
        public void one_strike_at_middle(int[] rolls, int expected)
        {
            Assert.Equal(expected, MakeAGame(rolls).Score());
        }

        [Theory]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 4, 3, 6, 3, 0, 8, 3, 6, 10, 10, 5, 1 }, 99)]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 10, 10, 2, 6, 3, 6, 2, 2, 5, 1, 0, 1 }, 87)]
        public void two_strikes_in_a_row_at_middle(int[] rolls, int expected)
        {
            Assert.Equal(expected, MakeAGame(rolls).Score());
        }

        [Theory]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 4, 3, 5, 2, 2, 6, 3, 6, 2, 2, 5, 1, 10, 5, 3 }, 78)]
        [InlineData(new int[] { 6, 2, 4, 4, 2, 1, 4, 3, 5, 2, 2, 6, 3, 6, 2, 2, 10, 10, 5, 3 }, 97)]
        public void end_with_a_strike(int[] rolls, int expected)
        {
            Assert.Equal(expected, MakeAGame(rolls).Score());
        }

        [Theory]
        [InlineData(new int[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, 300)]
        public void only_strikes(int[] rolls, int expected)
        {
            Assert.Equal(expected, MakeAGame(rolls).Score());
        }

        [Theory]
        [InlineData(new int[] { 10, 5, 5, 10, 10, 10, 3, 7, 10, 10, 0, 10, 10, 4, 6 }, 213)]
        public void only_spares_and_strikes(int[] rolls, int expected)
        {
            Assert.Equal(expected, MakeAGame(rolls).Score());
        }

    }
}
