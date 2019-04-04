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
        [Value(0, HelpText ="The relative or full path to the sqlite file", Required = true, MetaName= "sqlite-path")]
        public string Path { get; set; }

        [Option(Required = false, Default = false)]
        public bool Force { get; set; }
    }

    [Verb("load-world-series-scores", HelpText = "Load world series scores into sqlite database.")]
    public class LoadWorldSeriesScoresOptions
    {
        [Value(0, HelpText = "The relative or full path to the sqlite file", Required = true, MetaName = "sqlite-path")]
        public string Path { get; set; }

        [Option(Required = false, Default = false)]
        public bool Force { get; set; }
    }
}
