using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace TicTacToe.Tests
{
    public class Class1
    {
        public class Tests
        {
            [Test, Repeat(100)]
            public void RandomPlayerIsAlwaysXOrO()
            {
                var player = Program.GetStartingPlayer();
                Assert.IsInstanceOf<Player>(player);
            }

            [Test]
            public void SetTopLeft_TopLeftIsEmpty_TopLeftIsX()
            {
                var game = new GameState {Board = new BoardState(), ActivePlayer = Player.X};
                var result = GameControlFunctions.TrySetTopLeft(game);
                Assert.IsTrue(result);
                Assert.IsTrue(game.Board.TopLeft.State == Space.X);
                Assert.AreEqual(Player.O, game.ActivePlayer);
            }
        }
    }
}
