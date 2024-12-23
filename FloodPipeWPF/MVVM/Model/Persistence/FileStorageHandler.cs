namespace FloodPipeWPF.MVVM.Model.Persistence;

using System.Data.SQLite;

public class FileStorageHandler
{
    public FileStorageHandler()
    {
        // Step 1: Define the connection string
        string connectionString = "Data Source=Floodpipe2.sqlite;Version=3;";

        // Step 2: Create the database file if it doesn't exist
        SQLiteConnection.CreateFile("Floodpipe2.sqlite");

        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            // Step 3: Create a table
            string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Age INTEGER NOT NULL
                );";

            using (var command = new SQLiteCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }

            // Step 4: Insert data into the table
            string insertQuery = "INSERT INTO Users (Name, Age) VALUES ('Alice', 30), ('Bob', 25);";
            using (var command = new SQLiteCommand(insertQuery, connection))
            {
                command.ExecuteNonQuery();
            }

            // Step 5: Query the data
            string selectQuery = "SELECT * FROM Users;";
            using (var command = new SQLiteCommand(selectQuery, connection))
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

        Console.WriteLine("Done!");
    }
    
    public void SaveNameList(List<string> names)
    {
        // Save the list of names to a file
    }
}