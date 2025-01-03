using Accessibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloodPipeWPF.MVVM.Model.Game.GameField
{
    public enum CellType
    {
        EMPTY,
        FULL
    }

    public class Cell
    {
        private CellType _type;

        public Cell(CellType cellType)
        {
            _type = cellType;
        }

        public CellType Type
        {
            get => _type;
            set => _type = value;
        }
    }
}
