using System;

namespace TicTacToe
{
    public static class Functions
    {
        public static void NewGame()
        {
            var game = new TicTacToe
            {
                Board = new BoardState(),
                ActivePlayer = GetStartingPlayer()
            };

            DrawGame(game);

            var gameOver = false;

            while (!gameOver)
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
                    case 'w': // top left
                        Functions.TrySetTopLeft(game);
                        break;
                    case 'e': // top middle
                        Functions.TrySetTopMiddle(game);
                        break;
                    case 'r': // top right
                        Functions.TrySetTopRight(game);
                        break;
                    case 's': // left
                        Functions.TrySetLeft(game);
                        break;
                    case 'd': // middle
                        Functions.TrySetMiddle(game);
                        break;
                    case 'f': // right
                        Functions.TrySetRight(game);
                        break;
                    case 'x': // bottom left
                        Functions.TrySetBottomLeft(game);
                        break;
                    case 'c': // bottom middle
                        Functions.TrySetBottomMiddle(game);
                        break;
                    case 'v': // bottom right
                        Functions.TrySetBottomRight(game);
                        break;
                    default:
                        break;
                }

                DrawGame(game);
            }
        }

        public static Player GetStartingPlayer()
        {
            var random = new Random();
            var n = random.Next(0, 1);

            if (n == 0)
                return Player.X;
            else if (n == 1)
                return Player.O;
            else
                throw new Exception("Can only select player X or player O");

        }

        public static bool TrySetGameSpace(TicTacToe state, Func<BoardState, Square> selectGameSpace)
        {
            var space = selectGameSpace(state.Board);
            if (space.State== Space.Empty)
            {
                space.State =
                    state.ActivePlayer == Player.O
                        ? Space.O
                        : Space.X;

                state.ActivePlayer = 
                    state.ActivePlayer == Player.O
                        ? Player.X
                        : Player.O;

                return true;
            }

            return false;
        }

        public static bool TrySetTopLeft(TicTacToe state) => TrySetGameSpace(state, b => b.TopLeft);
        public static bool TrySetTopMiddle(TicTacToe state) => TrySetGameSpace(state, b => b.TopMiddle);
        public static bool TrySetTopRight(TicTacToe state) => TrySetGameSpace(state, b => b.TopRight);
        public static bool TrySetLeft(TicTacToe state) => TrySetGameSpace(state, b => b.Left);
        public static bool TrySetMiddle(TicTacToe state) => TrySetGameSpace(state, b => b.Middle);
        public static bool TrySetRight(TicTacToe state) => TrySetGameSpace(state, b => b.Right);
        public static bool TrySetBottomLeft(TicTacToe state) => TrySetGameSpace(state, b => b.BottomLeft);
        public static bool TrySetBottomMiddle(TicTacToe state) => TrySetGameSpace(state, b => b.BottomMiddle);
        public static bool TrySetBottomRight(TicTacToe state) => TrySetGameSpace(state, b => b.BottomRight);

        public static void DrawGame(TicTacToe state)
        {
            var b = state.Board;

            var message = TryGetWinner(state, out Player winner)
                ? $"Player {winner} you won!!!!"
                : $"Player {state.ActivePlayer} it's your turn!";

            var gameUpdate = $@"w
Press q to quit
Press n for new game

 {RenderSpace(b.TopLeft)} | {RenderSpace(b.TopMiddle)} | {RenderSpace(b.TopRight)}
 ---------
 {RenderSpace(b.Left)} | {RenderSpace(b.Middle)} | {RenderSpace(b.Right)}
 ---------
 {RenderSpace(b.BottomLeft)} | {RenderSpace(b.BottomMiddle)} | {RenderSpace(b.BottomRight)}

{message}";

            Console.Clear();
            Console.Write(gameUpdate);
        }

        public static bool TryGetWinner(TicTacToe state, out Player winner)
        {
            bool ThreeInARow((Square, Square, Square) combo, out Player w)
            {
                var (a, b, c) = combo;

                w = Player.X;

                bool IsEmpty(Square s) => s.State == Space.Empty;

                Player GetWinner(Square s) =>
                    s.State == Space.O
                        ? Player.O
                        : Player.X;

                if (IsEmpty(a) || IsEmpty(b) || IsEmpty(c))
                    return false;

                if (a.State == b.State && b.State == c.State)
                {
                    w = GetWinner(a);
                    return true;
                }

                return false;
            }

            var combinations = new(Square, Square, Square)[]
            {
                (state.Board.TopLeft, state.Board.TopMiddle, state.Board.TopRight),
                (state.Board.Left, state.Board.Middle, state.Board.Right),
                (state.Board.BottomLeft, state.Board.BottomMiddle, state.Board.BottomRight),
                (state.Board.TopLeft, state.Board.Left, state.Board.BottomLeft),
                (state.Board.TopMiddle, state.Board.Middle, state.Board.BottomMiddle),
                (state.Board.TopRight, state.Board.Right, state.Board.BottomRight),
                (state.Board.TopLeft, state.Board.Middle, state.Board.BottomRight),
                (state.Board.BottomLeft, state.Board.Middle, state.Board.TopRight)
            };

            foreach (var combination in combinations)
            {
                if (ThreeInARow(combination, out winner))
                {
                    return true;
                }
            }

            winner = Player.X;

            return false;
        }

        public static char RenderSpace(Square square)
        {
            var space = square.State;

            switch (space)
            {
                case Space.Empty:
                    return '\0';
                case Space.O:
                    return 'O';
                case Space.X:
                    return 'X';
                default:
                    throw new Exception("Expected Empty, X, or O");
            }
        }
    }
}