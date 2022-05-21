using System.Collections.Generic;
using Jidelnicek.DataMappers;
using Jidelnicek.Models;

namespace Jidelnicek.ViewModels;

public class ListFoodViewModel : ViewModelBase
{
    private IDataMapper<Food> _mapper;
    private List<Food> _foods;

    public List<Food> Foods
    {
        get => _foods;
        set
        {
            _foods = value;
            OnPropertyChanged(nameof(_foods));
        }
    }

    public ListFoodViewModel()
    {
        _mapper = new FoodDataMapper();
        Foods = _mapper.GetAll();
    }
}