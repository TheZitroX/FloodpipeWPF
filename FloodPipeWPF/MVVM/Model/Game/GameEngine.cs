namespace FloodPipeWPF.MVVM.Model.Game;

using FloodPipeWPF.MVVM.Model.Persistence;

public class GameEngine
{
    public List<string> Items { get; private set; }
    
    private readonly FileStorageHandler _fileStorageHandler = new();
    
    public GameEngine()
    {
        Items = ["Item 1", "Item 2", "Item 3"];
        
        _fileStorageHandler.SaveNameList(Items);
    }
}