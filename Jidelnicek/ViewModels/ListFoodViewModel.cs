using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Jidelnicek.DataMappers;
using Jidelnicek.Models;
using Jidelnicek.Views;

namespace Jidelnicek.ViewModels;

public class ListFoodViewModel : BaseViewModel
{
    public BindingList<Food> Foods { get; }
    public ICommand MakeFoodCommand { get; }
    public ICommand DeleteFoodCommand { get; }
    public ICommand EditFoodCommand { get; }

    private readonly IDataMapper<Food> _mapper;
    private List<Food> _back;

    public ListFoodViewModel()
    {
        _mapper = new FoodDataMapper();
        _back = _mapper.GetAll().ToList();
        _back.Sort(LastTimeCompare);
        Foods = new BindingList<Food>(_back);
        MakeFoodCommand = new CommandViewModel(MakeFood);
        DeleteFoodCommand = new CommandViewModel(DeleteFood);
        EditFoodCommand = new CommandViewModel(ShowEdit);
    }

    private int LastTimeCompare(Food f1, Food f2)
    {
        if (f1.LastTime == f2.LastTime)
            return 0;
        if (f1.LastTime == "Nevařeno")
            return 1;
        if (f2.LastTime == "Nevařeno")
            return -1;
        var d1 = DateTime.Parse(f1.LastTime);
        var d2 = DateTime.Parse(f2.LastTime);
        return d2.CompareTo(d1);
    }

    private void MakeFood(object? obj)
    {
        if (obj == null)
            return;
        var id = (int) obj;
        var f = Foods.Single(f => f.Id == id);
        f.History.Add(DateTime.Now);
        f.History.Sort((d1, d2) => d2.CompareTo(d1));
        _back.Sort(LastTimeCompare);
        _mapper.Update(f);
        Foods.ResetBindings();
    }

    private void DeleteFood(object? obj)
    {
        if (obj == null)
            return;
        var id = (int) obj;
        var f = Foods.Single(f => f.Id == id);
        Foods.Remove(f);
        _mapper.Delete(id);
    }

    private void ShowEdit(object? obj)
    {
        if (obj == null)
            return;
        var id = (int) obj;
        var f = Foods.Single(f => f.Id == id);
        EditFoodWindow w = new EditFoodWindow()
        {
            DataContext = new EditFoodViewModel(_mapper, f)
        };
        w.ShowDialog();
        _back.Sort(LastTimeCompare);
        Foods.ResetBindings();
    }
}