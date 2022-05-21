using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Jidelnicek.Models;

namespace Jidelnicek.DataMappers;

public class FoodDataMapper : IDataMapper<Food>
{
    private string _fileName;

    public FoodDataMapper()
    {
        _fileName = @"E:\PROGRAMOVANI\Jidelnicek\Jidelnicek\Data\foods.json";
    }

    public List<Food> GetAll()
    {
        // List<Food> a = new List<Food>();
        // List<string> tagy = new List<string>();
        // tagy.Add("hovezi");
        // tagy.Add("lehky");
        // a.Add(new Food(0, "Hamburger", "chutna mi", new List<DateTime>(){new(2000, 1, 30)}, tagy));
        // a.Add(new Food(1, "Pizza", "", new List<DateTime>() {DateTime.Today}, new List<string>()));
        // SaveAll(a);

        string jsonString = File.ReadAllText(_fileName);
        var foods = JsonSerializer.Deserialize<List<Food>>(jsonString);
        if (foods == null)
            foods = new List<Food>();
        return foods;
    }

    public Food? Get(int id)
    {
        var foods = GetAll();
        return foods.Find(x => x.Id == id);
    }

    public bool Insert(Food entity)
    {
        var foods = GetAll();
        if (foods.Exists(x => x.Id == entity.Id))
            return false;
        
        foods.Insert(foods.Count - 1, entity);
        SaveAll(foods);
        return true;
    }

    public bool Update(Food entity)
    {
        var foods = GetAll();
        var index = foods.FindIndex(x => x.Id == entity.Id);
        if (index == -1)
            return false;

        foods[index] = entity;
        SaveAll(foods);
        return true;
    }

    public bool Delete(int id)
    {
        var foods = GetAll();
        var cnt = foods.RemoveAll(x => x.Id == id);
        if (cnt == 0)
            return false;
        
        SaveAll(foods);
        return true;
    }

    public void SaveAll(List<Food> foods)
    {
        string jsonString = JsonSerializer.Serialize(foods, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_fileName, jsonString);
    }
}