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
    public BindingList<MyDateWrapper> History { get; set; }
    public ICommand SaveCommand { get; }
    public ICommand CloseCommand { get; }
    public ICommand MakeFoodCommand { get; }
    
    private Food _food;
    private IDataMapper<Food> _mapper;
    private List<MyDateWrapper> _back;

    public EditFoodViewModel(IDataMapper<Food> mapper, Food food)
    {
        _mapper = mapper;
        _food = food;
        
        Name = _food.Name;
        Tags = string.Join(",", _food.Tags);
        Notes = _food.Notes;
        
        _food.History.Sort((d1, d2) => d2.CompareTo(d1));
        _back = new List<MyDateWrapper>();
        foreach (var dateTime in _food.History)
        {
            _back.Add(new MyDateWrapper(dateTime));
        }
        History = new BindingList<MyDateWrapper>(_back);

        SaveCommand = new CommandViewModel(Save, (o => Name != ""));
        CloseCommand = new CommandViewModel(Close);
        MakeFoodCommand = new CommandViewModel(MakeFood);
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

        _mapper.Update(_food);
        Close(obj);
    }

    private void Close(object? obj)
    {
        if (obj == null)
            return;
        var win = (Window) obj;
        win.Close();
    }

    private void MakeFood(object? obj)
    {
        _back.Add(new MyDateWrapper(DateTime.Now));
        _back.Sort((d1, d2) => d2.Date.CompareTo(d1.Date));
        History.ResetBindings();
    }
}