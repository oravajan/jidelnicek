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
    public DateTime LastTime => History.LastOrDefault();

    // public Food(int id, string name, string notes, List<DateTime> history, List<string> tags)
    // {
    //     Id = id;
    //     Name = name;
    //     Notes = notes;
    //     History = history;
    //     Tags = tags;
    // }
}