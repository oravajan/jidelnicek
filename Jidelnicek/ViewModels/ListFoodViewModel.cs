using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Jidelnicek.DataMappers;
using Jidelnicek.Models;

namespace Jidelnicek.ViewModels;

public class ListFoodViewModel : BaseViewModel
{
    private readonly IDataMapper<Food> _mapper;

    public ListFoodViewModel()
    {
        IsOpen = false;
        _mapper = new FoodDataMapper();
        var tmp = _mapper.GetAll();
        tmp.Sort((f1, f2) => f2.LastTime.CompareTo(f1.LastTime));
        Foods = new BindingList<Food>(tmp);
        MakeFoodCommand = new CommandViewModel(MakeFood);
        DeleteFoodCommand = new CommandViewModel(DeleteFood);
        EditFoodCommand = new CommandViewModel(ShowEdit);
    }

    public ICommand MakeFoodCommand { get; }
    public ICommand DeleteFoodCommand { get; }
    public ICommand EditFoodCommand { get; }

    public BindingList<Food> Foods { get; }
    private bool _isOpen;
    public bool IsOpen
    {
        get { return _isOpen; }
        set
        {
            if (_isOpen == value) return;
            _isOpen = value;
            OnPropertyChanged("IsOpen");
        }
    }

    private void MakeFood(object? obj)
    {
        if (obj == null)
            return;
        var id = (int) obj;
        var f = Foods.Single(f => f.Id == id);
        f.History.Add(DateTime.Now);
        Foods.ResetItem(0);
        _mapper.Update(f);
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
        IsOpen = true;
    }
}