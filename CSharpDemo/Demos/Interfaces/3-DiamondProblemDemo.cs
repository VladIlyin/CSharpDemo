using CSharpDemo.Helpers;
using System.Runtime.CompilerServices;

namespace CSharpDemo.Demos.Interfaces
{
    public class DiamondProblemDemo : DemoRunner<DiamondProblemDemo>
    {
        [DemoCaption("Demo 1")]
        public void Demo1()
        {
            IInterfaceA a = new D();
            a.Method(); // I am from class D

            IInterfaceB b = new D();
            b.Method(); // I am from class D


            IInterfaceC c = new D();
            c.Method(); // I am from class D
        }

        interface IInterfaceA
        {
            void Method()
            {
            }
        }

        interface IInterfaceB : IInterfaceA
        {
            void IInterfaceA.Method() => Console.WriteLine("I am From Interface B");
        }

        interface IInterfaceC : IInterfaceA
        {
            void IInterfaceA.Method() => Console.WriteLine("I am From Interface C");
        }

        class D : IInterfaceB, IInterfaceC
        {
            // Compiler uses the most specific override,
            // which is defined in the class D.
            void IInterfaceA.Method() => Console.WriteLine("I am from class D");
        }
    }
}
