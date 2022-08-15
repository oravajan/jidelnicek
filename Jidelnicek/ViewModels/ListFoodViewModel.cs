using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Jidelnicek.Commands;
using Jidelnicek.DataMappers;
using Jidelnicek.Models;

namespace Jidelnicek.ViewModels;

public class ListFoodViewModel : ViewModelBase
{
    private readonly IDataMapper<Food> _mapper;

    public ListFoodViewModel()
    {
        _mapper = new FoodDataMapper();
        var tmp = _mapper.GetAll();
        tmp.Sort((f1, f2) => f2.LastTime.CompareTo(f1.LastTime));
        Foods = new BindingList<Food>(tmp);
        MakeFoodCommand = new MakeFoodCommand(MakeFood);
        DeleteFoodCommand = new DeleteFoodCommand(DeleteFood);
    }

    public ICommand MakeFoodCommand { get; }
    public ICommand DeleteFoodCommand { get; }

    public BindingList<Food> Foods { get; }

    private void MakeFood(int id)
    {
        var f = Foods.Single(f => f.Id == id);
        f.History.Add(DateTime.Now);
        Foods.ResetItem(0);
        _mapper.Update(f);
    }

    private void DeleteFood(int id)
    {
        var f = Foods.Single(f => f.Id == id);
        Foods.Remove(f);
        _mapper.Delete(id);
    }
}