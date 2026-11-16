// ● Normally, every object has its own copy of fields. These are called instance members (they belong to an object).
//
// ● static: A static member belongs to the class itself, not to any object.
//           There is only ONE copy of it, shared by all objects. It is used with the class name: ClassName.Member
//
// ● Problem: We want to count how many students are created. If we use a normal field, each object has its own
//            count, so the count never goes above 1.
//   Solution: Make the count static, so all objects share one single count.

// ● Types of static members:
//   1. static field:       Data shared by all objects. e.g., total number of students, school name.
//   2. static method:      Can be called without creating an object. e.g., Math.Max(2, 5)
//                          It can't use normal (instance) fields directly, because there is no object.
//   3. static constructor: Runs only ONCE, automatically, before the class is used for the first time.
//                          Used to give values to static fields. It has no parameters and no access modifier.
//   4. static class:       A class that has only static members. You can't create its object.
//                          e.g., Math and Console are static classes. You never write 'new Math()'.

// ● Singleton: A class which allows only ONE object to be created in the whole program.
//              e.g., A school has only one Principal. Made using a private constructor + a static field.


class Student
{
	public string Name;
	public int Count = 0;                  // normal field: each object has its own copy
	public static int TotalStudents = 0;   // static field: only one copy, shared by all objects
	public static string SchoolName;

	// static constructor: runs only once
	static Student()
	{
		SchoolName = "DPS";
		Console.WriteLine("Static constructor called");
	}

	public Student(string name)
	{
		Name = name;
		Count++;
		TotalStudents++;
	}

	// static method
	public static void ShowTotal()
	{
		Console.WriteLine("Total students: " + TotalStudents);
		// Console.WriteLine(Name);   // Error: static method can't use Name (which student's Name?)
	}
}

// static class
static class Calculator
{
	public static int Add(int a, int b)
	{
		return a + b;
	}
}

// Singleton
class Principal
{
	private static Principal? instance;   // stores the one and only object
	public string Name = "Mr. Sharma";

	private Principal() { }   // private: no one can write 'new Principal()' outside this class

	public static Principal GetInstance()
	{
		if (instance == null)
			instance = new Principal();   // object is created only the first time
		return instance;
	}
}

class Program
{
	static void Main()
	{
		Student s1 = new Student("Ravi");    // o/p: "Static constructor called"   (only once, before first use)
		Student s2 = new Student("Priya");
		Student s3 = new Student("Aman");

		Console.WriteLine(s3.Count);               // o/p: 1   (each object has its own Count)
		Console.WriteLine(Student.TotalStudents);  // o/p: 3   (one shared count)
		Student.ShowTotal();                       // o/p: "Total students: 3"
		Console.WriteLine(Student.SchoolName);     // o/p: "DPS"

		Console.WriteLine(Calculator.Add(2, 3));   // o/p: 5   (no object needed)
		// Calculator c = new Calculator();        // Error: can't create an object of a static class

		Principal p1 = Principal.GetInstance();
		Principal p2 = Principal.GetInstance();
		// Principal p3 = new Principal();         // Error: constructor is private
		Console.WriteLine(p1 == p2);               // o/p: True   (both are the same object)
	}
}
