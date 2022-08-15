using System;
using System.Windows.Input;

namespace Jidelnicek.Commands;

public class DeleteFoodCommand : ICommand
{
    private readonly Action<int> _func;

    public DeleteFoodCommand(Action<int> func)
    {
        _func = func;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        _func(Convert.ToInt32(parameter));
    }
}