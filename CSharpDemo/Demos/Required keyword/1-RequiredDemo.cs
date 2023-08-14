using CSharpDemo.Helpers;
using System.Diagnostics.CodeAnalysis;

namespace CSharpDemo.Demos.Required_keyword
{
    // The required modifier indicates that the field or property
    // it's applied to must be initialized by an object initializer.
    // Any expression that initializes a new instance of the type
    // must initialize all required members.
    public class RequiredDemo : DemoRunner<RequiredDemo>
    {
        [DemoCaption("Demo 1")]
        public void Demo1()
        {
            // Define required fields: Age and FirstName
            var p1 = new Person() { Age = 10, FirstName = "Bob" };

            // The compiler issues an error if the member
            // isn't initialized at all
            // var p2 = new Person("John", "Smith");

            // Use SetsRequiredMembers attribute
            var p3 = new Person();
        }

        public class Person
        {
            required public string FirstName { get; init; }
            public string LastName { get; init; }
            required public int Age { get; init; }

            public Person(string firstName, string lastName)
            {
                FirstName = firstName;
                LastName = lastName;
            }

            // The SetsRequiredMembers disables the compiler's checks
            // that all required members are initialized when an object is created.
            // Use it with caution.
            [SetsRequiredMembers]
            public Person()
            {
            }

            public override string ToString() => $"{FirstName} {LastName}, {Age} years old";
        }
    }
}
