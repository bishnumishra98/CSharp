// ● Method Overriding: A child class writes its own version of a parent's method.
//                      Same name, same parameters, different work.
//                      e.g., Every animal speaks, but a Dog says "Woof" and a Cat says "Meow".
//
// ● Keywords:
//   1. virtual:  Written in the parent method. Means "children may override me".
//   2. override: Written in the child method.  Means "I am replacing the parent's version".
//   3. base:     base.Speak() calls the parent's version from inside the child.

// ● Runtime Polymorphism:
//      Animal a = new Dog();   // A parent variable can hold a child object, because a Dog IS an Animal. (Upcasting)
//      a.Speak();              // Dog's Speak() runs.
//   C# looks at the real object (Dog) while the program runs, not the variable type (Animal).
//
// ● Why is it useful?
//   Problem:  if (animal is Dog) ... else if (animal is Cat) ...   -> for every new animal, change this code again.
//   Solution: animal.Speak();                                      -> each object runs its own version by itself.

// ● Good to know:
//   1. sealed override: No further child can override this method.   sealed class: No class can inherit it.
//   2. new (Method Hiding): Child writes the same method with 'new' instead of 'override'.
//      Then the variable type decides which one runs, not the object. Rarely used. Prefer override.
//   3. Downcasting: Parent variable back to child type. Check first with 'is':  if (a is Dog d) { d.Speak(); }


class Animal
{
	public virtual void Speak()   // virtual: children may override
	{
		Console.WriteLine("Animal makes a sound");
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
}

class Program
{
	static void Main()
	{
		// 1. Upcasting + Runtime Polymorphism
		Animal a = new Dog();   // Animal variable, Dog object
		a.Speak();              // o/p: "Dog says Woof"   (real object is Dog)

		// 2. One loop works for every animal
		Animal[] animals = { new Dog(), new Cat() };
		foreach (Animal animal in animals)
		{
			animal.Speak();
		}
		// o/p: "Dog says Woof"
		//      "Animal makes a sound"
		//      "Cat says Meow"
	}
}
