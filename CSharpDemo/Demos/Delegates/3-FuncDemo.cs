using CSharpDemo.Helpers;

namespace CSharpDemo.Demos.Delegates
{
    public partial class DelegateDemo
    {
        [DemoCaption("Func demo: sum two numbers and return result")]
        public void Demo9()
        {
            Func<int, int, int> sumTwo = (int a, int b) => a + b;

            // 30
            Console.WriteLine(sumTwo(15, 15));
        }

        [DemoCaption("Func demo: covariance and contravariance")]
        public void Demo10()
        {
            // Func - input marked as in (contrvariant), output makred as out (covariance)

            Employee GetEmployee(Identity id)
            {
                return new Employee() { Name = "Trinity" };
            }

            Func<Guid, Person> getEmployeeById = GetEmployee;
            
            
            Console.WriteLine(getEmployeeById(new Guid()).Name);

            /*
             * Output:
             * Trinity
             */
        }
    }
}
