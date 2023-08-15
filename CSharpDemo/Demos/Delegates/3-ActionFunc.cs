using CSharpDemo.Helpers;

namespace CSharpDemo.Demos.Delegates
{
    public class ActionFuncDemo : DemoRunner<ActionFuncDemo>
    {
        [DemoCaption("Action demo: print sum of two numbers")]
        public void Demo1()
        {
            var sumTwo = (int a, int b) => Console.WriteLine(a + b);

            sumTwo(5, 5);
        }

        [DemoCaption("Func demo: sum two numbers and return result")]
        public void Demo2()
        {
            var sumTwo = (int a, int b) => a + b;

            Console.WriteLine(sumTwo(15, 15));
        }

        public class Person { public string Name { get; set; } }
        public class Employee : Person { }
        public class Programmer : Employee { }
        public class Id { }
        public class Guid : Id { }

        delegate Person GetPersonById(Guid id);

        [DemoCaption("Delegate demo: covariance and contrvariance")]
        public void Demo3()
        {
            // Delegate GetEmployeeById takes parameter with type Guid and returns Person
            // Covariance and contrvariance allow to assign method GetPerson to the delegate
            // despite the fact that GetPerson returns Employee and takes Id
            GetPersonById getPerson = GetPerson;

            getPerson(new Guid());

            Employee GetPerson(Id id)
            {
                return new Programmer();
            }
        }

        [DemoCaption("Func demo: covariance and contrvariance")]
        public void Demo4()
        {
            // Func - input marked as in (contrvariant), output makred as out (covariance)
            // Covariance (out) - more derived type assign to less derived. It preserves assignment compatibility.
            // Contrvariance (in) - less derived type assign to more derived. It reverses assignment compatibility.

            Func<Guid, Person> getEmployeeById = (Guid id) => new Employee(); // Employee assign to Person
            getEmployeeById = (Guid id) => new Employee();
        }

        [DemoCaption("Action demo: covariance and contrvariance")]
        public void Demo5()
        {
            // Action - input marked as in (contrvariant), output makred as out (covariance)
            // Covariance (out) - more derived type assign to less derived. It preserves assignment compatibility.
            // Contrvariance (in) - less derived type assign to more derived. It reverses assignment compatibility.

            Action<Employee> updateEmployee = updateMethod;

            updateEmployee(new Employee());

            void updateMethod(Person employee)
            {
                Console.WriteLine(employee);
            }
        }
    }
}
