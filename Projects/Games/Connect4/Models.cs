using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Connect4
{
    public class Connect4Game
    {
        public int RowCount { get; } = 6;
        public int ColumnCount { get; } = 8;

        public Connect4Game()
        {
            Space[] EmptyColumn() => Enumerable.Range(0, RowCount)
                .Select(i => Space.Empty)
                .ToArray();

            Columns = Enumerable.Range(0, ColumnCount)
                .Select(i => EmptyColumn())
                .ToArray();
        }

        public int CursorPosition { get; set; }
        public Player ActivePlayer { get; set; }
        public Space[][] Columns { get; }
    }
}
