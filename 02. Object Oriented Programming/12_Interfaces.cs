// ● Interface: A contract. It has only a list of methods (what to do), not their code (how to do).
//              A class that implements an interface promises to write all of its methods.
//              e.g., interface IFlyable { void Fly(); } -> "Anyone who is IFlyable must have a Fly() method."
//   Naming: Interface names start with capital I. e.g., IFlyable, ISwimmable

// ● Why do we need interfaces?
//   1. Multiple Inheritance: A class can inherit only ONE class, but it can implement MANY interfaces.
//   2. Unrelated classes can share the same ability. e.g., A Bird and an Airplane both can Fly(),
//      but an Airplane is not a Bird. So they can't share a parent class, but they can share an interface.
//      Inheritance is an "is-a" relationship (Dog is an Animal). Interface is a "can-do" relationship (Bird can fly).

// ● Diamond Problem: Why C# doesn't allow multiple inheritance with classes.
//
//            Animal (Speak)
//            /           \
//      Dog (Speak)    Cat (Speak)
//            \           /
//              DogCat         -> DogCat.Speak()... which one? Dog's or Cat's?
//
//   This confusion is called the Diamond Problem. Interfaces have no code inside (only method names),
//   so even if two interfaces have the same method, the class writes it only once. No confusion.

// ● Rules:
//   1. Interface members are public by default.
//   2. An interface can't have fields or constructors.
//   3. You can't create an object of an interface. e.g., new IFlyable() -> Error
//   4. The class must write all methods of the interface.
//   5. A class can inherit one class and many interfaces. The class name comes first:
//      class Duck : Animal, IFlyable, ISwimmable

// ● Abstract Class vs Interface:
//
//                           Abstract Class                       Interface
//   Methods                 Abstract and normal (with body)      Only method names (no body)
//   Fields                  Yes                                  No
//   Constructor             Yes                                  No
//   Multiple inheritance    No (only one parent class)           Yes (many interfaces)
//   Relationship            "is-a" (Dog is an Animal)            "can-do" (Bird can fly)
//   Use when                Classes are closely related and      Unrelated classes need the same ability
//                           share common code


interface IFlyable
{
	void Fly();
}

interface ISwimmable
{
	void Swim();
}

class Animal
{
	public void Eat()
	{
		Console.WriteLine("Eating");
	}
}

class Bird : IFlyable
{
	public void Fly()
	{
		Console.WriteLine("Bird flies with wings");
	}
}

class Airplane : IFlyable
{
	public void Fly()
	{
		Console.WriteLine("Airplane flies with engines");
	}
}

// One class + two interfaces
class Duck : Animal, IFlyable, ISwimmable
{
	public void Fly()
	{
		Console.WriteLine("Duck flies a little");
	}

	public void Swim()
	{
		Console.WriteLine("Duck swims in water");
	}
}

class Program
{
	static void Main()
	{
		// IFlyable f = new IFlyable();   // Error: can't create an object of an interface

		// Bird, Airplane and Duck are not related, but all of them can fly
		IFlyable[] flyers = { new Bird(), new Airplane(), new Duck() };
		foreach (IFlyable flyer in flyers)
		{
			flyer.Fly();
		}
		// o/p: "Bird flies with wings"
		//      "Airplane flies with engines"
		//      "Duck flies a little"

		Duck duck = new Duck();
		duck.Eat();    // o/p: "Eating"                (from Animal class)
		duck.Swim();   // o/p: "Duck swims in water"   (from ISwimmable interface)
	}
}
