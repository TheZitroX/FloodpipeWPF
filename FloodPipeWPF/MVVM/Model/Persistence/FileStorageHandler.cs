using System.IO;

namespace FloodPipeWPF.MVVM.Model.Persistence;

using System.Data.SQLite;

public class FileStorageHandler
{
    private const string FileName = "Floodpipe2.sqlite";
    private const string ConnectionString = "Data Source=" + FileName + ";Version=3;";

    private readonly SQLiteConnection _connection;

    public FileStorageHandler()
    {
        if (!File.Exists(FileName))
        {
            Console.Out.WriteLine("Creating new database file");
            SQLiteConnection.CreateFile(FileName);
        }

        _connection = new SQLiteConnection(ConnectionString);
        _connection.Open();

        string createTableQuery = @"
            CREATE TABLE IF NOT EXISTS Users 
            (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Age INTEGER DEFAULT NULL
            );
        ";

        using (var command = new SQLiteCommand(createTableQuery, _connection))
            command.ExecuteNonQuery();

        string selectQuery = "SELECT * FROM Users;";
        using (var command = new SQLiteCommand(selectQuery, _connection))
        using (var reader = command.ExecuteReader())
        {
            Console.WriteLine("ID\tName\tAge");
            Console.WriteLine("--------------------");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["Id"]}\t{reader["Name"]}\t{reader["Age"]}");
            }
        }
    }

    public void SaveNameList(List<string> names)
    {
        // Save the list of names to a file
    }

    public List<string> LoadNameList()
    {
        List<string> list = [];

        try
        {
            TryLoadingNamesFromDatabase(list);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading name list: {ex.Message}");
        }
        finally
        {
            if (_connection.State == System.Data.ConnectionState.Open)
                _connection.Close();
        }

        return list;
    }

    private void TryLoadingNamesFromDatabase(List<string> list)
    {
        if (_connection.State != System.Data.ConnectionState.Open)
            _connection.Open();

        string selectQuery = "SELECT Name FROM Users;";
        using var command = new SQLiteCommand(selectQuery, _connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            if ((reader["Name"] is not string name))
            {
                Console.Error.WriteLine("Name is not a string");
                
                continue;
            }

            list.Add(name);
        }
    }

    public void AddName(string name)
    {
        try
        {
            TryAddingNameToDatabase(name);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding name: {ex.Message}");
        }
        finally
        {
            if (_connection.State == System.Data.ConnectionState.Open)
                _connection.Close();
        }
    }

    private void TryAddingNameToDatabase(string name)
    {
        if (_connection.State != System.Data.ConnectionState.Open)
            _connection.Open();

        string insertQuery = "INSERT INTO Users (Name, Age) VALUES (@Name, @Age);";
        using (var command = new SQLiteCommand(insertQuery, _connection))
        {
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Age", null);
            command.ExecuteNonQuery();
        }
    }
}