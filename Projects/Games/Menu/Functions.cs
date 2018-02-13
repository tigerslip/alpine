using System;
using System.Linq;

namespace Games.Menu
{
    public static class Functions
    {
        public static void RunMenu()
        {
            var menu = new Menu();

            bool exit = false;
            DrawMenu(menu);

            while (!exit)
            {
                var key = Console.ReadKey();

                switch(key.Key)
                {
                    case ConsoleKey.Q:
                        exit = true;
                        break;
                    case ConsoleKey.UpArrow:
                        MoveSelector(menu, -1);
                        break;
                    case ConsoleKey.DownArrow:
                        MoveSelector(menu, 1);
                        break;
                    case ConsoleKey.Enter:
                        switch (menu.SelectorPosition)
                        {
                            case 0:
                                break;
                            case 1:
                                Connect4.Functions.NewConnect4Game();
                            break;
                        }
                        break;
                }

                DrawMenu(menu);
            }
        }

        public static void MoveSelector(Menu menu, int move)
        {
            var nextPos = menu.SelectorPosition + move;

            if (menu.Games.ElementAtOrDefault(nextPos) != null)
            {
                menu.SelectorPosition = nextPos;
            }
        }

        public static void DrawMenu(Menu state) {

            string[] DrawLines(Menu menu)
            {
                char CursorOrEmpty(string game) =>
                    Array.IndexOf(menu.Games, game) == menu.SelectorPosition
                        ? '>'
                        : '\0';

                return menu.Games
                    .Select(g => $" {CursorOrEmpty(g)} {g}")
                    .ToArray();
            }

            var gameLines = String.Join("\r\n", DrawLines(state));

            var scene = 
$@"Select a game:

{gameLines}

Press q to quit:
";

            Console.Clear();
            Console.Write(scene);
        }
    }
}
