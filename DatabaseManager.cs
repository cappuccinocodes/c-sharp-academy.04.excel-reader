using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace ExcelReader
{
    internal class DatabaseManager
    {
        static readonly string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
        static readonly string dbPath = ConfigurationManager.AppSettings.Get("DbPath");

        internal static void DeleteDatabase()
        {
            File.Delete(dbPath);
            Console.WriteLine("\n\nDatabase Deleted.\n\n");
        }

        internal static void CheckDatabase()
        {

            string dbPath = ConfigurationManager.AppSettings.Get("DbPath");
            Console.WriteLine("\n\nHi there! I'm checking if database exists\n\n");
            bool dbExists = File.Exists(dbPath);

            if (dbExists)
            {
                DeleteDatabase();
            }
            
        }

        internal static void CreateDatabase()
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

        internal static void SeedDatabase()
        {
            List<Match> seedData = ReadFromFile.Read();

            for (int i = 0; i < seedData.Count; i++ )
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    var tableCmd = connection.CreateCommand();
                    tableCmd.CommandText = 
                        $@"INSERT INTO matches 
                             (Date,
                              Competition,
                              Level,
                              Score,
                              Opponent,
                              Shots,
                              ShotsAgainst,
                              Possession,
                              Passing)

                           VALUES (
                             '{seedData[i].Date}', 
                             {seedData[i].Competition}, 
                             '{seedData[i].Level}', 
                             '{seedData[i].Score}',
                             '{seedData[i].Opponent}', 
                             {seedData[i].Shots}, 
                             {seedData[i].ShotsAgainst}, 
                             {seedData[i].Possession}, 
                             {seedData[i].Passing}
                           )";
                    Console.WriteLine(tableCmd.CommandText);
                    tableCmd.ExecuteNonQuery();
                    connection.Close();
                }

            }
        }
    }
}