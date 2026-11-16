// ● Constructor: A special method that runs automatically when an object is created using 'new'.
//                It is used to give starting values to the fields of an object.
//   Rules:
//   1. Its name is the same as the class name.
//   2. It has no return type (not even void).

// ● Types of constructors:
//   1. Default Constructor: Takes no parameters.
//                           If you don't write any constructor, C# gives an empty default constructor automatically.
//                           But if you write any constructor, C# doesn't give it.
//   2. Parameterized Constructor: Takes parameters, so each object can start with different values.

// ● Constructor Overloading: A class can have many constructors with different parameters.
//                            C# calls the one which matches the arguments you pass.

// ● this keyword: 'this' means the current object.
//   I.  When a parameter has the same name as a field, 'this.x' means the field and 'x' means the parameter.
//   II. Constructor Chaining: One constructor can call another constructor of the same class using ': this(...)'.
//       Problem: In overloaded constructors, the same lines (Name = name; Age = age;) are repeated again and again.
//       Solution: Write the full code in one constructor only, and let other constructors call it.

// ● Private Constructor: If the constructor is private, no one can create an object of that class from outside.
//                        Used in Singleton pattern (see 04_StaticMembers.cs).

// ● Object Initializer: A short way to set values while creating an object.
//      Student s = new Student { Name = "Aman", Age = 15 };


class Student
{
	public string Name;
	public int Age;
	public string School;

	// 1. Default constructor
	public Student()
	{
		Name = "Unknown";
		Age = 0;
		School = "Not assigned";
	}

	// 2. Parameterized constructor
	public Student(string name, int age)
	{
		Name = name;
		Age = age;
		School = "Not assigned";
	}

	// 3. Overloaded constructor (same name, different parameters)
	public Student(string name, int age, string school)
	{
		Name = name;
		Age = age;
		School = school;
	}

	public void Show()
	{
		Console.WriteLine(Name + ", " + Age + ", " + School);
	}
}

// this keyword: parameter names are the same as field names
class Point
{
	public int x;
	public int y;

	public Point(int x, int y)
	{
		this.x = x;   // this.x is the field, x is the parameter
		this.y = y;
	}
}

// Constructor chaining using : this(...)
class Employee
{
	public string Name;
	public int Salary;
	public string City;

	// Main constructor: the actual work is written only here
	public Employee(string name, int salary, string city)
	{
		Name = name;
		Salary = salary;
		City = city;
	}

	// Calls the main constructor with default city "Delhi"
	public Employee(string name, int salary) : this(name, salary, "Delhi") { }

	// Calls the above constructor with default salary 10000
	public Employee(string name) : this(name, 10000) { }

	public void Show()
	{
		Console.WriteLine(Name + ", " + Salary + ", " + City);
	}
}

class Program
{
	static void Main()
	{
		Student s1 = new Student();                      // calls default constructor
		Student s2 = new Student("Ravi", 14);            // calls constructor with 2 parameters
		Student s3 = new Student("Priya", 15, "DPS");    // calls constructor with 3 parameters

		s1.Show();   // o/p: "Unknown, 0, Not assigned"
		s2.Show();   // o/p: "Ravi, 14, Not assigned"
		s3.Show();   // o/p: "Priya, 15, DPS"

		// Object initializer: default constructor runs first, then Name and Age are set
		Student s4 = new Student { Name = "Aman", Age = 15 };
		s4.Show();   // o/p: "Aman, 15, Not assigned"

		Point p = new Point(3, 4);
		Console.WriteLine(p.x + ", " + p.y);   // o/p: "3, 4"

		Employee e1 = new Employee("Ravi");
		Employee e2 = new Employee("Priya", 50000, "Pune");
		e1.Show();   // o/p: "Ravi, 10000, Delhi"
		e2.Show();   // o/p: "Priya, 50000, Pune"
	}
}
