using CSharpDemo.Helpers;

namespace CSharpDemo.Demos.CovarianceContrvariance
{

    interface IAnimal { }
    class Dog : IAnimal { }
    class Cat : IAnimal { }

    interface IZoo<out T> { }
    class AnimalZoo<T> : IZoo<T> { }

    public class GenericInterfaceCovarContrvarDemo : DemoRunner<GenericInterfaceCovarContrvarDemo>
    {
        public void Demo1()
        {
            // Generic interface IEnumerable is covarint
            IEnumerable<IAnimal> animals = new List<Cat>();

            // Dog more derived type than originally specified
            // Covariance keeps assign compatibility (T marked as out)
            IZoo<IAnimal> zoo = new AnimalZoo<Dog>();
        }
    }
}
