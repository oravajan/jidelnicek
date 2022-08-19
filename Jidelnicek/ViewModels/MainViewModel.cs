using System.Windows.Input;

namespace Jidelnicek.ViewModels;

public class MainViewModel : BaseViewModel
{
    public BaseViewModel CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
    public ICommand UpdateViewCommand { get; }
    
    private BaseViewModel _currentViewModel;

    public MainViewModel()
    {
        _currentViewModel = new ListFoodViewModel();
        UpdateViewCommand = new CommandViewModel(UpdateView);
    }

    private void UpdateView(object? obj)
    {
        CurrentViewModel = obj?.ToString() switch
        {
            "AddFood" => new AddFoodViewModel(),
            "HistoryFood" => new HistoryFoodViewModel(),
            "ListFood" => new ListFoodViewModel(),
            _ => CurrentViewModel
        };
    }
}