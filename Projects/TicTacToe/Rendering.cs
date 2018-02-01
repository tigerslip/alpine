using System;

namespace TicTacToe
{
    public static class Rendering
    {
        public static void RenderGameState(GameState state)
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

        public static bool TryGetWinner(GameState state, out Player winner)
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

            var combinations = new (Square, Square, Square)[]
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