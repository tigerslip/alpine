using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Menu
{
    public class Menu
    {
        public int SelectorPosition { get; set; }
        public string[] Games => new[] { "Tic-Tac-Toe", "Connect-4" };
    }
}
