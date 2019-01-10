namespace YahooWebScraper
{
    class WorldSeriesData
    {
        public int Year { get; }
        public string WinningTeam { get; }
        public string WinningTeamManager { get; }
        public string Games { get; }
        public string LosingTeam { get; }
        public string LosingTeamManager { get; }

        public WorldSeriesData(
            int year,
            string winningTeam,
            string winningTeamManager,
            string games,
            string losingTeam,
            string losingTeamManager)
        {
            Year = year;
            WinningTeam = winningTeam;
            WinningTeamManager = winningTeamManager;
            Games = games;
            LosingTeam = losingTeam;
            LosingTeamManager = losingTeamManager;
        }
    }
}