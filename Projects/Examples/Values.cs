namespace Examples
{
    public class Values
    {
        public void Example()
        {
            // variable a is declared and it's value is the string "joe"
            var a = "joe";

            // it's value has now been changed to "bob"
            // it is still a variable named a
            // only the value it points to has changed
            a = "bob";
        }
    }
}