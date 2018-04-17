using System;
using System.Collections.Generic;

namespace Examples
{
    public class Lists
    {
        // a list is just a collection of items that you can add / insert / delete from
        // think of a shopping list
        // you can add items to the list you want to get
        // and remove them when you are done
        // list are useful when you are working with a collection of items that are going to change

        public void Example()
        {
            // in c# you specify the type for your list in angle brackets
            
            // here is a list of bools (true or false)
            var list = new List<bool>();

            // the first item in the list is true
            list.Add(true);

            // the second item in the list is false
            list.Add(false);

            // this pushes a new bool to the front of the list (index 0 is the 1st item in the list)
            list.Insert(0, false);

            // this removes the last item
            list.RemoveAt(2);

            // now our list has 2 items - in order, they are false, true
        }
    }
}