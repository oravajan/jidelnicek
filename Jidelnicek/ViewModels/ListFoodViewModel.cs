using System;
using System.Collections.Generic;
using System.Windows.Input;
using Jidelnicek.Commands;
using Jidelnicek.DataMappers;
using Jidelnicek.Models;

namespace Jidelnicek.ViewModels;

public class ListFoodViewModel : ViewModelBase
{
    private readonly IDataMapper<Food> _mapper;
    private List<Food> _foods;

    public ListFoodViewModel()
    {
        _mapper = new FoodDataMapper();
        _foods = _mapper.GetAll();
        MakeFoodCommand = new MakeFoodCommand(MakeFood);
    }

    public ICommand MakeFoodCommand { get; }

    public List<Food> Foods
    {
        get => _foods;
        set
        {
            _foods = value;
            OnPropertyChanged(nameof(_foods));
        }
    }

    private void MakeFood(int id)
    {
        _foods[id - 1].History.Add(DateTime.Now);
        _mapper.Update(_foods[id - 1]);
    }
}