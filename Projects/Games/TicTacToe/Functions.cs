using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    static class Functions
    {
        static void NewGame()
        {
            var gameOver = false;
            var state = new GameState();

            Draw(state);

            while (gameOver != true) // !gameOver
            {
                var key = Console.ReadKey();

                switch (key.KeyChar)
                {
                    case 'q':
                        gameOver = true;
                        break;
                    case 'n':
                        gameOver = true;
                        NewGame();
                        break;
                    case 'w':
                        TryMarkSpace(state, g => g.TopLeft);
                        break;
                    case 'e':
                        TryMarkSpace(state, g => g.TopMiddle);
                        break;
                    case 'r':
                        TryMarkSpace(state, g => g.TopRight);
                        break;
                    case 's':
                        TryMarkSpace(state, g => g.Left);
                        break;
                    case 'd':
                        TryMarkSpace(state, g => g.Middle);
                        break;
                    case 'f':
                        TryMarkSpace(state, g => g.Right);
                        break;
                    case 'x':
                        TryMarkSpace(state, g => g.BottomLeft);
                        break;
                    case 'c':
                        TryMarkSpace(state, g => g.BottomMiddle);
                        break;
                    case 'v':
                        TryMarkSpace(state, g => g.BottomRight);
                        break;
                }

                PickWinner(state);

                if (state.Winner.HasValue)
                {
                    DrawWinScreen(state);
                }
                else
                {
                    Draw(state);
                }
            }
        }

        static void DrawWinScreen(GameState state)
        {
            Console.Clear();

            var board = DrawGameBoard(state);

            var update = "WINNER!!!! \r\n" + board;

            Console.Write(update);
            Console.ReadKey();
        }

        static void PickWinner(GameState state)
        {
            bool ThreeInARow(Space[] spaces, out Player winner)
            {
                winner = Player.O;

                Space space1 = spaces[0];
                Space space2 = spaces[1];
                Space space3 = spaces[2];

                if (space1.State == SpaceState.Empty)
                {
                    return false;
                }

                if (space1.State == space2.State && space2.State == space3.State)
                {
                    if (space1.State == SpaceState.O)
                    {
                        winner = Player.O;
                    }
                    else
                    {
                        winner = Player.X;
                    }

                    return true;
                }

                return false;
            }

            var winningCombinations = new Space[][]
            {
                new Space[] {state.TopLeft, state.TopMiddle, state.TopRight},
                new Space[] {state.TopLeft, state.Middle, state.BottomRight},
                new Space[] {state.TopLeft, state.Left, state.BottomLeft},
                new Space[] {state.Left, state.Middle, state.Right},
                new Space[] {state.BottomLeft, state.Middle, state.TopRight},
                new Space[] {state.BottomLeft, state.BottomMiddle, state.BottomRight},
                new Space[] {state.TopMiddle, state.Middle, state.BottomMiddle},
                new Space[] {state.TopRight, state.Right, state.BottomRight}
            };

            for (int i = 0; i < winningCombinations.Length; i++)
            {
                var combination = winningCombinations[i];

                if (ThreeInARow(combination, out Player player))
                {
                    state.Winner = player;
                    return;
                }
            }
        }

        static void TryMarkSpace(GameState state, Func<GameState, Space> selectSpace)
        {
            var space = selectSpace(state);

            if (space.State == SpaceState.Empty)
            {
                if (state.ActivePlayer == Player.X)
                {
                    space.State = SpaceState.X;
                }

                if (state.ActivePlayer == Player.O)
                {
                    space.State = SpaceState.O;
                }

                if (state.ActivePlayer == Player.X)
                {
                    state.ActivePlayer = Player.O;
                }
                else
                {
                    state.ActivePlayer = Player.X;
                }
            }
        }

        static void Draw(GameState state)
        {
            var board = DrawGameBoard(state);

            var gameUpdate = $@"
{board}

Player {state.ActivePlayer} it's your turn!
";

            Console.Clear();
            Console.Write(gameUpdate);
        }

        static string DrawGameBoard(GameState state)
        {
            string DrawSpace(Space space)
            {
                switch (space.State)
                {
                    case SpaceState.Empty:
                        return " ";
                    case SpaceState.X:
                        return "X";
                    case SpaceState.O:
                        return "O";
                    default:
                        return " ";
                }
            }

            return $@"
    Press q to quit
    Press n for a new game

     {DrawSpace(state.TopLeft)} | {DrawSpace(state.TopMiddle)} | {DrawSpace(state.TopRight)}
    -----------
     {DrawSpace(state.Left)} | {DrawSpace(state.Middle)} | {DrawSpace(state.Right)}
    -----------
     {DrawSpace(state.BottomLeft)} | {DrawSpace(state.BottomMiddle)} | {DrawSpace(state.BottomRight)}
    ";
        }
    }

    public enum Player
    {
        X,
        O
    }

    public enum SpaceState
    {
        Empty,
        X,
        O
    }

    public class Space
    {
        public SpaceState State { get; set; }
    }

    public class GameState
    {
        public GameState()
        {
            TopLeft = new Space();
            TopMiddle = new Space();
            TopRight = new Space();
            Left = new Space();
            Middle = new Space();
            Right = new Space();
            BottomLeft = new Space();
            BottomMiddle = new Space();
            BottomRight = new Space();
        }

        public Player ActivePlayer { get; set; }
        public Player? Winner { get; set; }
        public Space TopLeft { get; set; }
        public Space TopMiddle { get; set; }
        public Space TopRight { get; set; }
        public Space Left { get; set; }
        public Space Middle { get; set; }
        public Space Right { get; set; }
        public Space BottomLeft { get; set; }
        public Space BottomMiddle { get; set; }
        public Space BottomRight { get; set; }
    }
}
