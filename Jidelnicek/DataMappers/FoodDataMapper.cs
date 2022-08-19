using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Jidelnicek.Models;

namespace Jidelnicek.DataMappers;

public class FoodDataMapper : IDataMapper<Food>
{
    private readonly string _connectionString;

    public FoodDataMapper()
    {
        string dataPath = Path.Combine(new []{
            AppDomain.CurrentDomain.BaseDirectory, "menu.db"
        });

        var builder = new SQLiteConnectionStringBuilder
        {
            DataSource = dataPath
        };
        _connectionString = builder.ConnectionString;

        if (!File.Exists(dataPath))
            CreateDb(dataPath);
    }

    public IEnumerable<Food> GetAll()
    {
        var all = new List<Food>();

        using var conn = new SQLiteConnection(_connectionString);
        conn.Open();
        const string selectFromFood = @"SELECT * FROM Food";

        using var cmd = new SQLiteCommand(selectFromFood, conn);
        using var dr = new SQLiteDataAdapter(cmd);

        var dataTable = new DataTable();
        dr.Fill(dataTable);

        foreach (DataRow row in dataTable.Rows)
        {
            var id = int.Parse(row["id_food"].ToString());
            var name = row["name"].ToString();
            var notes = row["notes"].ToString();
            var history = GetHistory(id);
            var tags = row["tags"].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
            all.Add(new Food(id, name, notes, history, tags));
        }

        return all;
    }

    public bool Insert(Food food)
    {
        var tags = string.Join(",", food.Tags);

        var sqlInsert = $@"INSERT INTO Food (name, notes, tags) VALUES('{food.Name}','{food.Notes}','{tags}')";
        return ExeNonQueryCommand(sqlInsert) == 1;
    }

    public bool Update(Food food)
    {
        UpdateHistory(food);
        var tags = string.Join(",", food.Tags);
        var sqlUpdate =
            $@"UPDATE Food SET name='{food.Name}', notes='{food.Notes}', tags='{tags}' WHERE id_food='{food.Id}'";
        return ExeNonQueryCommand(sqlUpdate) == 1;
    }

    public bool Delete(int id)
    {
        var sqlDelete = $@"DELETE FROM History WHERE id_food='{id}'";
        ExeNonQueryCommand(sqlDelete);
        sqlDelete = $@"DELETE FROM Food WHERE id_food='{id}'";
        return ExeNonQueryCommand(sqlDelete) == 1;
    }

    private void CreateDb(string dataPath)
    {
        SQLiteConnection.CreateFile(dataPath);
        const string sqlCreateFood = @"CREATE TABLE Food(
                                        id_food INTEGER PRIMARY KEY, 
                                        name varchar(30) NOT NULL, 
                                        notes varchar(100) NOT NULL,
                                        tags varchar(200) NOT NULL
                 )";
        const string sqlCreateHistory = @"CREATE TABLE History(
                                        id_history INTEGER PRIMARY KEY, 
                                        id_food INTEGER NOT NULL, 
                                        date TEXT NOT NULL
                 )";
        ExeNonQueryCommand(sqlCreateFood);
        ExeNonQueryCommand(sqlCreateHistory);
    }

    private int ExeNonQueryCommand(string sqlCommandText)
    {
        using var conn = new SQLiteConnection(_connectionString);
        conn.Open();

        using var cmd = new SQLiteCommand(sqlCommandText, conn);
        return cmd.ExecuteNonQuery();
    }

    private List<DateTime> GetHistory(int foodId)
    {
        var all = new List<DateTime>();

        using var conn = new SQLiteConnection(_connectionString);
        conn.Open();
        var selectFromHistory = $@"SELECT * FROM History WHERE id_food='{foodId}'";

        using var cmd = new SQLiteCommand(selectFromHistory, conn);
        using var dr = new SQLiteDataAdapter(cmd);

        var dataTable = new DataTable();
        dr.Fill(dataTable);

        foreach (DataRow row in dataTable.Rows) all.Add(DateTime.Parse(row["date"].ToString()));

        return all;
    }

    private void UpdateHistory(Food food)
    {
        var sqlDelete = $@"DELETE FROM History WHERE id_food='{food.Id}'";
        ExeNonQueryCommand(sqlDelete);
        foreach (var date in food.History)
        {
            var d = date.ToString("dd.MM.yyyy");
            var sqlInsert = $@"INSERT INTO History (id_food, date) VALUES('{food.Id}','{d}')";
            ExeNonQueryCommand(sqlInsert);
        }
    }
}