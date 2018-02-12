using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    public enum Space
    {
        Empty,
        X,
        O
    }

    public class Square
    {
        public Space State { get; set; }
    }

    public enum Player
    {
        X,
        O
    }
    public class TicTacToe
    {
        public BoardState Board { get; set; }
        public Player ActivePlayer { get; set; }
    }

    public class BoardState
    {
        public BoardState()
        {
            TopLeft = new Square();
            TopMiddle = new Square();
            TopRight = new Square();
            Left = new Square();
            Middle = new Square();
            Right = new Square();
            BottomLeft = new Square();
            BottomMiddle = new Square();
            BottomRight = new Square();
        }

        public Square TopLeft { get; set; }
        public Square TopMiddle { get; set; }
        public Square TopRight { get; set; }
        public Square Left { get; set; }
        public Square Middle { get; set; }
        public Square Right { get; set; }
        public Square BottomLeft { get; set; }
        public Square BottomMiddle { get; set; }
        public Square BottomRight { get; set; }
    }
}
