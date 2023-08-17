using CSharpDemo.Helpers;

namespace CSharpDemo.Demos.Interfaces
{
    public partial class InterfaceDemo : DemoRunner<InterfaceDemo>
    {
        [DemoCaption(@"Interface members demo:
                        Methods: public, default public, protected, internal, private, static,
                        Property: public, private, private static")]
        public void Demo1()
        {
            IChild a = new A();

            // Class does not know anything about the default method
            // of an interface, so we use interface type variable
            a.PublicMethodDefault();

            // Public property
            Console.WriteLine(a.PublicProperty);

            // Static method call
            IParent.StaticMethod();

            // It's not allowed to call protected interface method directly
            // Protected interface member is allowed only within interface hierarchy
            // So, we call protected method from default public method of IChild interface
            a.ProtectedMethodCallHere();

            // Internal method call (implemented explicitly)
            a.Internal();

            // Old plain public member
            a.PublicMethod();
        }

        public interface IParent
        {
            /// <summary>
            /// Protected interface method
            /// </summary>
            protected void Protected()
            {
                Console.WriteLine("I am interface protected method");
            }

            /// <summary>
            /// Internal interface method
            /// </summary>
            internal void Internal();

            /// <summary>
            /// Public interface method to be implemented in derived classes
            /// </summary>
            public void PublicMethod();

            /// <summary>
            /// Default public interface method
            /// </summary>
            public void PublicMethodDefault()
            {
                /* Private interface methods are accessible from within interface,
                 * e.g. in public default methods
                 */
                PrivateMethod();
                Console.WriteLine(PrivateProperty);
                Console.WriteLine(PrivateStaticProperty);
            }

            /// <summary>
            /// Private interface method
            /// </summary>
            private void PrivateMethod()
            {
                Console.WriteLine("I am interface private method");
            }

            /// <summary>
            /// Public interface property
            /// </summary>
            public string PublicProperty => "I am interface public property";

            /// <summary>
            /// Private interface property
            /// </summary>
            private string PrivateProperty => "I am interface private property";

            /// <summary>
            /// Private static interface property
            /// </summary>
            private static string PrivateStaticProperty => "I am interface private static property";

            /// <summary>
            /// Public static method
            /// </summary>
            public static void StaticMethod()
            {
                Console.WriteLine("I am interface static method");
            }
        }

        public interface IChild : IParent
        {
            public void ProtectedMethodCallHere()
            {
                Protected();
            }
        }

        public class A : IChild
        {
            public void PublicMethod()
            {
                Console.WriteLine("I am Public method of class A");
            }

            public void Internal()
            {
                Console.WriteLine("I am Internal method of class A");
            }
        }
    }
}
