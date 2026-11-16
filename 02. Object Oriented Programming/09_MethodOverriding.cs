// ● Method Overriding: A child class writes its own version of a method that is already in the parent class.
//                      Same name, same parameters, different work.
//                      e.g., Every animal speaks, but a Dog says "Woof" and a Cat says "Meow".

// ● Keywords:
//   1. virtual:      Written in the parent method. Means "child classes are ALLOWED to override this method".
//   2. override:     Written in the child method. Means "I am replacing the parent's method".
//   3. base.Method(): Calls the parent's version of the method from inside the child.
//   4. sealed:       I.  sealed override: No further child can override this method again.
//                    II. sealed class:    No class can inherit this class.

// ● Upcasting: A parent type variable can hold a child object, because a Dog IS an Animal.
//      Animal a = new Dog();
//
// ● Runtime Polymorphism: When we call a.Speak(), which Speak() runs? Dog's.
//   C# checks the actual object (Dog) while the program is running, not the variable type (Animal).
//
// ● Why is it useful?
//   Problem: Without it, we must check every type ourselves:
//      if (animal is Dog) ... else if (animal is Cat) ... else if (animal is Lion) ...
//   And for every new animal, we must come back and change this code.
//   Solution: Just call animal.Speak(). Each object runs its own version automatically.

// ● new keyword (Method Hiding): If the child writes a method with the same name WITHOUT override, and uses 'new',
//   it only hides the parent's method. Then the variable type decides which method runs, not the object.
//   This is rarely used. In most cases, you want override.

// ● Downcasting: Converting a parent type variable back to the child type. It may fail, so check first:
//   I.  is: if (a is Dog d) -> checks if a is a Dog, and if yes, gives it as d.
//   II. as: Dog? d = a as Dog; -> gives null if a is not a Dog (no crash).


class Animal
{
	public virtual void Speak()   // virtual: can be overridden
	{
		Console.WriteLine("Animal makes a sound");
	}

	public void Sleep()           // not virtual
	{
		Console.WriteLine("Animal is sleeping");
	}
}

class Dog : Animal
{
	public override void Speak()
	{
		Console.WriteLine("Dog says Woof");
	}
}

class Cat : Animal
{
	public override void Speak()
	{
		base.Speak();   // first runs Animal's Speak()
		Console.WriteLine("Cat says Meow");
	}

	public new void Sleep()   // new: hides Animal's Sleep(), does NOT override it
	{
		Console.WriteLine("Cat is sleeping");
	}
}

class Lion : Animal
{
	public sealed override void Speak()   // children of Lion can't override Speak() again
	{
		Console.WriteLine("Lion says Roar");
	}
}

sealed class BabyLion : Lion   // sealed class: no class can inherit BabyLion
{
	// public override void Speak() { }   // Error: Speak() is sealed in Lion
}

// class Cub : BabyLion { }               // Error: can't inherit from a sealed class

class Program
{
	static void Main()
	{
		// 1. Overriding
		Dog dog = new Dog();
		dog.Speak();   // o/p: "Dog says Woof"

		// 2. Upcasting + Runtime Polymorphism
		Animal a1 = new Dog();   // Animal variable, Dog object
		a1.Speak();              // o/p: "Dog says Woof"   (actual object is Dog)

		// 3. Power of polymorphism: one loop works for every animal
		Animal[] animals = { new Dog(), new Cat(), new Lion() };
		foreach (Animal animal in animals)
		{
			animal.Speak();
		}
		// o/p: "Dog says Woof"
		//      "Animal makes a sound"
		//      "Cat says Meow"
		//      "Lion says Roar"

		// 4. new (Method Hiding): the variable type decides
		Cat cat = new Cat();
		cat.Sleep();       // o/p: "Cat is sleeping"
		Animal a2 = cat;
		a2.Sleep();        // o/p: "Animal is sleeping"   (variable type is Animal)

		// 5. Downcasting
		Animal a3 = new Dog();
		// Dog d = a3;     // Error: Animal can't be converted to Dog automatically

		if (a3 is Dog d)
		{
			d.Speak();     // o/p: "Dog says Woof"
		}

		Cat? c = a3 as Cat;            // a3 is a Dog, not a Cat, so c becomes null
		Console.WriteLine(c == null);  // o/p: True
	}
}
