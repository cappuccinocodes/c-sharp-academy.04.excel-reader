using ConsoleTableExt;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;

namespace ExcelReader
{
    internal class ReadFromFile
    {
        public static void Read()
        {
            FileInfo existingFile = new FileInfo(@"C:\Projects\Tutorials\ExcelReader\ExcelReader\Fifa.xlsx");
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
                        match.Competition = Enum.Parse<Competition>(worksheet.Cells[row, 2].Value?.ToString().Trim());
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

                ConsoleTableBuilder
                    .From(tableData)
                    .ExportAndWriteLine();
                Console.WriteLine("\n\n");
            }
        }
    }
}