using System;
using System.Collections.Generic;
using System.Linq;
using Jidelnicek.DataMappers;

namespace Jidelnicek.ViewModels;

public class HistoryFoodViewModel : BaseViewModel
{
    public class Record
    {
        public string Name { get; }
        public DateTime Date { get; }

        public Record(string name, DateTime date)
        {
            Name = name;
            Date = date;
        }
    }
    
    public List<Record> Records { get; set; }

    public HistoryFoodViewModel()
    {
        Records = new List<Record>();
        var foods = new FoodDataMapper().GetAll().ToList();
        foreach (var food in foods)
        {
            foreach (var date in food.History) 
                Records.Add(new Record(food.Name, date));
        }

        Records.Sort((a, b) => b.Date.CompareTo(a.Date));
    }
}