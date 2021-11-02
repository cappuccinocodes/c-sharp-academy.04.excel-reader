using ConsoleTableExt;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;

namespace ExcelReader
{
    class Program 
    {
        static void Main(string[] args)
        {
            DatabaseManager.CheckDatabase();
            DatabaseManager.CreateDatabase();
            DatabaseManager.SeedDatabase();
        }
    }
}