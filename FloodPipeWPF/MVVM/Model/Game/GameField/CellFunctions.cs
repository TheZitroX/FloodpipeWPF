using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloodPipeWPF.MVVM.Model.Game.GameField
{
    internal class CellFunctions
    {
        internal static void ClearField(List<List<Cell>> cells)
        {
            for(int i = 0; i < cells.Count; i++)
            {
                for (int j = 0; j < cells[i].Count; j++)
                {
                    cells[i].Clear();
                }
            }

            cells.Clear();
        }

        internal static void CreateField(List<List<Cell>> cells, int width, int height)
        {
            ClearField(cells);

            for (int i = 0; i < height; i++)
            {
                cells.Add([]);
                for (int j = 0; j < width; j++)
                {
                    cells[i].Add(new Cell(CellType.EMPTY));
                }
            }
        }
    }
}
