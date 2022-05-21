using System;
using System.Windows.Input;
using Jidelnicek.ViewModels;

namespace Jidelnicek.Commands;

public class AddFoodCommand : ICommand
{
    private readonly AddFoodViewModel _viewModel;

    public AddFoodCommand(AddFoodViewModel viewModel)
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
        _viewModel.AddFood();
    }
}