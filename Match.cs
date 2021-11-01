using System;
using System.ComponentModel.DataAnnotations;

namespace ExcelReader
{
    internal class Match
    {
        public string Id { get; set; }
        public string Date { get; set; }
        public int Competition
        {
            get => (int)this.CompetitionType;

            set => CompetitionType = (CompetitionTypes)value;
        }
        public string Level { get; set; }
        public string Score { get; set; }
        public string Opponent { get; set; }
        public int Shots { get; set; }
        public int ShotsAgainst { get; set; }
        public int Possession { get; set; }
        public int Passing { get; set; }

        [EnumDataType(typeof(CompetitionTypes))]
        public CompetitionTypes CompetitionType { get; set; }

        public enum CompetitionTypes
        {
            Rivals = 0,
            Champions = 1,
            SquadBattles = 2,
        }
    }

}