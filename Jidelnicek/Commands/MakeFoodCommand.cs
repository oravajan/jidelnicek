using System;
using System.Windows.Input;

namespace Jidelnicek.Commands;

public class MakeFoodCommand : ICommand
{
    private readonly Action<int> _func;

    public MakeFoodCommand(Action<int> func)
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