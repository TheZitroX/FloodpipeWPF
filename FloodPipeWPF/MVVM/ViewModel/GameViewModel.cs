using System.Collections.ObjectModel;
using System.Windows.Input;
using FloodPipeWPF.Core;
using FloodPipeWPF.MVVM.Model.Game;

namespace FloodPipeWPF.MVVM.ViewModel;

public class GameViewModel : ObservableObject
{
    private readonly GameEngine _gameEngine;

    public ObservableCollection<string> Items { get; }

    private string _textBoxValue;

    public string TextBoxValue
    {
        get => _textBoxValue;
        set
        {
            _textBoxValue = value;
            OnPropertyChanged();
        }
    }

    public ICommand AddItemCommand { get; }

    public GameViewModel()
    {
        // Initialize GameEngine and Items collection
        _gameEngine = new GameEngine();
        Items = new ObservableCollection<string>(_gameEngine.Items);

        // Subscribe to updates from GameEngine
        _gameEngine.ItemsUpdated += OnItemsUpdated;

        // Initialize commands
        AddItemCommand = new RelayCommand(AddItem);
    }

    private void OnItemsUpdated(List<string> updatedItems)
    {
        // Synchronize ObservableCollection with the updated list
        Items.Clear();
        foreach (var item in updatedItems)
        {
            Items.Add(item);
        }
    }

    private void AddItem(object parameter)
    {
        // Add item from TextBoxValue and clear it
        if (string.IsNullOrWhiteSpace(TextBoxValue))
            return;

        _gameEngine.AddItem(TextBoxValue);
        TextBoxValue = string.Empty;
    }
}