using System.Windows.Input;
using Jidelnicek.Commands;

namespace Jidelnicek.ViewModels;

public class MainViewModel : ViewModelBase
{
    private ViewModelBase _currentViewModel;

    public MainViewModel()
    {
        _currentViewModel = new ListFoodViewModel();
        UpdateViewCommand = new UpdateViewCommand(this);
    }

    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }

    public ICommand UpdateViewCommand { get; }
}