

namespace FloodPipeWPF.MVVM.Model.Game.GameField;

public class GameField
{
    private CellField _cellField;

    public GameField()
    {
        _cellField = new();
    }

    internal void ClearField()
    {
        _cellField.ClearField();
    }

    internal void InitializeEmptyField(int width, int height)
    {
        _cellField.CreateEmptyField(width, height);
    }
}
