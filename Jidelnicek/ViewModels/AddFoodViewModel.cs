﻿using System;
using System.Linq;
using System.Windows.Input;
using Jidelnicek.DataMappers;
using Jidelnicek.Models;

namespace Jidelnicek.ViewModels;

public class AddFoodViewModel : BaseViewModel
{
    public string? Name { get; set; }
    public string? Tags { get; set; }
    public string? Notes { get; set; }
    public ICommand AddFoodCommand { get; }
    
    private readonly IDataMapper<Food> _mapper;

    public AddFoodViewModel()
    {
        _mapper = new FoodDataMapper();
        AddFoodCommand = new CommandViewModel(AddFood);
    }

    private void AddFood(object? obj)
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