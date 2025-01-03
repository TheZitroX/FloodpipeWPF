using System;

using FloodPipeWPF.MVVM.Model.Persistence;
using GF = FloodPipeWPF.MVVM.Model.Game.GameField;

namespace FloodPipeWPF.MVVM.Model.Game;

public class GameEngine
{
    public event Action<List<string>> ItemsUpdated;

    public List<string> Items { get; private set; }

    private readonly FileStorageHandler _fileStorageHandler = new();
    private readonly GF.GameField _gameField = new();

    public GameEngine()
    {
        Items = _fileStorageHandler.LoadNameList();

        _gameField.InitializeEmptyField();
    }

    public void AddItem(string item)
    {
        Items.Add(item);
        ItemsUpdated?.Invoke(Items);

        _fileStorageHandler.AddName(item);
    }

    public void RemoveItem(string item)
    {
        Items.Remove(item);
        ItemsUpdated?.Invoke(Items);
    }
}