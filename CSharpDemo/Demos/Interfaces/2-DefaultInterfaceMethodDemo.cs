using CSharpDemo.Helpers;

namespace CSharpDemo.Demos.Interfaces
{
    public class DefaultInterfaceMethodDemo : DemoRunner<DefaultInterfaceMethodDemo>
    {
        [DemoCaption("Demo 1")]
        public void Demo1()
        {
            IInterfaceA ia = new A();
            ia.Method(); // I am from IInterfaceA

            IInterfaceA ib = new B();
            ib.Method(); // I am IInterfaceA.Method

            B b = new B();
            b.Method(); // I am Method
        }

        interface IInterfaceA
        {
            public void Method() 
                => Console.WriteLine($"I am from {nameof(IInterfaceA)}");
        }

        public class A : IInterfaceA
        { 
            // It's not necessary to implement dafault interface method here
        }

        public class B : IInterfaceA
        {
            public void Method() 
                => Console.WriteLine("I am Method");

            void IInterfaceA.Method()
                => Console.WriteLine("I am IInterfaceA.Method");
        }
    }
}
