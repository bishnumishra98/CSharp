// ● Problem: Dog and Cat both have Name, Eat() and Sleep(). Without inheritance, we write this same code in both classes.
//            If we have 10 animals, we repeat it 10 times. And if Eat() needs a change, we change it in 10 places.
//
//      class Dog { Name, Eat(), Sleep(), Bark() }
//      class Cat { Name, Eat(), Sleep(), Meow() }
//
// ● Inheritance: A class (child) gets all the fields and methods of another class (parent). So common code is written
//                only once in the parent class, and child classes reuse it.
//      class Animal { Name, Eat(), Sleep() }
//      class Dog : Animal { Bark() }    -> Dog gets Name, Eat(), Sleep() from Animal
//      class Cat : Animal { Meow() }
//
// ● Parent class is also called Base class. Child class is also called Derived class.
// ● Inheritance means an "is-a" relationship: Dog is an Animal. Cat is an Animal.

// ● Types of Inheritance:
//   1. Single:       One child, one parent.                 Animal -> Dog
//   2. Multilevel:   Child of a child.                      Animal -> Dog -> Puppy
//   3. Hierarchical: Many children of one parent.           Animal -> Dog, Animal -> Cat
//   4. Multiple:     One child, many parents.               (Dog, Pet) -> Puppy
//                    NOT allowed with classes in C#. It is done using interfaces (see 12_Interfaces.cs).

// ● base keyword: Used by the child class to talk to its parent class.
//   I.  : base(...)    calls the parent's constructor.
//   II. base.Method()  calls the parent's method (see 09_MethodOverriding.cs).

// ● Constructor order: When a child object is created, the parent constructor runs first, then the child constructor.
//   (A child can't exist before its parent.)

// ● Note: Constructors are not inherited. Private members are inherited, but the child can't use them directly.
//         Use protected for members that only child classes should use.


class Animal   // Parent class
{
	public string Name { get; set; }
	protected int Legs = 4;   // protected: usable in Animal and its child classes only

	public Animal(string name)
	{
		Name = name;
		Console.WriteLine("Animal constructor called");
	}

	public void Eat()
	{
		Console.WriteLine(Name + " is eating");
	}
}

// Single inheritance: Animal -> Dog
class Dog : Animal
{
	// : base(name) sends name to the Animal constructor
	public Dog(string name) : base(name)
	{
		Console.WriteLine("Dog constructor called");
	}

	public void Bark()
	{
		Console.WriteLine(Name + " is barking");   // Name came from Animal
	}

	public void ShowLegs()
	{
		Console.WriteLine(Name + " has " + Legs + " legs");   // child class can use protected Legs
	}
}

// Hierarchical inheritance: Animal -> Dog and Animal -> Cat
class Cat : Animal
{
	public Cat(string name) : base(name) { }

	public void Meow()
	{
		Console.WriteLine(Name + " says Meow");
	}
}

// Multilevel inheritance: Animal -> Dog -> Puppy
class Puppy : Dog
{
	public Puppy(string name) : base(name) { }

	public void Play()
	{
		Console.WriteLine(Name + " is playing");
	}
}

class Program
{
	static void Main()
	{
		Dog dog = new Dog("Tommy");
		// o/p: "Animal constructor called"
		//      "Dog constructor called"     (parent constructor runs first)

		dog.Eat();        // o/p: "Tommy is eating"   (from Animal)
		dog.Bark();       // o/p: "Tommy is barking"  (its own method)
		dog.ShowLegs();   // o/p: "Tommy has 4 legs"
		// dog.Legs = 3;  // Error: Legs is protected, can't be used outside

		Cat cat = new Cat("Kitty");   // o/p: "Animal constructor called"
		cat.Eat();                    // o/p: "Kitty is eating"
		cat.Meow();                   // o/p: "Kitty says Meow"

		Puppy puppy = new Puppy("Bruno");
		// o/p: "Animal constructor called"
		//      "Dog constructor called"

		puppy.Eat();    // o/p: "Bruno is eating"    (from Animal)
		puppy.Bark();   // o/p: "Bruno is barking"   (from Dog)
		puppy.Play();   // o/p: "Bruno is playing"   (its own method)
	}
}
