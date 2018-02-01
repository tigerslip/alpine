using System;

namespace TicTacToe
{
    public static class GameControlFunctions
    {
        // original functions

        //public static bool TrySetTopLeft(GameState state)
        //{
        //    if (state.Board.TopLeft == Square.Empty)
        //    {
        //        state.Board.TopLeft = 
        //            state.ActivePlayer == Player.O 
        //                ? Square.O 
        //                : Square.X;

        //        return true;
        //    }

        //    return false;
        //}

        public static bool TrySetGameSpace(GameState state, Func<BoardState, Square> selectGameSpace)
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

        public static bool TrySetTopLeft(GameState state) => TrySetGameSpace(state, b => b.TopLeft);
        public static bool TrySetTopMiddle(GameState state) => TrySetGameSpace(state, b => b.TopMiddle);
        public static bool TrySetTopRight(GameState state) => TrySetGameSpace(state, b => b.TopRight);
        public static bool TrySetLeft(GameState state) => TrySetGameSpace(state, b => b.Left);
        public static bool TrySetMiddle(GameState state) => TrySetGameSpace(state, b => b.Middle);
        public static bool TrySetRight(GameState state) => TrySetGameSpace(state, b => b.Right);
        public static bool TrySetBottomLeft(GameState state) => TrySetGameSpace(state, b => b.BottomLeft);
        public static bool TrySetBottomMiddle(GameState state) => TrySetGameSpace(state, b => b.BottomMiddle);
        public static bool TrySetBottomRight(GameState state) => TrySetGameSpace(state, b => b.BottomRight);
    }
}