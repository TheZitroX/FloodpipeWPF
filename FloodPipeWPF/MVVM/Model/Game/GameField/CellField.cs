namespace FloodPipeWPF.MVVM.Model.Game.GameField;

public class CellField
{
    private List<List<Cell>> _cells;

    public CellField()
    {
        _cells = new();
    }

    internal void ClearField()
    {
        CellFunctions.ClearField(_cells);
    }

    internal void CreateEmptyField(int width, int height)
    {
        CellFunctions.CreateField(_cells, width, height);
    }
}

