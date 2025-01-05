using System.Numerics;

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
    }
}
