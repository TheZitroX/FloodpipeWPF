using FloodPipeWPF.Core;

namespace FloodPipeWPF.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    public RelayCommand HomeViewCommand { get; }
    public RelayCommand GameViewCommand { get; }
    public RelayCommand QuitCommand { get; }

    private readonly HomeViewModel _homeViewModel = new();
    private readonly GameViewModel _gameViewModel = new();
    private object _currentView;

    public object CurrentView
    {
        get => _currentView;
        private set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    public MainViewModel()
    {
        _currentView = _gameViewModel;
        CurrentView = _currentView;

        HomeViewCommand = new RelayCommand(o => SwitchToHomeView());
        GameViewCommand = new RelayCommand(o => SwitchToGameView());
        QuitCommand = new RelayCommand(o => Shutdown());
    }

    private void SwitchToGameView()
    {
        CurrentView = _gameViewModel;
    }

    private void SwitchToHomeView()
    {
        CurrentView = _homeViewModel;
    }

    private void Shutdown()
    {
        _gameViewModel.Shutdown();
        System.Windows.Application.Current.Shutdown();
    }
}