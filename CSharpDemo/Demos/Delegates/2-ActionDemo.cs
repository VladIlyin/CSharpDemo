using CSharpDemo.Helpers;

namespace CSharpDemo.Demos.Delegates
{
    public partial class DelegateDemo
    {
        [DemoCaption("Action demo: print sum of two numbers")]
        public void Demo7()
        {
            Action<int, int> sumTwo = (int a, int b) => Console.WriteLine(a + b);

            // 10
            sumTwo(5, 5);
        }

        [DemoCaption("Action demo: covariance and contrvariance")]
        public void Demo8()
        {
            // Action - input marked as in (contravariant), output marked as out (covariance)

            void UpdatePerson(Person employee)
            {
                Console.WriteLine(employee.Name);
            }

            Action<Employee> update = UpdatePerson;

            update(new Employee() { Name = "Neo" });

            /*
             * Output:
             * Neo
             */
        }
    }
}
