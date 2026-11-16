// ● object class: In C#, every class automatically inherits from the 'object' class (System.Object).
//                 It is the parent of all types. So every object gets these methods for free:
//   1. ToString():    Returns the object as a string.
//                     Default: returns only the class name. e.g., "Student" (not useful)
//                     Console.WriteLine(obj) calls obj.ToString() automatically.
//   2. Equals():      Checks if two objects are equal.
//                     Default: for classes, it checks if both variables point to the SAME object, not the same data.
//   3. GetHashCode(): Returns a number (hash code) for the object. Dictionary and HashSet use it to find objects fast.
//                     Rule: If two objects are Equal, they must have the same hash code.
//                     So whenever you override Equals(), override GetHashCode() too.
//   4. GetType():     Returns the type (class name) of the object. It can't be overridden.

// ● Problem: Two students with the same RollNo and Name are the same student, but Equals() says False,
//            and printing a student shows just "StudentDefault".
//   Solution: Override ToString(), Equals() and GetHashCode().


// Without overriding
class StudentDefault
{
	public int RollNo { get; set; }
	public string Name { get; set; } = "";
}

// With overriding
class Student
{
	public int RollNo { get; set; }
	public string Name { get; set; } = "";

	public override string ToString()
	{
		return "RollNo: " + RollNo + ", Name: " + Name;
	}

	// object? obj means obj can be any object (or null)
	public override bool Equals(object? obj)
	{
		if (obj is Student other)   // check if obj is a Student
			return RollNo == other.RollNo && Name == other.Name;
		return false;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(RollNo, Name);   // makes one hash code from RollNo and Name
	}
}

class Program
{
	static void Main()
	{
		StudentDefault d1 = new StudentDefault { RollNo = 1, Name = "Ravi" };
		StudentDefault d2 = new StudentDefault { RollNo = 1, Name = "Ravi" };

		Console.WriteLine(d1);              // o/p: "StudentDefault"   (only the class name)
		Console.WriteLine(d1.Equals(d2));   // o/p: False   (d1 and d2 are 2 different objects)

		Student s1 = new Student { RollNo = 1, Name = "Ravi" };
		Student s2 = new Student { RollNo = 1, Name = "Ravi" };

		Console.WriteLine(s1);                                     // o/p: "RollNo: 1, Name: Ravi"
		Console.WriteLine(s1.Equals(s2));                          // o/p: True   (same data)
		Console.WriteLine(s1.GetHashCode() == s2.GetHashCode());   // o/p: True
		Console.WriteLine(s1.GetType());                           // o/p: "Student"

		// HashSet keeps only unique items. It uses Equals() and GetHashCode() to find duplicates.
		HashSet<Student> students = new HashSet<Student> { s1, s2 };
		Console.WriteLine(students.Count);   // o/p: 1   (s2 is a duplicate of s1)

		// Note: == still checks if both point to the same object.
		// To change ==, use operator overloading (see 18_OperatorOverloadingAndIndexers.cs).
		Console.WriteLine(s1 == s2);   // o/p: False
	}
}
