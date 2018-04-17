using System;
using System.Security.Cryptography.X509Certificates;

namespace Examples
{
    public class Enums
    {
        // an enum is a set of values
        public enum Colors
        {
            Red,
            Blue,
            Black,
            Green
        }

        public void Examples()
        {
            // both the variables below are of type 'Colors' which is an enum we have defined
            var red = Colors.Red;

            var blue = Colors.Blue;

        }

        // here is a function that takes a color and returns a string
        public string ConvertToString(Colors color)
        {
            // this is a switch statement
            switch (color)
            {
                case Colors.Red:
                    return "RED";
                case Colors.Blue:
                    return "BLUE";
                case Colors.Black:
                    return "BLACK";
                case Colors.Green:
                    return "GREEN";
                default:
                    throw new ArgumentOutOfRangeException(nameof(color), color, null);
            }
        }
    }
}