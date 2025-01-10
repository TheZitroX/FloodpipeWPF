using System.Numerics;
using System.Windows;

namespace FloodPipeWPF.MVVM.Model.Game.GameField
{
    public class CellFunctions
    {
        public static void ClearField(List<List<Cell>> cells)
        {
            cells.Clear();
        }

        public static void CreateField(List<List<Cell>> cells, int width, int height)
        {
            ClearField(cells);

            for (int i = 0; i < height; i++)
            {
                cells.Add([]);
                for (int j = 0; j < width; j++)
                {
                    var pos = new Vector2(i, j);
                    cells[i].Add(new Cell(CellType.EMPTY, pos));
                }
            }
        }

        public static bool IsCellPositionValid(Cell cell, int width, int height)
        {
            return cell.Position.X >= 0 && cell.Position.X < width && cell.Position.Y >= 0 && cell.Position.Y < height;
        }

        public static bool IsCellConnectedToCell(Cell cell1, Cell cell2)
        {
            var cell1_connections = cell1.RelativeConnections;
            var cell2_connections = cell2.RelativeConnections;
            var cell1_pos = cell1.Position;
            var cell2_pos = cell2.Position;

            foreach (var connection1 in cell1_connections)
            {
                foreach (var connection2 in cell2_connections)
                {
                    if (cell1_pos + connection1 == cell2_pos && cell2_pos + connection2 == cell1_pos)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool IsCellEmpty(Cell cell)
        {
            return cell.CellState == CellState.EMPTY;
        }
    }
}
