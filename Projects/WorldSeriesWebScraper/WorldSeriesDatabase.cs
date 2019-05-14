using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
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

        public void CreatePlayerTable()
        {  
            var createPlayerTable = @"
CREATE TABLE IF NOT EXISTS Players (
    Name varchar(32),
    Age INT,
    DateOfBirth varchar(10),
    Country varchar(10),
    Salary NUMERIC,
    Team varchar(32)
);
";
            _connection.Execute(createPlayerTable);
        }

        public void InsertPlayers(Player[] players, string team)
        {
            var insertPlayersQuery = @"
INSERT INTO Players (
    Name,
    Age,
    DateOfBirth,
    Country,
    Salary,
    Team) 
VALUES (
    @Name,
    @Age,
    @DateOfBirth,
    @Country,
    @Salary,
    @Team);
";

            var data = players
                .Select(player => new
                {
                    Name = player.Name,
                    Age = player.Age,
                    DateOfBirth = player.DateOfBirth,
                    Country = player.Country,
                    Salary = player.Salary,
                    Team = team
                })
                .ToList();

            _connection.Execute(
                sql: insertPlayersQuery,
                param: data);
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

        private bool TableExists(string tableName)
        {
            return _connection.Query(
                sql: "SELECT name FROM sqlite_master WHERE type= 'table' AND name=@tableName;",
                param: new { tableName = tableName }
             ).Any();
        }

        public bool WorldSeriesScoresExist()
        {
            if (!TableExists("WorldSeriesScores"))
            {
                return false;
            }

            return _connection.Query(
                sql: "SELECT Year FROM WorldSeriesScores LIMIT 1"
                ).Any();
        }

        public IReadOnlyList<WorldSeriesData> GetWorldSeriesData()
        {
            const string query = @"
SELECT
    Year, 
    WinningTeam,
    WinningTeamManager,
    WinnerScore WinningScore,
    LoserScore LosingScore,
    TiedGames,
    LosingTeam,
    LosingTeamManager
FROM WorldSeriesScores
";

            return _connection
                .Query<WorldSeriesData>(query)
                .ToList();
        }

        public bool TeamPlayersAreLoaded(string team)
        {
            const string query = "SELECT Name FROM Players WHERE Team = @team";

            var playerNames = _connection.Query(query, new {team = team});

            if (playerNames.Any())
            {
                return true;
            }

            return false;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
