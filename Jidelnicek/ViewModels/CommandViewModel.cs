using System;
using System.Windows.Input;

namespace Jidelnicek.ViewModels;

public class CommandViewModel : ICommand
{
    private readonly Predicate<object?>? _canExecuteAction;
    private readonly Action<object?> _executeAction;

    public CommandViewModel(Action<object?> executeAction)
    {
        _executeAction = executeAction;
        _canExecuteAction = null;
    }

    public CommandViewModel(Action<object?> executeAction, Predicate<object?> canExecuteAction)
    {
        _executeAction = executeAction;
        _canExecuteAction = canExecuteAction;
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object? parameter)
    {
        return _canExecuteAction?.Invoke(parameter) ?? true;
    }

    public void Execute(object? parameter)
    {
        _executeAction(parameter);
    }
}