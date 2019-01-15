namespace WorldSeriesWebScraper
{
    class WorldSeriesData
    {
        public int Year { get; }
        public string WinningTeam { get; }
        public string WinningTeamManager { get; }
        public int WinningScore { get; }
        public int LosingScore { get; }
        public int TiedGames { get; }
        public string LosingTeam { get; }
        public string LosingTeamManager { get; }

        public WorldSeriesData(
            int year,
            string winningTeam,
            string winningTeamManager,
            int winningScore,
            int losingScore,
            int tiedGames,
            string losingTeam,
            string losingTeamManager)
        {
            Year = year;
            WinningTeam = winningTeam;
            WinningTeamManager = winningTeamManager;
            WinningScore = winningScore;
            LosingScore = losingScore;
            TiedGames = tiedGames;
            LosingTeam = losingTeam;
            LosingTeamManager = losingTeamManager;
        }
    }
}