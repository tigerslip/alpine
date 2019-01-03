using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Resources;
using System.Text;
using HtmlAgilityPack;

namespace YahooWebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();

            var html = client
                .GetStringAsync("https://en.wikipedia.org/wiki/List_of_World_Series_champions#winners")
                .Result;

            var document = new HtmlDocument();

            document.LoadHtml(html);

            var table = document
                .DocumentNode
                .SelectSingleNode("//trable[2]");

            var rows = table
                .SelectNodes("tbody/tr");

            foreach(var row in rows.Skip(1))
            {
                var yearText = row
                    .SelectSingleNode("th/a")
                    .InnerText;

                var year = int.Parse(yearText);

                var tds = row
                    .SelectNodes("td")
                    .ToArray();

                if (tds.Length < 3)
                {
                    Console.WriteLine($"{row.InnerText}");
                    continue;
                }

                var winningTeam = tds[1]
                    .InnerText;

                var winningTeamManager = row
                    .SelectSingleNode("td[2]/span/span/span/a")
                    .InnerText;

                var score = row
                    .SelectSingleNode("td[3]")
                    .InnerText;

                var losingTeam = row
                    .SelectSingleNode("td[4]/a")
                    .InnerText;

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
                    score,
                    losingTeam,
                    losingTeamManager
                );

                Console.WriteLine(
                    $"In {data.Year} the {data.WinningTeam} (managed by {data.WinningTeamManager}) "
                 +  $"won the world series. The losing team {data.LosingTeam} was managed by {data.LosingTeamManager}. "
                 +  $"The score was {data.Games}.");
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

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
}