namespace ExcelReader
{
    internal class Match
    {
        public string Id { get; set; }
        public string Date { get; set; }
        public Competition Competition { get; set; }
        public string Level { get; set; }
        public string Score { get; set; }
        public string Opponent { get; set; }
        public int Shots { get; set; }
        public int ShotsAgainst { get; set; }
        public int Possession { get; set; }
        public int Passing { get; set; }
    }

    public enum Competition
    {
        Rivals,
        Champions,
        SquadBattles,
    }

}