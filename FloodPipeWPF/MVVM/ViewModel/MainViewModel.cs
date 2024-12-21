using FloodPipeWPF.Core;

namespace FloodPipeWPF.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    public RelayCommand HomeViewCommand { get; set; }
    public RelayCommand DiscoverViewCommand { get; set; }
    
    private object currentView;
    private HomeViewModel homeViewModel;
    private DiscoverViewModel discoverViewModel;
    
    public object CurrentView
    {
        get => currentView;
        set 
        {
            currentView = value;
            OnPropertyChanged();
        }
    }
    
    public MainViewModel()
    {
        homeViewModel = new HomeViewModel();
        discoverViewModel = new DiscoverViewModel();
        
        CurrentView = homeViewModel;
        HomeViewCommand = new RelayCommand(o => { CurrentView = homeViewModel; });
        DiscoverViewCommand = new RelayCommand(o => { CurrentView = discoverViewModel; });
    }
}