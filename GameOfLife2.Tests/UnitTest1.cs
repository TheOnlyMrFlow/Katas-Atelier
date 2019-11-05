using FluentAssertions;
using System;
using Xunit;

namespace GameOfLife2.Tests
{
    public class UnitTest1
    {

        private string inputA =
@"4 8
........
....*...
...**...
........";

        private string inputB =
@"4 8
.*......
....*...
...**...
........";

        private string inputC =
@"4 8
........
...**...
...****.
....*...";

        //1. Any live cell with fewer than two live neighbours dies, as if caused by underpopulation.
        //2. Any live cell with more than three live neighbours dies, as if by overcrowding.
        //3. Any live cell with two or three live neighbours lives on to the next generation.
        //4. Any dead cell with exactly three live neighbours becomes a live cell.


        [Fact]
        public void input_is_parsed_correctly()
        {
            var grid = new Grid(inputA);
            grid.Cells.GetLength(0).Should().Be(4);
            grid.Cells.GetLength(1).Should().Be(8);
            grid.Cells[0, 0].Should().BeFalse();
            grid.Cells[1, 3].Should().BeFalse();
            grid.Cells[1, 4].Should().BeTrue();
        }

        [Fact]
        public void underpopulation_causes_death()
        {
            var grid = new Grid(inputB);
            var game = new Game(grid);
            var nextGen = game.PlayNextFrameAndReturnGrid();
            nextGen.Cells[0, 1].Should().BeFalse();

        }

        [Fact]
        public void overcrowding_causes_death()
        {
            var grid = new Grid(inputC);
            var game = new Game(grid);
            var nextGen = game.PlayNextFrameAndReturnGrid();
            nextGen.Cells[2, 5].Should().BeFalse();

        }

        [Fact]
        public void cell_lives_otherwise()
        {
            var grid = new Grid(inputC);
            var game = new Game(grid);
            var nextGen = game.PlayNextFrameAndReturnGrid();
            nextGen.Cells[3, 4].Should().BeTrue();
            nextGen.Cells[1, 3].Should().BeTrue();

        }

        [Fact]
        public void dead_cell_revives_if_3_live_neighbors()
        {
            var grid = new Grid(inputB);
            var game = new Game(grid);
            var nextGen = game.PlayNextFrameAndReturnGrid();
            nextGen.Cells[1, 3].Should().BeTrue();

        }

    }
}
