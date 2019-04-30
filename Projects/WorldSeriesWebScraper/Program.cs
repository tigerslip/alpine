using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using HtmlAgilityPack;
using CommandLine;

namespace WorldSeriesWebScraper
{
    partial class Program
    {
        static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<LoadPlayersOptions, LoadWorldSeriesScoresOptions>(args)
                .MapResult(
                    (LoadPlayersOptions opts) => LoadPlayers(opts),
                    (LoadWorldSeriesScoresOptions opts) => LoadWorldSeriesScores(opts),
                    errs => PrintErrors(errs));
        }

        static int LoadPlayers(LoadPlayersOptions options)
        {
            var path = MakeSureDatabaseFileExists(options.Path);

            bool WorldSeriesScoresExist()
            {
                using (var connection = new Database(path))
                {
                    return connection.WorldSeriesScoresExist();
                }
            }

            if (!WorldSeriesScoresExist())
            {
                Console.WriteLine("World series scores not yet loaded. Loading now...");

                var loadWorldSeriesScoresOptions = new LoadWorldSeriesScoresOptions()
                {
                    Path = path
                };

                LoadWorldSeriesScores(loadWorldSeriesScoresOptions);
            }

            // ok need to load players now
            throw new NotImplementedException("Not done yet!");
        }
        
        static string MakeSureDatabaseFileExists(string path)
        {
            if (!File.Exists(path))
            {
                var extension = Path.GetExtension(path);
                if (extension != ".db3")
                {
                    path = Path.ChangeExtension(path, "db3");
                }

                Console.WriteLine($"Creating file: {path}");
                Database.CreateIfNotExists(path);
            }

            return path;
        }

        static int LoadWorldSeriesScores(LoadWorldSeriesScoresOptions options)
        {
            var path = MakeSureDatabaseFileExists(options.Path);

            using(var data = new Database(path))
            {
                if (data.WorldSeriesScoresExist())
                {
                    if(options.Force != true)
                    {
                        Console.WriteLine("World Series scores already loaded. Use --force to re-load.");
                        return 1;
                    }
                    else
                    {
                        File.Delete(path);
                        Database.CreateIfNotExists(path);
                    }
                }
            }

            Console.WriteLine("Beginning Get-World-Series-Scores");

            var scores = new List<WorldSeriesData>();
            
            var totalYears = DateTime.Now.Year - 1903;
            double count = 0;
            var nextPercentMessage = 20; // 20, 40, 60, 80, 100

            foreach (var score in GetWorldSeriesScores())
            {
                count++;
                scores.Add(score);

                var percent = (int)Math.Round(count / totalYears) * 100;
                if (percent >= nextPercentMessage)
                {
                    Console.WriteLine($"{nextPercentMessage} percent complete");
                    nextPercentMessage += 20;
                }
            }

            using (var data = new Database(path))
            {
                data.InsertWorldSeriesScores(scores.ToArray());
            }

            Console.WriteLine($"Completed loading World series data at {path}");

            
            return 1;
        }

        static int PrintErrors(IEnumerable<Error> errors)
        {
            foreach (var error in errors)
            {
                Console.WriteLine(error.ToString());
            }

            return 1;
        }

        private static Database ConnectDatabase()
        {
            var executingDir = AppDomain.CurrentDomain.BaseDirectory;
            var fileName = "worldseriesdatabase.db3";
            var fullPath = Path.Combine(executingDir, fileName);
            return new Database(fullPath);
        }

        private static IEnumerable<WorldSeriesData> GetWorldSeriesScores()
        {
            var client = new HttpClient();

            var html = client
                .GetStringAsync("https://en.wikipedia.org/wiki/List_of_World_Series_champions#winners")
                .Result;

            var document = new HtmlDocument();

            document.LoadHtml(html);

            var table = document
                .DocumentNode
                .SelectSingleNode("//table[2]");

            var rows = table
                .SelectNodes("tbody/tr");

            foreach (var row in rows.Skip(1))
            {
                string GetRowText(string xpath)
                {
                    return row
                        .SelectSingleNode(xpath)
                        .InnerText;
                }

                var yearText = GetRowText("th/a");

                var year = int.Parse(yearText);

                var tds = row
                    .SelectNodes("td")
                    .ToArray();

                if (tds.Length < 3)
                {
                    continue;
                }

                var winningTeam = GetRowText("td[1]/a");

                var winningTeamManager = GetRowText("td[2]/span/span/span/a");

                var score = GetRowText("td[3]");
                
                var splitScore = score.Split('–');

                var winningScore = int.Parse(splitScore[0]);

                var losingScore = (int) char.GetNumericValue(splitScore[1][0]);

                var tiedGames = splitScore.Length == 3
                    ? (int)char.GetNumericValue(splitScore[2][1])
                    : 0;

                var losingTeam = GetRowText("td[4]/a");

                var losingTeamManagerNode =
                    row.SelectSingleNode("td[5]/span/span/span/a")
                    ?? row.SelectSingleNode("td[5]/a");

                var losingTeamManager =
                    losingTeamManagerNode
                        .InnerText;

                var data = new WorldSeriesData(
                    year,
                    winningTeam,
                    winningTeamManager,
                    winningScore,
                    losingScore,
                    tiedGames,
                    losingTeam,
                    losingTeamManager
                );

                yield return data;
            }
        }
        
        private static string BaseballReferenceSearchUrl(int year, string teamName)
        {
            //Boston Red Sox
            //Boston+Red+Sox
            //St. Louis Cardinals -> St+Louis+Cardinals
            var formattedTeamName = teamName
                .Trim()
                .Replace(".", "")
                .Replace(' ', '+');
            
            //2018+Boston+Red+Sox
            var searchQuery = $"{year}+{formattedTeamName}";

            return $"https://www.baseball-reference.com/search/search.fcgi?hint=&search={searchQuery}&pid=&idx=";
        }

        private static IEnumerable<Player> GetWorldSeriesTeamPlayers(string team, int year)
        {
            var client = new HttpClient();

            var query = BaseballReferenceSearchUrl(year, team);

            var html = client
                .GetStringAsync(query)
                .Result;

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var rosterTableComment = doc
                .DocumentNode
                .SelectSingleNode("//comment()[contains(., 'Full-Season Roster &amp; Games by Position')]");

            var cleanedTable = rosterTableComment
                .InnerHtml
                .Replace("<!--", "")
                .Replace("-->", "")
                .Trim();

            var rosterTable = new HtmlDocument();
            rosterTable.LoadHtml(cleanedTable);

            var rows = rosterTable
                .DocumentNode
                .SelectNodes("div/div/table/tbody/tr");

            foreach (var row in rows)
            {
                var name = row.SelectSingleNode("th/a").InnerText;

                string RowText(string dataStat) => row
                    .SelectSingleNode($"td[@data-stat='{dataStat}']")
                    ?.InnerText;

                var ageText = RowText("age");

                var age = int.Parse(ageText);

                var country = RowText("flag");

                var dateOfBirthText = RowText("date_of_birth");
                
                var dateOfBirth = DateTime.ParseExact(
                    s: dateOfBirthText, 
                    formats: new [] {"MMM d, yyyy", "yyyy"},
                    provider: null,
                    style: DateTimeStyles.None);

                var salaryText = RowText("Salary");

                var style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;

                var salary = string.IsNullOrWhiteSpace(salaryText)
                    ? default(decimal?)
                    : decimal.Parse(salaryText, style);

                yield return new Player(
                    name,
                    age,
                    country,
                    dateOfBirth,
                    salary);
            }
        }
    }
}