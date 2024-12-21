using FloodPipeWPF.Core;

namespace FloodPipeWPF.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    public RelayCommand HomeViewCommand { get; }
    public RelayCommand DiscoverViewCommand { get; }
    public RelayCommand QuitCommand { get; }
    
    private readonly HomeViewModel _homeViewModel = new HomeViewModel();
    private readonly DiscoverViewModel _discoverViewModel = new DiscoverViewModel();
    private object _currentView = new HomeViewModel();
    
    public object CurrentView
    {
        get => _currentView;
        set 
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }
    
    public MainViewModel()
    {
        CurrentView = _homeViewModel;
        
        HomeViewCommand = new RelayCommand(o => { CurrentView = _homeViewModel; });
        DiscoverViewCommand = new RelayCommand(o => { CurrentView = _discoverViewModel; });
        QuitCommand = new RelayCommand(o => { Shutdown(); });
    }

    private static void Shutdown()
    {
        System.Windows.Application.Current.Shutdown();
    }
}