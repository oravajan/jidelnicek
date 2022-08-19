using System;
using System.Collections.Generic;
using System.Linq;
using Jidelnicek.DataMappers;
using Jidelnicek.Models;

namespace Jidelnicek.ViewModels;

public class HistoryFoodViewModel : BaseViewModel
{
    public struct Record
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public Record(string name, DateTime date)
        {
            Name = name;
            Date = date;
        }
    }
    
    public List<Food> Foods
    {
        get => _foods;
        set
        {
            _foods = value;
            OnPropertyChanged(nameof(Foods));
        }
    }
    public List<Record> Records { get; set; }
    
    private readonly IDataMapper<Food> _mapper;
    private List<Food> _foods;

    public HistoryFoodViewModel()
    {
        _mapper = new FoodDataMapper();
        _foods = _mapper.GetAll().ToList();
        Records = LoadRecords();
    }
    
    private List<Record> LoadRecords()
    {
        var res = new List<Record>();
        foreach (var food in _foods)
        {
            var id = food.Name;
            foreach (var date in food.History) res.Add(new Record(id, date));
        }

        res.Sort((a, b) => b.Date.CompareTo(a.Date));
        return res;
    }
}