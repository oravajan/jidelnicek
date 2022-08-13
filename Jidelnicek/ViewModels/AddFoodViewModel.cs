using System;
using System.Linq;
using System.Windows.Input;
using Jidelnicek.Commands;
using Jidelnicek.DataMappers;
using Jidelnicek.Models;

namespace Jidelnicek.ViewModels;

public class AddFoodViewModel : ViewModelBase
{
    private IDataMapper<Food> _mapper;
    public string? Name { get; set; }
    public string? Tags { get; set; }
    public string? Notes { get; set; }
    public ICommand AddFoodCommand { get; }

    public AddFoodViewModel()
    {
        _mapper = new FoodDataMapper();
        AddFoodCommand = new AddFoodCommand(this);
    }
    public void AddFood()
    {
        if(Name is "" or null)
            return;
        Tags ??= String.Empty;
        Notes ??= String.Empty;
        var tags = Tags.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
        
        var newFood = new Food(Name, Notes, tags);
        _mapper.Insert(newFood);  //TODO kontrola spravneho vlozeni
    }
}
