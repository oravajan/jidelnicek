using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Jidelnicek.DataMappers;
using Jidelnicek.Models;

namespace Jidelnicek.ViewModels;

public class EditFoodViewModel : BaseViewModel
{
    public class MyDateWrapper
    {
        public DateTime Date { get; set; }

        public MyDateWrapper(DateTime date)
        {
            Date = date;
        }
    }
    
    public string Name { get; set; }
    public string Notes { get; set; }
    public string Tags { get; set; }
    public List<MyDateWrapper> History { get; set; }
    public ICommand SaveCommand { get; }
    public ICommand CloseCommand { get; }
    
    private Food _food;
    private IDataMapper<Food> _mapper;

    public EditFoodViewModel(IDataMapper<Food> mapper, Food food)
    {
        _mapper = mapper;
        _food = food;
        Name = _food.Name;
        Tags = string.Join(",", _food.Tags);
        Notes = _food.Notes;
        History = new List<MyDateWrapper>();
        foreach (var dateTime in _food.History)
        {
            History.Add(new MyDateWrapper(dateTime));
        }

        SaveCommand = new CommandViewModel(Save);
        CloseCommand = new CommandViewModel(Close);
    }

    private void Save(object? obj)
    {
        _food.Name = Name;
        _food.Tags = Tags.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
        _food.Notes = Notes;
        _food.History = new List<DateTime>();
        foreach (var dateWrapper in History)
        {
            _food.History.Add(dateWrapper.Date);
        }
        if (!_mapper.Update(_food))
            _mapper.Insert(_food);
        Close(obj);
    }

    private void Close(object? obj)
    {
        if (obj == null)
            return;
        var win = (Window) obj;
        win.Close();
    }
}