namespace FloodPipeWPF.MVVM.Model.Game;

using FloodPipeWPF.MVVM.Model.Persistence;
using System;

public class GameEngine
{
    public event Action<List<string>> ItemsUpdated;

    public List<string> Items { get; private set; }

    private readonly FileStorageHandler _fileStorageHandler = new();

    public GameEngine()
    {
        Items = _fileStorageHandler.LoadNameList();
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