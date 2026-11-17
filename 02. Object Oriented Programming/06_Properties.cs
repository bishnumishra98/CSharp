// ● Problem: For every private field, we write 2 methods: GetAge() and SetAge(). This is a lot of code, and calling
//            p.SetAge(20) is not as easy as p.Age = 20.
//
// ● Property: Looks like a field from outside, but works like methods inside.
//             It has 2 parts:
//             I.  get: runs when you read the value.   e.g., Console.WriteLine(p.Age);
//             II. set: runs when you assign a value.  e.g., p.Age = 20;
//                      'value' keyword holds the value being assigned (20 here).
//
//             In simple terms, Properties are class members that provide a flexible mechanism to read, write, or compute
//             the values of private fields.

// ● Types of properties:
//   1. Full Property: A private field + get/set. Use it when you want to check the value before saving it.
//   2. Auto Property: { get; set; } C# creates the hidden private field for you. Use it when no checking is needed.
//   3. Read-only from outside: { get; private set; } Anyone can read, but only the class itself can change it.
//   4. init: { get; init; } Can be set only while creating the object, and can never be changed after that.
//   5. Computed Property: Has no field. It calculates its value every time. e.g., IsAdult is calculated from age.

// ● Naming: private fields start with small letter (age), properties start with capital letter (Age).
//   From now on, we will use properties instead of public fields.


// Old way: getter and setter methods (like in Java/C++)
class OldPerson
{
	private int age;

	public int GetAge()
	{
		return age;
	}

	public void SetAge(int value)
	{
		if (value > 0)
			age = value;
	}
}

// C# way: properties
class Person
{
	// 1. Full property
	private int age;
	public int Age
	{
		get { return age; }
		set
		{
			if (value > 0)
				age = value;
			else
				Console.WriteLine("Age must be positive");
		}
	}

	// 2. Auto property
	public string Name { get; set; } = "";

	// 3. Read-only from outside
	public int Id { get; private set; }

	// 4. init property
	public string AadharNumber { get; init; } = "";

	// 5. Computed property
	public bool IsAdult
	{
		get { return age >= 18; }
	}

	public Person(int id)
	{
		Id = id;
	}
}

class Program
{
	static void Main()
	{
		OldPerson old = new OldPerson();
		old.SetAge(20);
		Console.WriteLine(old.GetAge());   // o/p: 20

		Person p = new Person(1) { Name = "Ravi", AadharNumber = "1234-5678" };

		p.Age = 20;                      // set runs, value = 20
		Console.WriteLine(p.Age);        // get runs. o/p: 20
		p.Age = -5;                      // o/p: "Age must be positive"

		Console.WriteLine(p.Name);       // o/p: "Ravi"
		Console.WriteLine(p.Id);         // o/p: 1
		Console.WriteLine(p.IsAdult);    // o/p: True

		// p.Id = 5;                     // Error: set is private
		// p.AadharNumber = "9999";      // Error: init property can be set only while creating the object
	}
}
