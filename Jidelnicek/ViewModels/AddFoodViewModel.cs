using System;
using System.Linq;
using System.Windows.Input;
using Jidelnicek.Commands;
using Jidelnicek.DataMappers;
using Jidelnicek.Models;

namespace Jidelnicek.ViewModels;

public class AddFoodViewModel : ViewModelBase
{
    private readonly IDataMapper<Food> _mapper;

    public AddFoodViewModel()
    {
        _mapper = new FoodDataMapper();
        AddFoodCommand = new AddFoodCommand(this);
    }

    public string? Name { get; set; }
    public string? Tags { get; set; }
    public string? Notes { get; set; }
    public ICommand AddFoodCommand { get; }

    public void AddFood()
    {
        if (Name is "" or null)
            return;
        Tags ??= string.Empty;
        Notes ??= string.Empty;
        var tags = Tags.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

        var newFood = new Food(Name, Notes, tags);
        _mapper.Insert(newFood); //TODO kontrola spravneho vlozeni
    }
}