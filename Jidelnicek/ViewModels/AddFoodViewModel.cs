using System.Collections.Generic;
using System.Windows.Input;
using Jidelnicek.Commands;
using Jidelnicek.DataMappers;
using Jidelnicek.Models;

namespace Jidelnicek.ViewModels;

public class AddFoodViewModel : ViewModelBase
{
    private IDataMapper<Food> _mapper;
    public Food NewFood { get; set; }
    public ICommand AddFoodCommand { get; }

    public AddFoodViewModel()
    {
        _mapper = new FoodDataMapper();
        NewFood = new Food();
        AddFoodCommand = new AddFoodCommand(this);
    }

    public void AddFood()
    {
        _mapper.Insert(NewFood);
    }
}