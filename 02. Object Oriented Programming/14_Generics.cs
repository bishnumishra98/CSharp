// ● Problem: We want a Box class that can store one item of any type: int, string, double...
//   Option 1: Write IntBox, StringBox, DoubleBox... The same code is repeated for every type.
//   Option 2: Use the 'object' type, because it can store anything. But:
//             I.  We must cast every time we take the item out: int n = (int)box.Item;
//             II. No safety: We can put a string inside and take it out as an int by mistake.
//                 The compiler won't stop us, and the program crashes while running.
//
// ● Solution: Generics. Write the class once with a placeholder type <T>, and give the real type while using it.
//      class Box<T> { public T Item; }
//      Box<int> intBox = new Box<int>();          -> T becomes int
//      Box<string> stringBox = new Box<string>(); -> T becomes string
//   You have already used generics: List<int>, List<string>.

// ● T is just a name. You can use any name, but T (Type) is the usual one. For 2 types, use 2 names: <TKey, TValue>

// ● Generic Method: A method that works with any type. e.g., Swap<T>(ref T a, ref T b)
//   Usually, you don't need to write the type while calling it. The compiler finds it from the arguments.

// ● Constraint (where): Puts a rule on what T can be.
//   e.g., To find the bigger of two values, T must be something that can be compared. So:
//      T GetMax<T>(T a, T b) where T : IComparable<T>    -> "T must be comparable"
//   int, double and string are all comparable.


// Generic class
class Box<T>
{
	public T Item { get; set; }

	public Box(T item)
	{
		Item = item;
	}

	public void Show()
	{
		Console.WriteLine("Box has: " + Item);
	}
}

// Generic class with 2 types
class Pair<TKey, TValue>
{
	public TKey Key { get; set; }
	public TValue Value { get; set; }

	public Pair(TKey key, TValue value)
	{
		Key = key;
		Value = value;
	}
}

class Program
{
	// Generic method
	static void Swap<T>(ref T a, ref T b)
	{
		T temp = a;
		a = b;
		b = temp;
	}

	// Generic method with a constraint
	static T GetMax<T>(T a, T b) where T : IComparable<T>
	{
		if (a.CompareTo(b) > 0)   // CompareTo() gives a number > 0 if a is bigger than b
			return a;
		return b;
	}

	static void Main()
	{
		Box<int> intBox = new Box<int>(10);
		Box<string> stringBox = new Box<string>("Hello");

		intBox.Show();      // o/p: "Box has: 10"
		stringBox.Show();   // o/p: "Box has: Hello"
		// intBox.Item = "text";   // Error: Box<int> can store only int

		Pair<string, int> marks = new Pair<string, int>("Ravi", 90);
		Console.WriteLine(marks.Key + ": " + marks.Value);   // o/p: "Ravi: 90"

		int x = 1, y = 2;
		Swap(ref x, ref y);   // compiler finds T = int by itself
		Console.WriteLine("x = " + x + ", y = " + y);   // o/p: "x = 2, y = 1"

		Console.WriteLine(GetMax(10, 20));             // o/p: 20
		Console.WriteLine(GetMax("apple", "mango"));   // o/p: "mango"   (alphabetically bigger)
	}
}
