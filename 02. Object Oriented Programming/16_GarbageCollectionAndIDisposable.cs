// ● In C++, when you create an object using 'new', you must delete it yourself (using destructor / delete).
//   If you forget, that memory is never freed. This is called a memory leak.

// ● Garbage Collector (GC): In C#, you never delete objects yourself. The GC (part of the CLR) automatically
//                           removes objects from memory when no variable points to them anymore.
//      Student s = new Student();
//      s = null;   // The object has no one pointing to it now. GC will remove it sometime later.

// ● Destructor (Finalizer): A method that the GC calls just before removing an object.
//      class Student
//      {
//          ~Student() { Console.WriteLine("Destructor called"); }
//      }
//   But we don't know WHEN the GC will run. It may be after seconds or minutes. So destructors are rarely used in C#.

// ● Problem: GC only takes care of memory. But some objects also use outside things (resources), like
//            files, database connections and internet connections. These must be closed as soon as we are done,
//            not "sometime later". e.g., If a file is not closed, other programs can't use it.
//
// ● Solution: IDisposable interface. It has only one method: Dispose().
//             We write the cleanup code (closing the file, connection) inside Dispose().
//
// ● using: Calls Dispose() automatically at the end, even if an error happens in between.
//          So we never forget to clean up.
//   I.  using block:       using (FileHandler f = new FileHandler()) { ... }   -> Dispose() runs at the closing }
//   II. using declaration: using FileHandler f = new FileHandler();           -> Dispose() runs at the end of the method
//
// ● Built-in classes like StreamWriter (for files) and SqlConnection (for databases) implement IDisposable.
//   So always use them with 'using'.


class FileHandler : IDisposable
{
	private string fileName;

	public FileHandler(string fileName)
	{
		this.fileName = fileName;
		Console.WriteLine("Opened " + fileName);
	}

	public void Write(string text)
	{
		Console.WriteLine("Writing '" + text + "' in " + fileName);
	}

	public void Dispose()
	{
		Console.WriteLine("Closed " + fileName);   // cleanup code goes here
	}
}

class Program
{
	static void Main()
	{
		// 1. Calling Dispose() ourselves. If we forget it, or an error comes before it, the file stays open.
		FileHandler f1 = new FileHandler("a.txt");   // o/p: "Opened a.txt"
		f1.Write("Hello");                           // o/p: "Writing 'Hello' in a.txt"
		f1.Dispose();                                // o/p: "Closed a.txt"

		// 2. using block
		using (FileHandler f2 = new FileHandler("b.txt"))   // o/p: "Opened b.txt"
		{
			f2.Write("Hi");                                  // o/p: "Writing 'Hi' in b.txt"
		}                                                    // o/p: "Closed b.txt"   (Dispose() called automatically)

		// 3. using declaration
		using FileHandler f3 = new FileHandler("c.txt");    // o/p: "Opened c.txt"
		f3.Write("Bye");                                    // o/p: "Writing 'Bye' in c.txt"
		Console.WriteLine("End of Main");                   // o/p: "End of Main"
	}   // o/p: "Closed c.txt"   (Dispose() called automatically at the end of Main)
}
