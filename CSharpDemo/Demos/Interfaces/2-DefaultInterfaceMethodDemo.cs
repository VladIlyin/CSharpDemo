using CSharpDemo.DemoRunner;

namespace CSharpDemo.Demos.Interfaces;

public partial class InterfaceDemo
{
    [DemoCaption("Demo 2")]
    public void Demo2()
    {
        IInterfaceA2 a2 = new A2();
        a2.Method();

        var b2 = new B2();
        b2.Method();

        IInterfaceA2 ib2 = new B2();
        ib2.Method();

        /*
         * I am default method
         * I am method from class B2
         * I am IInterfaceA.Method from class B2
         */
    }

    interface IInterfaceA2
    {
        public void Method()
        {
            Console.WriteLine($"I am default method");
        }
    }

    public class A2 : IInterfaceA2 { }

    public class B2 : IInterfaceA2
    {
        public void Method()
        {
            Console.WriteLine("I am method from class B2");
        }

        void IInterfaceA2.Method()
        {
            Console.WriteLine("I am IInterfaceA.Method from class B2");
        }
    }
}