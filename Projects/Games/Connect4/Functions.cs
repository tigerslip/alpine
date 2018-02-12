using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Connect4
{
    public static class Functions
    {
        public static void NewConnect4Game()
        {
            var quitGame = false;
            var game = new Connect4Game();

            Draw(game);

            while (!quitGame)
            {
                var key = Console.ReadKey();

                switch(key.Key)
                {
                    case ConsoleKey.RightArrow:
                        Move(game, 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        Move(game, -1);
                        break;
                    case ConsoleKey.Enter:
                        DropPiece(game);
                        break;
                    case ConsoleKey.Q:
                        quitGame = true;
                        return;
                    case ConsoleKey.N:
                        quitGame = true;
                        NewConnect4Game();
                        break;
                }

                Draw(game);
            }
        }

        public static bool TryGetWinner(Connect4Game game, out Player winner)
        {
            bool TryGet4Up(Space[][] board, int column, int row)
            {
                throw new NotImplementedException();

                var space = board[column][row];

                if (space == Space.Empty)
                    return false;

                if (board.Length < column + 4)
                    return false;

                for (int i = 3; i > 0; i--)
                {

                }

                var fourthAbove = board[column + 3][row];

                if (fourthAbove != space) return false;
            }

            throw new NotImplementedException();
        }

        private static List<Space>[] DistributeColumnToRows(List<Space>[] rows, Space[] column)
        {
            for (int i = 0; i < column.Length; i++)
            {
                var item = column[i];
                rows[i].Add(item);
            }

            return rows;
        }

        public static string[] DrawRows(Connect4Game game)
        {
            string DrawSpace(Space space)
            {
                if (space == Space.Black) return "O";
                if (space == Space.Red) return "X";
                return " ";
            }

            var seed = Enumerable.Range(0, game.RowCount)
                .Select(i => new List<Space>())
                .ToArray();

            var rows = game.Columns.
                Aggregate(seed, DistributeColumnToRows);

            return rows
                .Reverse()
                .Select(r => String.Join(" | ", r.Select(DrawSpace)))
                .ToArray();
        }

        private static void Draw(Connect4Game game)
        {
            var cursorMarkers = Enumerable.Range(0, game.ColumnCount)
                .Select(i => " ")
                .ToArray();

            cursorMarkers[game.CursorPosition] = "V";

            var cursorLine = String.Join("   ", cursorMarkers);

            var rowLines = DrawRows(game);

            var mergedRowlines = String.Join("\r\n", rowLines);

            var scene = $@"
Player {game.ActivePlayer} it's your turn!

{cursorLine}
{mergedRowlines}

Press q to quit
Press n for new game
";

            Console.Clear();
            Console.Write(scene);
        }

        private static void DropPiece(Connect4Game game)
        {
            var column = game.Columns[game.CursorPosition];
            for (int i = 0; i < column.Length; i++)
            {
                var marker = column[i];
                if(marker == Space.Empty)
                {
                    column[i] = game.ActivePlayer == Player.Black
                            ? Space.Black
                            : Space.Red;

                    game.ActivePlayer = game.ActivePlayer == Player.Black
                        ? Player.Red
                        : Player.Black;

                    return;
                }
            }
        }

        private static void Move(Connect4Game game, int move)
        {
            var nextPosition = game.CursorPosition + move;
            if(nextPosition > -1 && nextPosition < game.Columns.Length - 1)
            {
                game.CursorPosition = nextPosition;
            }
        }
    }

    public enum Player
    {
        Red,
        Black
    }

    public enum Space
    {
        Empty,
        Red,
        Black
    }

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
