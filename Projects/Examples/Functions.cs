using System.Collections.Generic;
using NUnit.Framework;

namespace Examples
{
    public static class Functions
    {
        // this function is named Add
        // it takes 2 integers (x and y)
        // and returns an integer (the sum of x  and y)
        public static int Add(int x, int y)
        {
            return x + y;
        }

        // functions have 0 to many arguments and usually return a result

        // some functions return void - that means they return nothing

        // here is a function that does absolutely nothing
        public static void DoNothing()
        {

        }

        // here is a class representing a list of integers you can only add to
        public class AddOnlyIntList
        {
            // behind the scenes it has a private list of integers
            private List<int> _list = new List<int>();

            // this function is called a 'Method'
            // methods are functions that are attached to an instance of a class
            // Remember, all methods are functions but not all functions are methods
            // methods usually change something behind the scenes
            // functions that aren't methods usually just take their inputs and return some result without changing anything else
            public void Add(int a)
            {
                // for example
                // behind the scenes this method Add changes the list (it adds a new item to the list)
                _list.Add(a);
            }
        }
    }
}