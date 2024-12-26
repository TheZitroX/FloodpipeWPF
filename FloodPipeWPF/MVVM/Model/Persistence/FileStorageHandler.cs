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

        // Step 3: Create a table
        string createTableQuery = @"
            CREATE TABLE IF NOT EXISTS Users 
            (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Age INTEGER
            );
        ";

        using (var command = new SQLiteCommand(createTableQuery, _connection))
        {
            command.ExecuteNonQuery();
        }

        // Step 4: Insert data into the table
        // string insertQuery = "INSERT INTO Users (Name, Age) VALUES ('Alice', 30), ('Bob', 25);";
        // using (var command = new SQLiteCommand(insertQuery, _connection))
        // {
        //     command.ExecuteNonQuery();
        // }

        // Step 5: Query the data
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
            // Ensure the connection is open
            if (_connection.State != System.Data.ConnectionState.Open)
                _connection.Open();

            // Define the SQL query
            string selectQuery = "SELECT Name FROM Users;";

            // Execute the query and read the results
            using (var command = new SQLiteCommand(selectQuery, _connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if ((reader["Name"] is not string name))
                        continue;

                    list.Add(name);
                }
            }
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

    public void AddName(string name)
    {
        try
        {
            // Ensure the connection is open
            if (_connection.State != System.Data.ConnectionState.Open)
                _connection.Open();

            // Define the SQL query
            string insertQuery = $"INSERT INTO Users (Name) VALUES ('{name}');";

            // Execute the query
            using (var command = new SQLiteCommand(insertQuery, _connection))
            {
                command.ExecuteNonQuery();
            }
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
}