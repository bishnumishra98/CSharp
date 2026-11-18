// ● partial class: One class split into 2 or more parts (usually in different files).
//                  While compiling, C# joins all the parts into ONE single class.
//                  Just write the 'partial' keyword before 'class' in every part.
//
// ● Problem: 1. Some classes become very big (1000s of lines). Hard to read, and when 2 people edit
//               the same file at the same time, their changes clash.
//            2. Some code is written by tools, not by us. e.g., In WinForms, the designer tool writes the code
//               for buttons and textboxes. If we write our own code in that same file, the tool may erase it
//               when it writes the file again.
//   Solution: partial class. Keep the tool's code in one file and our code in another file.
//      Form1.Designer.cs   ->   partial class Form1 { ... }   (written by the tool)
//      Form1.cs            ->   partial class Form1 { ... }   (written by us)
//   Both are the same class Form1. The tool can rewrite its file without touching ours.

// ● Rules:
//   1. Every part must have the 'partial' keyword. If one part misses it -> Error.
//   2. All parts must have the same name, and be in the same namespace and the same project.
//   3. All parts must have the same access modifier (e.g., all public).
//   4. If any one part is abstract or sealed, or inherits a base class, the whole class becomes that.
//   5. Members of one part can freely use members of the other parts (it's all one class).
//   6. partial also works with struct, interface and record.

// ● partial method: A method declared in one part (without body), and its body written in another part.
//                   If no part writes the body, C# simply removes the method and its calls. No error.
//                   (This simple kind returns void and has no access modifier.)
//   Used by tools: the tool's part says "I'll call OnCreated() here", and we may or may not fill it in.


// ---------- Part 1 (imagine this is in file Student.cs) ----------
partial class Student
{
	public string Name;
	public int Marks;

	public Student(string name, int marks)
	{
		Name = name;
		Marks = marks;
		OnCreated();   // calling the partial method
	}

	partial void OnCreated();   // partial method: only declared here, no body
}

// ---------- Part 2 (imagine this is in file Student.Report.cs) ----------
partial class Student
{
	public void ShowReport()
	{
		Console.WriteLine(Name + " got " + Marks + " marks");   // using fields from Part 1
	}

	public bool IsPass()
	{
		return Marks >= 33;
	}

	// body of the partial method
	partial void OnCreated()
	{
		Console.WriteLine("Student created: " + Name);
	}
}

// class Student { }   // Error: missing 'partial' keyword (another part of Student already exists)


class Program
{
	static void Main()
	{
		Student s = new Student("Ravi", 80);   // o/p: "Student created: Ravi"
		s.ShowReport();                        // o/p: "Ravi got 80 marks"     (method from Part 2)
		Console.WriteLine(s.IsPass());         // o/p: True
		Console.WriteLine(s.Name);             // o/p: "Ravi"                  (field from Part 1)

		// One object, using members from both parts, because both parts are the same class.
	}
}
