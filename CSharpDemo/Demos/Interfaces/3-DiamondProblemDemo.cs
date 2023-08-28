using System.Runtime.CompilerServices;
using CSharpDemo.DemoRunner;

namespace CSharpDemo.Demos.Interfaces;

public partial class InterfaceDemo
{
    [DemoCaption("Demo 3")]
    public void Demo3()
    {
        /* Compiler uses the most specific override,
         * which is defined in the class D.
         * Let's say we comment out D and C implementation of Sum method
         * then output will be 'I am from Interface B'
         */

        IInterfaceA a = new D();
        a.Sum(2, 3); // I am from class D

        IInterfaceB b = new D();
        b.Sum(3, 4); // I am from class D

        IInterfaceC c = new D();
        c.Sum(4, 5); // I am from class D
    }

    private interface IInterfaceA
    {
        int Sum(int a, int b)
        {
            Console.WriteLine("I am From Interface A");
            return a + b;
        }
    }

    private interface IInterfaceB : IInterfaceA
    {
        int IInterfaceA.Sum(int a, int b)
        {
            Console.WriteLine("I am From Interface B");
            return a + b;
        }
    }

    private interface IInterfaceC : IInterfaceA
    {
        int IInterfaceA.Sum(int a, int b)
        {
            Console.WriteLine("I am From Interface C");
            return a + b;
        }
    }

    public class D : IInterfaceB, IInterfaceC
    {
        /*
         * If there is no implementation then we have an error:
         * Interface member 'IInterfaceA.Sum' doesn't have a most specific implementation
         * So, compiler cannot choose between two implementations located on the same level.
         */

        public int Sum(int a, int b)
        {
            Console.WriteLine("I am from class D");
            return a + b;
        }
    }
}