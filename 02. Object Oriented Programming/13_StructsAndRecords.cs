// ● Other than class, C# gives 2 more ways to make your own types: struct and record.

// ● struct: Looks just like a class, but it is a value type (class is a reference type).
//           So when you copy a struct, the whole data is copied. Changing the copy doesn't change the original.
//           Use it for small and simple data. e.g., Point (X, Y), Color (R, G, B).
//           Fun fact: int, double, bool and DateTime are all structs.
//           A struct can't inherit another struct or class (but it can implement interfaces).

// ● record: A class made mainly for holding data.
//   Problem: In 10_ObjectClassMethods.cs, we wrote ToString(), Equals() and GetHashCode() ourselves,
//            just to print a student nicely and compare two students by their data. That's a lot of code.
//   Solution: record. In just one line, C# writes all of that for us:
//      record Person(string Name, int Age);
//   1. Properties Name and Age, and a constructor.
//   2. Equals() and == compare the data, not the reference.
//   3. A nice ToString(). e.g., "Person { Name = Ravi, Age = 20 }"
//   4. Its values can't be changed after creation. To change something, make a copy using 'with'.
//   Use it for data that should not change. e.g., data received from an API, or from a database.

// ● class vs struct vs record:
//
//                      class                  struct                 record
//   Type               Reference type         Value type             Reference type
//   On copy (b = a)    Copies the reference   Copies the whole data  Copies the reference
//   Equals()           Same object?           Same data?             Same data?
//   Inheritance        Yes                    No                     Yes (only from records)
//   Use for            Objects with           Small simple data      Data that should not change
//                      behaviour (methods)


class PointClass
{
	public int X;
	public int Y;
}

struct PointStruct
{
	public int X;
	public int Y;
}

record Person(string Name, int Age);

class Program
{
	static void Main()
	{
		// 1. class: copies the reference
		PointClass c1 = new PointClass { X = 1, Y = 2 };
		PointClass c2 = c1;
		c2.X = 100;
		Console.WriteLine(c1.X);   // o/p: 100   (c1 also changed, both point to the same object)

		// 2. struct: copies the whole data
		PointStruct s1 = new PointStruct { X = 1, Y = 2 };
		PointStruct s2 = s1;
		s2.X = 100;
		Console.WriteLine(s1.X);   // o/p: 1   (s1 didn't change, s2 is a separate copy)

		// 3. record
		Person p1 = new Person("Ravi", 20);
		Person p2 = new Person("Ravi", 20);

		Console.WriteLine(p1);          // o/p: "Person { Name = Ravi, Age = 20 }"
		Console.WriteLine(p1 == p2);    // o/p: True   (same data)

		// p1.Age = 21;                 // Error: record values can't be changed
		Person p3 = p1 with { Age = 21 };   // copy of p1, with Age changed
		Console.WriteLine(p3);          // o/p: "Person { Name = Ravi, Age = 21 }"
	}
}
