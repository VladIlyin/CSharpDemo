using CSharpDemo.DemoRunner;

namespace CSharpDemo.Demos.CovarianceContrvariance;

public interface IAnimal
{
    public void Eat() => Console.WriteLine("I'm eating");
    public void Say();
}

public class Dog : IAnimal
{
    public void Say() => Console.WriteLine("Woof");
}

public class Cat : IAnimal
{
    public void Say() => Console.WriteLine("Meow");
}

public class Sheep : IAnimal
{
    public void Say() => Console.WriteLine("Baa");
}

public interface IZoo<out T> { }
public class AnimalZoo<T> : IZoo<T> { }

/// <summary>
/// Covariance and contravariance enable implicit reference conversion for array types, delegate types, and generic type arguments.
/// Covariance preserves assignment compatibility and contravariance reverses it.
/// </summary>
public class CovarianceContravarianceDemo : DemoRunner<CovarianceContravarianceDemo>
{
    [DemoCaption("Arrays: co-variant array conversion")]
    public void Demo1()
    {
        IAnimal[] animals = new Dog[3] { new(), new(), new() };
        animals[0].Eat(); // I'm eating

        // not type safe - error:
        // System.ArrayTypeMismatchException: Attempted to access an element as a type incompatible with the array.
        // animals[0] = new Cat();
    }

    [DemoCaption("Delegates: Func<out TResult> co-variant, Action<in T> contra-variant, Func<in T, out TResult>")]
    public void Demo2()
    {
        // Covariance. A delegate specifies a return type as object,  
        // but you can assign a method that returns a string.
        Func<object> del = () => "Covariance";
            
        Console.WriteLine(del()); //Covariance

        // Contravariance. A delegate specifies a parameter type as string,  
        // but you can assign a method that takes an object.  
        var printObj = (object obj) => Console.WriteLine(obj);
        Action<string> del2 = printObj;
            
        del2("Contravariance"); // Contravariance
    }

    [DemoCaption("Delegates: Func<in T, out TResult>")]
    public void Demo21()
    {
        var animalToSheep = (IAnimal animal) => new Sheep();

        // Func<in T, out TResult>
        Func<Dog, IAnimal> DogToSheep = animalToSheep;
        Func<Cat, IAnimal> CatToSheep = animalToSheep;

        DogToSheep(new Dog()).Say(); // Baa
        CatToSheep(new Cat()).Say(); // Baa
    }

    [DemoCaption("Generic interface: IEnumerable<out T> covariance")]
    public void Demo3()
    {
        // Generic interface IEnumerable is covarint
        IEnumerable<IAnimal> animals = new List<Cat>() { new(), new(), new() };

        animals.ToList().ForEach(an => an.Say());
    }
        
    [DemoCaption("Generic interface: IComparer<in T> covariance")]
    public void Demo31()
    {
        // Generic interface IComparer is contravarint
        IComparer<Dog> dogComparer = new AnimalComparer();

        var dogs = new Dog[] { new(), new(), new() };
        Array.Sort(new Dog[] { new(), new (), new() }, dogComparer);

        ConsoleHelper.WriteCollection(dogs.Select(d => d.GetHashCode()));
    }

    public class AnimalComparer : IComparer<IAnimal>
    {
        public int Compare(IAnimal? x, IAnimal? y)
        {
            return x.GetHashCode().CompareTo(y.GetHashCode());
        }
    }

    [DemoCaption("Generic interface: creating Variant Generic Interfaces")]
    public void Demo32()
    {
        /*
         * You can declare variant generic interfaces
         * by using the in and out keywords for generic type parameters
         */

        // Dog more derived type than originally specified
        // Covariance keeps assign compatibility (IZoo<out T>)
        IZoo<IAnimal> zoo = new AnimalZoo<Dog>();
    }

    [DemoCaption("Value types do not support variance")]
    public void Demo4()
    {
        IEnumerable<int> list = new List<int>();

        // error, because int is a value type
        // IEnumerable<object> objects = list;
    }
}