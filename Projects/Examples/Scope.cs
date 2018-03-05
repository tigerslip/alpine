using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Examples
{
    public class Scope
    {
        // here is a private field (it's a field because it's declared in the scope of the class, and not within a function)
        private int u = 30;

        public void Example()
        {
            var x = 5;

            int Addx(int a)
            {
                // this function can access the variable x because it's in the outer function scope
                var result = a + x;

                return result;
            }

            // the following statement results in an error, because the outer scope cannot access variables from the inner scope
            //var y = result;

            // however, you can access the variable x from above the function Addx - because we are still in that enclosing scope
            // you can also access the variable u, because functions in a class have access to the class scope
            var z = x + u;
        }

        public void OtherExample()
        {
            // this function has no access to any variables / functions declared in the function 'Example'
            //Console.WriteLine(z); <-- error: The name 'z' Does not exist in the current context

            Console.WriteLine(u);
        }
    }
}
