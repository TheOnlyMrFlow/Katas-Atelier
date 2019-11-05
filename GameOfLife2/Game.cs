using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife2
{
    public class Game
    {
        private Grid temporaryGrid;
        public Grid Grid { get; private set; }
        public Game(Grid initialGrid)
        {
            this.Grid = initialGrid;
        }

        public Grid PlayNextFrameAndReturnGrid()
        {
            temporaryGrid = Grid.Clone();

            for (int i = 0; i < Grid.RowCount; i++)
                for (int j = 0; j < Grid.ColCount; j++)
                    ApplyRulesOnTempGridForCell(i, j);

            Grid = temporaryGrid;
            return Grid;
        }

        public void ApplyRulesOnTempGridForCell(int row, int col)
        {
            var selfState = Grid.Cells[row, col];
            var neighbors = GetNeighbours(row, col);
            var liveNeighborsCount = neighbors.Where(x => x == true).Count();

            if (selfState == true)
            {
                if (liveNeighborsCount < 2)
                    temporaryGrid.SetDead(row, col);

                if (liveNeighborsCount > 3)
                    temporaryGrid.SetDead(row, col);
            }

            else if (liveNeighborsCount == 3)
                temporaryGrid.SetLive(row, col);

        }

        private IEnumerable<bool> GetNeighbours(int row, int col)
        {
            var isNotOnRightEdge = col < Grid.ColCount - 1;
            var isNotOnLeftEdge = col != 0;
            var isNotOnTopEdge = row != 0;
            var isNotOnBottomEdge = row < Grid.RowCount - 1;

            if (isNotOnTopEdge)
                yield return Grid.Cells[row - 1, col];

            if (isNotOnTopEdge && isNotOnLeftEdge)
                yield return Grid.Cells[row - 1, col - 1];

            if (isNotOnTopEdge && isNotOnRightEdge)
                yield return Grid.Cells[row - 1, col + 1];

            if (isNotOnBottomEdge)
                yield return Grid.Cells[row + 1, col];

            if (isNotOnBottomEdge && isNotOnLeftEdge)
                yield return Grid.Cells[row + 1, col - 1];

            if (isNotOnBottomEdge && isNotOnRightEdge)
                yield return Grid.Cells[row + 1, col + 1];

            if (isNotOnLeftEdge)
                yield return Grid.Cells[row, col - 1];

            if (isNotOnRightEdge)
                yield return Grid.Cells[row, col + 1];           
        }
    }
}
