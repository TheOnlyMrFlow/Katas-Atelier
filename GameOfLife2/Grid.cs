using System;
using System.Text;

namespace GameOfLife2
{
    public class Grid
    {
        public Boolean[,] Cells;

        public int RowCount { get; set; }

        public int ColCount { get; set; }

        public Grid(string userInput)
        {
            var lines = userInput.Split(
                new[] { Environment.NewLine },
                StringSplitOptions.None);
            var boundaries = lines[0].Split(' ');
            RowCount = int.Parse(boundaries[0]);
            ColCount = int.Parse(boundaries[1]);

            Cells = new Boolean[RowCount, ColCount];

            for (int i = 0; i < RowCount; i++)
            {
                var inputRow = lines[i + 1].ToCharArray();
                for (int j = 0; j < ColCount; j++)
                    Cells[i,j] = CharToCell(inputRow[j]);
            }
        }

        private Grid() { }

        public Grid Clone()
        {
            var clone = new Grid();
            clone.RowCount = RowCount;
            clone.ColCount = ColCount;
            clone.Cells = this.Cells.Clone() as Boolean[,];
            return clone;
        }

        private Boolean CharToCell(char character)
        {
            return character == '*';
        }

        private char CellToChar(Boolean cell)
        {
            return cell == true ? '*' : '.';
        }

        public void SetDead(int row, int col)
        {
            Cells[row, col] = false;
        }

        public void SetLive(int row, int col)
        {
            Cells[row, col] = true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColCount; j++)
                {
                    sb.Append(CellToChar(Cells[i, j]));
                    sb.Append(' ');
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

    }
}
