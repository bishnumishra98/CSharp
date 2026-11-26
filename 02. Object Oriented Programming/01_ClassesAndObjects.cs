// ● OOP (Object Oriented Programming): A way of writing programs using objects, just like the real world.
//   In the real world, everything is an object: a car, a student, a mobile phone. Every object has:
//   1. Data (what it has):       e.g., A car has a color and a brand.
//   2. Behaviour (what it does): e.g., A car can start and stop.

// ● Class: A class is a blueprint (design) for creating objects.
//          e.g., The design of a car drawn on paper. You can't drive the design.
//
// ● Object: An object is a real thing created from a class. Each object has its own data.
//           e.g., Real cars built from that design. One design -> many cars.
//
//      class Car (blueprint)
//        ├── car1 : Red, BMW
//        └── car2 : Black, Audi

// ● Creating an object: Use the 'new' keyword.
//      Car car1 = new Car();
//   'new' creates the object in memory, and car1 stores the reference (address) of that object.
//   Class is a reference type (see 01. C# Program/13_ValueTypesAndReferenceTypes.cs).

// ● Dot (.) operator: Used to access the data and methods of an object. e.g., car1.Color, car1.Start()

// ● 4 Pillars of OOP (each one has its own chapter):
//   1. Encapsulation: Keeping data and methods together in a class, and hiding the data from outside. (Data hiding)
//   2. Inheritance:   A class can reuse the data and methods of another class. (Reusability)
//   3. Polymorphism:  One name, many forms. The same method can behave differently.
//   4. Abstraction:   Showing only the important things and hiding the complex details.
//                     e.g., You drive a car using steering and pedals, without knowing how the engine works.

// ● Note: In OOP files, we write our classes first, and the Program class (with Main) at the bottom to use them.


class Car
{
	// Data (called fields)
	public string Color = "";   // public: can be used outside the class (see 05_EncapsulationAndAccessModifiers.cs)
	public string Brand = "";   // "" gives a starting value, so Brand is never null

	// Behaviour (called methods)
	public void Start()
	{
		Console.WriteLine(Brand + " car started");
	}
}

class Program
{
	static void Main()
	{
		Car car1 = new Car();   // object 1
		car1.Color = "Red";
		car1.Brand = "BMW";

		Car car2 = new Car();   // object 2
		car2.Color = "Black";
		car2.Brand = "Audi";

		car1.Start();   // o/p: "BMW car started"
		car2.Start();   // o/p: "Audi car started"

		// Each object has its own data
		Console.WriteLine(car1.Color);   // o/p: "Red"
		Console.WriteLine(car2.Color);   // o/p: "Black"

		// car3 is NOT a new object. It points to the same object as car1.
		Car car3 = car1;
		car3.Color = "Blue";
		Console.WriteLine(car1.Color);   // o/p: "Blue"   (car1 also changed, because both point to the same object)
	}
}
