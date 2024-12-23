using System.Collections.ObjectModel;
using FloodPipeWPF.Core;
using FloodPipeWPF.MVVM.Model.Game;

namespace FloodPipeWPF.MVVM.ViewModel;

public class GameViewModel : ObservableObject
{
    private readonly GameEngine _gameEngine = new();
    
    public ObservableCollection<string> Items { get; }
    
    public GameViewModel()
    {
        Items = new(_gameEngine.Items);
    }
}