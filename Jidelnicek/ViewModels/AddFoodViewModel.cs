using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Jidelnicek.DataMappers;
using Jidelnicek.Models;

namespace Jidelnicek.ViewModels;

public class AddFoodViewModel : BaseViewModel
{
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }
    public string Tags
    {
        get => _tags;
        set
        {
            _tags = value;
            OnPropertyChanged(nameof(Tags));
        }
    }
    public string Notes
    {
        get => _notes;
        set
        {
            _notes = value;
            OnPropertyChanged(nameof(Notes));
        }
    }
    public ICommand AddFoodCommand { get; }
    
    private readonly IDataMapper<Food> _mapper;
    private string _name = "";
    private string _tags = "";
    private string _notes = "";

    public AddFoodViewModel()
    {
        _mapper = new FoodDataMapper();
        AddFoodCommand = new CommandViewModel(AddFood, o => _name != "");
    }

    private void AddFood(object? obj)
    {
        var tags = Tags.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
        var newFood = new Food(Name, Notes, tags);

        if (_mapper.Insert(newFood))
        {
            MessageBox.Show("Jídlo bylo přidáno", "Přidáno", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Cancel);
            Name = string.Empty;
            Tags = string.Empty;
            Notes = string.Empty;
        }
        else
            MessageBox.Show("Jídlo nebylo přidáno!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Cancel);
    }
}