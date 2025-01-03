using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloodPipeWPF.MVVM.Model.Game.GameField;

public class CellField
{
    private List<List<Cell>> _cells;

    public CellField()
    {
        _cells = new();
    }

    internal void CreateEmptyField(int width, int height)
    {
        CellFunctions.CreateField(_cells, width, height);
    }
}

