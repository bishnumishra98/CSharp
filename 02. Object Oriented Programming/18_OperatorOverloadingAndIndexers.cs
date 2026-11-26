// ----------------------------------------------------------------------------------------------------------------------------------
// 1. Operator Overloading
// ----------------------------------------------------------------------------------------------------------------------------------

// ● Problem: + works for int (2 + 3), but not for our own classes. If p1 and p2 are Points, p1 + p2 gives an error.
//            So we have to write a method and call p1.Add(p2), which doesn't look as natural as p1 + p2.
//
// ● Operator Overloading: Giving a meaning to operators (+, -, *, ==, etc.) for our own classes.
//   Syntax:
//      public static Point operator +(Point a, Point b) { ... }
//   Rules:
//   1. It must be public and static.
//   2. At least one parameter must be of our class type.
//   3. Some operators must be overloaded in pairs: == with !=, < with >.
//      (If you overload ==, also override Equals() and GetHashCode(). See 10_ObjectClassMethods.cs)
//   Use it only when the meaning is obvious. e.g., Adding two Points or two Money objects.


// ----------------------------------------------------------------------------------------------------------------------------------
// 2. Indexers
// ----------------------------------------------------------------------------------------------------------------------------------

// ● Problem: A Classroom class has a private array of students. To get a student, we write classroom.GetStudent(0).
//
// ● Indexer: Lets us use an object like an array, with []. e.g., classroom[0]
//   Syntax:
//      public string this[int index]
//      {
//          get { return students[index]; }
//          set { students[index] = value; }
//      }
//   You already used indexers: list[0], str[0] and dictionary["key"] all work using indexers.


class Point
{
	public int X { get; set; }
	public int Y { get; set; }

	public Point(int x, int y)
	{
		X = x;
		Y = y;
	}

	public static Point operator +(Point a, Point b)
	{
		return new Point(a.X + b.X, a.Y + b.Y);
	}

	public static Point operator -(Point a, Point b)
	{
		return new Point(a.X - b.X, a.Y - b.Y);
	}

	public override string ToString()
	{
		return "(" + X + ", " + Y + ")";
	}
}

class Classroom
{
	private string[] students = new string[3];

	// Indexer
	public string this[int index]
	{
		get { return students[index]; }
		set { students[index] = value; }
	}
}

class Program
{
	static void Main()
	{
		// 1. Operator Overloading
		Point p1 = new Point(1, 2);
		Point p2 = new Point(3, 4);

		Point sum = p1 + p2;          // calls operator +
		Point difference = p2 - p1;   // calls operator -

		Console.WriteLine(sum);          // o/p: "(4, 6)"
		Console.WriteLine(difference);   // o/p: "(2, 2)"

		// 2. Indexer
		Classroom classroom = new Classroom();
		classroom[0] = "Ravi";    // calls set
		classroom[1] = "Priya";

		Console.WriteLine(classroom[0]);   // calls get. o/p: "Ravi"
		Console.WriteLine(classroom[1]);   // o/p: "Priya"
	}
}
