using Microsoft.Data.Sqlite;
using System;
using System.Configuration;
using System.IO;

namespace ExcelReader
{
    internal class DatabaseManager
    {
        internal static void Check()
        {
            string databasePath = ConfigurationManager.AppSettings.Get("DatabasePath");
            Console.WriteLine("\n\nHi there! I'm checking if database exists\n\n");
            bool dbExists = File.Exists(databasePath);

            if (!dbExists)
            {
                Console.WriteLine("\n\nDatabase doesn't exist, creating one...\n\n");
                CreateDatabase();
            }
            else
            {
                Console.WriteLine("\n\nReady to Go...\n\n");
            }
        }

        private static void CreateDatabase()
        {
            string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $@"create table matches (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                        Date STRING, 
                        Competition INTEGER, 
                        Level TEXT, 
                        Score TEXT, 
                        Opponent TEXT, 
                        Shots INTEGER, 
                        ShotsAgainst INTEGER, 
                        Possession INTEGER, 
                        Passing INTEGER ) ";
                tableCmd.ExecuteNonQuery();
                connection.Close();

                Console.WriteLine("Table Created");
            }
        }
    }
}