using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TicTacToe;

namespace TicTacToe
{
    public class Program
    {
        static void Main(string[] args)
        {
            NewGame();
        }

        public static void NewGame()
        {
            var gameState = new GameState
            {
                Board = new BoardState(),
                ActivePlayer = GetStartingPlayer()
            };

            Rendering.RenderGameState(gameState);

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
                        GameControlFunctions.TrySetTopLeft(gameState);
                        break;
                    case 'e': // top middle
                        GameControlFunctions.TrySetTopMiddle(gameState);
                        break;
                    case 'r': // top right
                        GameControlFunctions.TrySetTopRight(gameState);
                        break;
                    case 's': // left
                        GameControlFunctions.TrySetLeft(gameState);
                        break;
                    case 'd': // middle
                        GameControlFunctions.TrySetMiddle(gameState);
                        break;
                    case 'f': // right
                        GameControlFunctions.TrySetRight(gameState);
                        break;
                    case 'x': // bottom left
                        GameControlFunctions.TrySetBottomLeft(gameState);
                        break;
                    case 'c': // bottom middle
                        GameControlFunctions.TrySetBottomMiddle(gameState);
                        break;
                    case 'v': // bottom right
                        GameControlFunctions.TrySetBottomRight(gameState);
                        break;
                    default:
                        break;
                }

                Rendering.RenderGameState(gameState);
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

            //switch(n)
            //{
            //    case 0:
            //        return Player.X;
            //    case 1:
            //        return Player.O;
            //    default:
            //        throw new Exception("Can only select player X or player O");
            //}
        }
    }
}
