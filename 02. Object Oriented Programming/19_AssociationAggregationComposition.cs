// ● In a program, classes work together. There are different types of relationships between them:
//
//   1. Inheritance: "is-a" relationship. e.g., Dog is an Animal. (See 07_Inheritance.cs)
//
//   2. Association: "uses-a" relationship. Two classes use each other, but both are independent. No one owns the other.
//                   e.g., A Doctor treats a Patient. Both exist on their own.
//
//   3. Aggregation: "has-a" relationship (weak). One class has another, but the inner object can live on its own.
//                   The inner object is created outside and passed in.
//                   e.g., A Department has Teachers. If the department is closed, the teachers still exist.
//
//   4. Composition: "part-of" relationship (strong). The inner object can't exist without the outer object.
//                   The inner object is created inside the outer class.
//                   e.g., A House has Rooms. If the house is destroyed, its rooms are destroyed too.

// ● Summary:
//
//   Relationship    Meaning     Inner object created     Can inner object live alone?   Example
//   Association     uses-a      outside                  Yes                            Doctor - Patient
//   Aggregation     has-a       outside, passed in       Yes                            Department - Teacher
//   Composition     part-of     inside the outer class   No                             House - Room


// 1. Association: Doctor uses Patient
class Patient
{
	public string Name { get; set; }

	public Patient(string name)
	{
		Name = name;
	}
}

class Doctor
{
	public string Name { get; set; }

	public Doctor(string name)
	{
		Name = name;
	}

	public void Treat(Patient patient)
	{
		Console.WriteLine(Name + " is treating " + patient.Name);
	}
}

// 2. Aggregation: Department has Teachers
class Teacher
{
	public string Name { get; set; }

	public Teacher(string name)
	{
		Name = name;
	}
}

class Department
{
	public string Name { get; set; }
	private List<Teacher> teachers = new List<Teacher>();

	public Department(string name)
	{
		Name = name;
	}

	public void AddTeacher(Teacher teacher)   // Teacher is created outside, and only added here
	{
		teachers.Add(teacher);
	}

	public void ShowTeachers()
	{
		foreach (Teacher teacher in teachers)
			Console.WriteLine(teacher.Name + " works in " + Name);
	}
}

// 3. Composition: House is made of Rooms
class Room
{
	public string Type { get; set; }

	public Room(string type)
	{
		Type = type;
	}
}

class House
{
	private List<Room> rooms = new List<Room>();

	public House()
	{
		// Rooms are created inside the House. They belong only to this house.
		rooms.Add(new Room("Bedroom"));
		rooms.Add(new Room("Kitchen"));
	}

	public void ShowRooms()
	{
		foreach (Room room in rooms)
			Console.WriteLine("House has a " + room.Type);
	}
}

class Program
{
	static void Main()
	{
		// 1. Association
		Doctor doctor = new Doctor("Dr. Mehta");
		Patient patient = new Patient("Ravi");
		doctor.Treat(patient);   // o/p: "Dr. Mehta is treating Ravi"

		// 2. Aggregation
		Teacher t1 = new Teacher("Mr. Sharma");   // teachers are created outside the department
		Teacher t2 = new Teacher("Ms. Gupta");

		Department science = new Department("Science");
		science.AddTeacher(t1);
		science.AddTeacher(t2);
		science.ShowTeachers();
		// o/p: "Mr. Sharma works in Science"
		//      "Ms. Gupta works in Science"
		// Even without the department, t1 and t2 still exist and can join another department.

		// 3. Composition
		House house = new House();   // rooms are created automatically inside the house
		house.ShowRooms();
		// o/p: "House has a Bedroom"
		//      "House has a Kitchen"
		// No one outside can reach these rooms. When the house is gone, its rooms are gone too.
	}
}
