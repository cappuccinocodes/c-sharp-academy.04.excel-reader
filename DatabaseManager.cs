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
        internal static void CheckDatabase()
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
                SeedDatabase();
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

        private static void SeedDatabase()
        {
            List<Match> seedData = ReadFromFile.Read();

            string date;
            Competition competition;
            string level;
            string score;
            string opponent;
            int shots;
            int shotsAgainst;
            int possession;
            int passing;

            for (int i = 1; i < seedData.Count; i++ )
            {
                //date = seedData[i].Date;
                //competition = seedData[i].Competition;
                //level = seedData[i].Level;
                //score = seedData[i].Score;
                //opponent = seedData[i].Opponent;
                //shots = seedData[i].Shots;
                //shotsAgainst = seedData[i].ShotsAgainst;
                //possession = seedData[i].Possession;
                //passing = seedData[i].Passing;

                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    var tableCmd = connection.CreateCommand();
                    tableCmd.CommandText = 
                        $@"INSERT INTO matches 
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
                    tableCmd.ExecuteNonQuery();
                    connection.Close();
                }

            }

           

        }
    }
}