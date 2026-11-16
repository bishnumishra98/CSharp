// ● There are 3 ways to "copy" an object:
//
//   1. Reference Copy: Student s2 = s1;
//                      No new object is created. Both s1 and s2 point to the same object.
//
//   2. Shallow Copy: A new object is created, and all fields are copied as they are.
//                    I.  Value type fields (int, double): the value is copied. So they are separate.
//                    II. Reference type fields (another object, array, list): only the reference is copied.
//                        So the inner object is SHARED by both copies. Changing it in one changes it in the other.
//                    In C#, MemberwiseClone() (a method from the object class) makes a shallow copy.
//
//   3. Deep Copy: A new object is created, AND new copies of all inner objects are also created.
//                 Nothing is shared. Both copies are fully separate. C# has no built-in deep copy, we write it ourselves.
//
//      Shallow Copy:                                Deep Copy:
//      original -> [Name, Age, Address] --+         original -> [Name, Age, Address] --> [City: Delhi]
//                                         +--> [City: Delhi]
//      copy     -> [Name, Age, Address] --+         copy     -> [Name, Age, Address] --> [City: Delhi]
//
// ● Note: string is a reference type, but it can't be changed (immutable). So sharing a string is safe.


class Address
{
	public string City { get; set; } = "";
}

class Student
{
	public string Name { get; set; } = "";
	public int Age { get; set; }
	public Address Address { get; set; } = new Address();

	public Student ShallowCopy()
	{
		return (Student)MemberwiseClone();   // copies all fields as they are
	}

	public Student DeepCopy()
	{
		Student copy = (Student)MemberwiseClone();             // first, copy all fields
		copy.Address = new Address { City = Address.City };    // then, make a NEW Address object
		return copy;
	}
}

class Program
{
	static void Main()
	{
		Student original = new Student { Name = "Ravi", Age = 20, Address = new Address { City = "Delhi" } };

		// 1. Reference Copy
		Student refCopy = original;
		refCopy.Age = 25;
		Console.WriteLine(original.Age);   // o/p: 25   (same object)

		// 2. Shallow Copy
		Student shallow = original.ShallowCopy();
		shallow.Age = 30;                  // Age is a value type: separate copy
		shallow.Address.City = "Mumbai";   // Address is a reference type: shared object

		Console.WriteLine(original.Age);            // o/p: 25         (not changed)
		Console.WriteLine(original.Address.City);   // o/p: "Mumbai"   (changed, because Address is shared)

		// 3. Deep Copy
		Student deep = original.DeepCopy();
		deep.Address.City = "Pune";

		Console.WriteLine(original.Address.City);   // o/p: "Mumbai"   (not changed, deep has its own Address)
		Console.WriteLine(deep.Address.City);       // o/p: "Pune"
	}
}
