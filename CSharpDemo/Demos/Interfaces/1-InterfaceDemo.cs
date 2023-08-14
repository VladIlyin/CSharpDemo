using CSharpDemo.Helpers;

namespace CSharpDemo.Demos.Interfaces
{
    public class InterfaceDemo : DemoRunner<InterfaceDemo>
    {
        [DemoCaption("Demo 1")]
        public void Demo1()
        {
            ITestChild tch = new A();

            // Old plain public member
            tch.PublicMethod();

            // Class does not know anything
            // about the default method of an interface,
            // so we use interface type variable
            tch.PublicMethodWithDefault();

            // It's not allowed to call protected interface method directly
            // Protected interface member is allowed only within interface hierarchy
            // So, we call protected method
            // from deafult public method of ITestChild interface
            tch.ProtectedMethodCallHere();

            // Internal method call (implemented explicitly)
            tch.Internal();

            // Call public method
            A a = new A();
            a.PublicMethod();
        }

        public interface ITest
        {
            protected void Protected();
            internal void Internal();
            public void PublicMethod();
            public void PublicMethodWithDefault()
            {
                // Private interface methods are accessible
                // from within interface,
                // e.g. in public default methods
                PrivateMethod();
                Console.WriteLine(PrivateProp);
            }
            private void PrivateMethod()
            {
                Console.WriteLine($"   I am private method");
            }
            private string PrivateProp { get => "   I am private property"; }
            public static void StaticMethod()
            {
                Console.WriteLine($"I am static method");
            }
        }

        public interface ITestChild : ITest
        {
            public void ProtectedMethodCallHere()
            {
                Protected();
            }
        }

        public class A : ITestChild
        {
            public void PublicMethod()
            {
                Console.WriteLine($"I am public method");
            }

            void ITest.Internal()
            {
                Console.WriteLine($"I am ITest.Internal");
            }

            void ITest.Protected()
            {
                Console.WriteLine($"I am ITest.Protected");
            }
        }
    }
}
