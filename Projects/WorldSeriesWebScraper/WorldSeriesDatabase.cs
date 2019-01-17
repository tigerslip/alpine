using System;
using System.Data.SQLite;
using System.IO;
using Dapper;

namespace WorldSeriesWebScraper
{
    class Database : IDisposable
    {
        private SQLiteConnection _connection;

        public Database(string filePath)
        {
            CreateIfNotExists(filePath);
            var connString = ConnectionString(filePath);
            _connection = new SQLiteConnection(connString);
        }

        private void CreateWorldSeriesScoreTable()
        {
            var createScoreTable = @"
CREATE TABLE IF NOT EXISTS WorldSeriesScores (
    Year INT PRIMARY KEY,
    WinningTeam varchar(32),
    WinningTeamManager varchar(32),
    WinnerScore INT,
    LoserScore INT,
    TiedGames INT,
    LosingTeam varchar(32),
    LosingTeamManager varchar(32)
);
";

            _connection.Execute(createScoreTable);
        }

        public void InsertWorldSeriesScores(WorldSeriesData[] scores)
        {
            CreateWorldSeriesScoreTable();

            var insertQuery = @"
INSERT INTO WorldSeriesScores (
    Year,
    WinningTeam,
    WinningTeamManager,
    WinnerScore,
    LoserScore,
    TiedGames,
    LosingTeam,
    LosingTeamManager) VALUES (
        @Year,
        @WinningTeam,
        @WinningTeamManager,
        @WinningScore,
        @LosingScore,
        @TiedGames,
        @LosingTeam,
        @LosingTeamManager
    );       
";

        _connection.Execute(insertQuery, scores);

        }

        internal static string ConnectionString(string path)
        {
            return $"Data Source={path};";
        }

        internal static void CreateIfNotExists(string path)
        {
            if (!File.Exists(path))
            {
                SQLiteConnection.CreateFile(path);
            }
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
