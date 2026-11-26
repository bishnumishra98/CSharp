// ● Interface: A contract. It lists methods (what to do), but not their code (how to do).
//              A class that implements it promises to write all those methods.
//              e.g., interface IFlyable { void Fly(); } -> "Whoever is IFlyable must have a Fly() method."
//   Naming: Starts with capital I. e.g., IFlyable, ISwimmable

// ● Why do we need interfaces?
//   1. A class can inherit only ONE class, but it can implement MANY interfaces.
//   2. Unrelated classes can share an ability. e.g., A Bird and an Airplane both can Fly(), but an Airplane is not a Bird.
//      Inheritance = "is-a" (Dog is an Animal).   Interface = "can-do" (Bird can fly).

// ● Why only ONE parent class? (Diamond Problem)
//
//            Animal
//           /      \
//        Dog        Cat        <- both have their own Speak()
//           \      /
//            DogCat            <- DogCat.Speak(): Dog's or Cat's? Confusing!
//
//   Interfaces have no code, so the class writes Speak() itself, only once. No confusion.

// ● Rules:
//   1. You can't create an object of an interface. (new IFlyable() -> Error)
//   2. No fields, no constructors. Members are public by default.
//   3. The class must write all methods of the interface.
//   4. Parent class first, then interfaces:  class Duck : Animal, IFlyable, ISwimmable

// ● Abstract Class vs Interface:
//
//                           Abstract Class                     Interface
//   Create object           No                                 No
//   Methods                 Abstract + normal (with body)      Only method names (*)
//   Fields, Constructor     Yes                                No
//   Multiple inheritance    No (only one parent class)         Yes (many interfaces)
//   Relationship            "is-a" (Dog is an Animal)          "can-do" (Bird can fly)
//   Use when                Related classes share code         Unrelated classes share an ability
//
//   (*) From C# 8, an interface method can also have a default body. But mostly, it has only names.


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
