namespace FloodPipeWPF.MVVM.Model.Game;

public class GameEngine
{
    public List<string> Items { get; private set; }
    
    public GameEngine()
    {
        Items = ["Item 1", "Item 2", "Item 3"];
    }
}