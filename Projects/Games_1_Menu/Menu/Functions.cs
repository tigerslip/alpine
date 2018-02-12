using System;
using System.Linq;

namespace Games_1_Menu.Menu
{
    public static class Functions
    {
        public static void RunMenu()
        {
            // run menu should start our game loop
            // up and down should move the selector position up and down
            // enter should select the game
            // q should quit

            throw new NotImplementedException();
        }

        public static void MoveSelector(Menu menu, int move)
        {
            // move cursor should set the selector to it's next position 

            throw new NotImplementedException();
        }

        public static void DrawMenu(Menu state) {

            // use this function to draw the menu
            // remember, you will need to determine which game is selected based on the arrow

            throw new NotImplementedException();
        }
    }

    public class Menu
    {
        // we need to track two things here
        // 1: selector position - this can be an integer
        // 2: Games - a string array representing all the games we can play
    }
}
