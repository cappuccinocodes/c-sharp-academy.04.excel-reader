using ConsoleTableExt;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;

namespace ExcelReader
{
    internal class ReadFromFile
    {
        public static List<Match> Read()
        {
            string filePath = ConfigurationManager.AppSettings.Get("FilePath");
            FileInfo existingFile = new FileInfo(filePath);
            //use EPPlus
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                List<Match> tableData = new List<Match>();

                //get the first worksheet in the workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.End.Column;  //get Column Count
                int rowCount = worksheet.Dimension.End.Row;     //get row count

                for (int row = 2; row <= rowCount; row++)
                {
                    Match match = new Match();

                    for (int col = 1; col <= colCount; col++)
                    {
                        match.Date = worksheet.Cells[row, 1].Value?.ToString().Trim();
                        match.Competition = Int32.Parse(worksheet.Cells[row, 2].Value?.ToString().Trim());
                        match.Level = worksheet.Cells[row, 3].Value?.ToString().Trim();
                        match.Score = worksheet.Cells[row, 4].Value?.ToString().Trim();
                        match.Opponent = worksheet.Cells[row, 5].Value?.ToString().Trim();
                        match.Shots = Int32.Parse(worksheet.Cells[row, 6].Value?.ToString().Trim());
                        match.ShotsAgainst = Int32.Parse(worksheet.Cells[row, 7].Value?.ToString().Trim());
                        match.Possession = Int32.Parse(worksheet.Cells[row, 8].Value?.ToString().Trim());
                        match.Passing = Int32.Parse(worksheet.Cells[row, 9].Value?.ToString().Trim());
                    }

                    tableData.Add(match);
                }

                return tableData;
            }
        }

        private static void ShowData(List<Match> tableData)
        {
            ConsoleTableBuilder
                                .From(tableData)
                                .ExportAndWriteLine();
            Console.WriteLine("\n\n");
        }
    }
}