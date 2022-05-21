using System;
using System.Collections.Generic;

namespace Jidelnicek.Models;

public class Food
{
    public int Id { get; }
    public string Name { get; set; }
    public string Notes { get; set; }
    public DateTime LastTime { get; set; }
    public List<string> Tags { get; set; }

    public Food(string name, string notes, List<string> tags)
    {
        Name = name;
        Notes = notes;
        LastTime = DateTime.MinValue;
        Tags = tags;
    }
}