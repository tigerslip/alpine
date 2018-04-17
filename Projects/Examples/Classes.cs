namespace Examples
{
    public class Classes
    {
        //In object-oriented programming (OOP), a class is an extensible program-code-template for creating objects, 
        // providing initial values for state(member variables) and implementations of behavior(member functions or methods).

        // since c# is an OOP language, almost everything lives in a class

        // classes can hold data and contain functions
        // you can create an instance of a class with the 'new' keyword

        // here is a class that represents some data about a person
        public class Person
        {
            // it has two properties Name and Age
            public string Name { get; }
            public int Age { get; }

            // those properties are set when you construct the class
            // this is a constructor - constructors have the same name as the Class
            public Person(string name, int age)
            {
                Name = name;
                Age = age;
            }
        }

        // here is a method that creates and returns some persons
        public Person[] CreateSomePersons()
        {
            // one instance of the Person class has the name jack and age 25
            var jack = new Person("jack", 25);

            // another instance is sally age 24
            var sally = new Person("sally", 24);

            // these are two different instances of the class "Person" with different values
            // they represent a person but with different properties

            // you could also say jack and sally are of 'type' Person because Person is a type that we have defined
            return new[] {jack, sally};
        }
    }
}