using System;
using System.Data.SQLite;
using System.IO;

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
    LosingTeam varchar(32),
    LosingTeamManager varchar(32)
);
";

        }

        public void Dispose()
        {
            _connection.Dispose();
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
    }
}
