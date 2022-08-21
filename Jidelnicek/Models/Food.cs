using System;
using System.Collections.Generic;
using System.Linq;

namespace Jidelnicek.Models;

public class Food
{
    public int Id { get; }
    public string Name { get; set; }
    public string Notes { get; set; }
    public List<DateTime> History { get; set; }
    public List<string> Tags { get; set; }
    public string LastTime => History.Count == 0 ? "Nevařeno" : History.Last().ToString("dd.MM.yyyy");
    public string Cnt => History.Count + "x";
    
    public Food(int id, string name, string notes, List<DateTime> history, List<string> tags)
    {
        Id = id;
        Name = name;
        Notes = notes;
        History = history;
        History.Sort();
        Tags = tags;
        History.Sort();
    }

    public Food(string name, string notes, List<string> tags)
    {
        Name = name;
        Notes = notes;
        History = new List<DateTime>();
        Tags = tags;
    }
}