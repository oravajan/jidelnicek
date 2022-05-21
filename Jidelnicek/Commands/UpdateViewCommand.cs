using System;
using System.Windows.Input;
using Jidelnicek.ViewModels;

namespace Jidelnicek.Commands;

public class UpdateViewCommand : ICommand
{
    private readonly MainViewModel _viewModel;

    public UpdateViewCommand(MainViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        _viewModel.CurrentViewModel = parameter?.ToString() switch
        {
            "AddFood" => new AddFoodViewModel(),
            "HistoryFood" => new HistoryFoodViewModel(),
            "ListFood" => new ListFoodViewModel(),
            _ => _viewModel.CurrentViewModel
        };
    }
}