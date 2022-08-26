using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Jidelnicek.DataMappers;
using Jidelnicek.Models;
using Jidelnicek.Views;

namespace Jidelnicek.ViewModels;

public class ListFoodViewModel : BaseViewModel
{
    public ICollectionView FoodView { get; }
    public string Filter
    {
        get => _filter;
        set
        {
            _filter = value;
            OnPropertyChanged(nameof(Filter));
            FoodView.Refresh();
        }
    }
    public ICommand MakeFoodCommand { get; }
    public ICommand DeleteFoodCommand { get; }
    public ICommand EditFoodCommand { get; }
    public ICommand DeleteFilterCommand { get; }

    private readonly IDataMapper<Food> _mapper;
    private List<Food> _food;
    private string _filter = string.Empty;

    public ListFoodViewModel()
    {
        _mapper = new FoodDataMapper();
        _food = _mapper.GetAll().ToList();
        FoodView = CollectionViewSource.GetDefaultView(_food);
        FoodView.SortDescriptions.Add(new SortDescription("LastTime", ListSortDirection.Descending));
        FoodView.Filter = FilterFood;
        
        MakeFoodCommand = new CommandViewModel(MakeFood);
        DeleteFoodCommand = new CommandViewModel(DeleteFood);
        EditFoodCommand = new CommandViewModel(ShowEdit);
        DeleteFilterCommand = new CommandViewModel((o => Filter = string.Empty));
    }

    private bool FilterFood(object obj)
    {
        if (obj is not Food f) 
            return false;
        if (Filter == string.Empty)
            return true;
        var filters = Filter.Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (var filter in filters)
        {
            if (f.Name.Contains(filter.Trim(), StringComparison.CurrentCultureIgnoreCase))
                return true;
            if (f.Tags.Any(tag => tag.Contains(filter.Trim(), StringComparison.CurrentCultureIgnoreCase)))
                return true;
        }
        
        return false;
    }

    private void MakeFood(object? obj)
    {
        if (obj == null)
            return;
        var id = (int) obj;
        var f = _food.Single(f => f.Id == id);
        f.History.Add(DateTime.Now);
        f.History.Sort();
        _mapper.Update(f);
        FoodView.Refresh();
    }

    private void DeleteFood(object? obj)
    {
        if (obj == null)
            return;
        var res = MessageBox.Show("Opravdu smazat?", "Smazat", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (res == MessageBoxResult.No)
            return;
        var id = (int) obj;
        var f = _food.Single(f => f.Id == id);
        _food.Remove(f);
        _mapper.Delete(id);
        FoodView.Refresh();
    }

    private void ShowEdit(object? obj)
    {
        if (obj == null)
            return;
        var id = (int) obj;
        var f = _food.Single(f => f.Id == id);
        EditFoodWindow w = new EditFoodWindow()
        {
            DataContext = new EditFoodViewModel(_mapper, f),
            Owner = Application.Current.MainWindow
        };
        App.SetBlurEffect(true, w);
        w.ShowDialog();
        App.SetBlurEffect(false,null);
        FoodView.Refresh();
    }
}