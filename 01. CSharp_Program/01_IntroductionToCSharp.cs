// ● C# (C-Sharp) is a high-level, object-oriented, case-sensitive programming language developed by Microsoft
//   that runs on the .NET Framework.

// ● C# is used to develop web apps, desktop apps, mobile apps, games and much more.

// ● using directive is used to import a namespace. System is the root namespace of .NET. System is a built-in namespace
//   provided by .NET which contains fundamental classes like Console, String, Math, DateTime, Exception, Random, etc.

// ● A namespace is a logical container for classes used to group related classes together and avoid name conflicts.

// ● class is a container for data and methods, which brings functionality to your program.
//   Every line of code that runs in C# must be inside a class. In our example, we named the class Program.

using System;

namespace HelloWorld
{
	class Program
	{
		// ● Entry point of the program: When you run a C# code, CLR looks for the Main method to start execution.

		// ● Why static ? Because static members belong to class, they do not need any object to be invoked. When we run a
		//   C# program, there are no ojects created yet, and the OS starts the CLR, and CLR searches for Main() method to
		//   begin program execution. So, Main() method must be static so that CLR can invoke it without creating any object.

		// ● Why void ? Because Main method does not return any value to the runtime.
		//   However, you can also define Main method to return an integer (int) to indicate the exit status of the program.

		// ● Why string[] args ? It is is used to receive command-line arguments passed during execution. args is an array
		//   of strings representing the arguments passed when executing the program from the command line.

		// ● The Main method can also be defined without any parameters, like this: static void Main() { ... }
		//   However, using string[] args allows you to handle command-line arguments if needed.

		// ● If you need to make other classes, you can define them under the same namespace, but outside the Program class.
		//   Program class should be the only class that contains the Main method, as it serves as the entry point of the program.

		static void Main(string[] args)
		{
			Console.WriteLine("Namaste Duniya1");   // prints output to the console and moves the cursor to the next line
			Console.Write("Namaste Duniya2");   // prints output to the console without moving the cursor to the next line
			Console.Write("Namaste Duniya3");
			Console.Write("\nNamaste Duniya4\nNamaste Duniya5");   // /n is used to print a new line in the console
		}
	}
}
