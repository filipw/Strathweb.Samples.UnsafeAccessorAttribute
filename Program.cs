using System.Runtime.CompilerServices;

namespace UnsafeAccessorSample;

public static class Program
{
    public static void Main()
    {
        var dog = new Dog();
        dog.Bark(2);
        
        DogNameField(dog) = "Minnie";
        dog.Bark(2);

        CallDogMeowMethod(dog, 2);

        var anotherDog = CallDogConstructor("Mickey");
        anotherDog.Bark(2);
    }
    
    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_name")]
    private static extern ref string DogNameField(Dog dog);
    
    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "Meow")]
    private static extern void CallDogMeowMethod(Dog dog, int times);
    
    [UnsafeAccessor(UnsafeAccessorKind.Constructor)]
    private static extern Dog CallDogConstructor(string name);
}

class Dog
{
    private string _name = "Pluto";

    private Dog(string name)
    {
        _name = name ?? throw new ArgumentNullException(nameof(name));
    }
    
    public Dog() {}

    private void Meow(int times)
    {
        Console.WriteLine($"{_name} meows!");
        for (var i = 0; i < times; i++)
        {
            Console.Write("Meow! ");
        }
        Console.WriteLine();
    }

    public void Bark(int times)
    {
        Console.WriteLine($"{_name} barks!");
        for (var i = 0; i < times; i++)
        {
            Console.Write("Woof! ");
        }
        Console.WriteLine();
    }
}