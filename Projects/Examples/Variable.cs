namespace Examples
{
    public class Variable
    {
        public void Example()
        {
            // both x and y (declared below) are of type integer (i.e. they are integers)

            // variable declaration using var (type is inferred)
            var x = 5;

            // variable declaration specifying the type explicitly
            int y = 5;

            // some programmers prefer to specify the type explicility instead of using var - to them it is easier to read

            // the downside to explicit type declarations is that when refactoring you will sometimes change the type of a variable / function result
            // this can make refactoring take longer, as you have to change the types for all variables involved in the refactor
        }
    }
}