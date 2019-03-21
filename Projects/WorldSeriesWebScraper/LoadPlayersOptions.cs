using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace WorldSeriesWebScraper
{
    [Verb("load-players", HelpText = "Load world series players into sqlite database.")]
    public class LoadPlayersOptions
    {
        [Option(Required = true, HelpText = "Sqlite file path")]
        public string Path { get; set; }

        [Option(Required = false, Default = false)]
        public bool Force { get; set; }
    }

    [Verb("load-world-series-scores", HelpText = "Load world series scores into sqlite database.")]
    public class LoadWorldSeriesScoresOptions
    {
        [Option(Required = true, HelpText = "Sqlite file path")]
        public string Path { get; set; }

        [Option(Required = false, Default = false)]
        public bool Force { get; set; }
    }
}
