using System;

namespace Examples
{
    public class Arrays
    {
        // An array is a data structure, which can store a fixed-size collection of elements
        public void Example()
        {
            // heres an array with 5 integers
            var array = new int[] {1, 2, 5, 7, 10};

            // arrays are useful when you are working with a collection that doesn't need to be changed
            // use a list if you need to add / update / or remove items from a collection

            // the property 'Length' tells us how long an array is
            for (int i = 0; i < array.Length; i++)
            {
                // this loop goes through the array and writes each value to the console
                var value = array[i];
                Console.WriteLine(value);
            }
        }
    }
}