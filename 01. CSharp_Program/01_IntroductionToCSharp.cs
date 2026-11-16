// ● C# (C-Sharp) is a programming language made by Microsoft. It is high-level, object-oriented and case-sensitive language.
//   C# is used to build web apps, desktop apps, mobile apps, games (Unity) and much more.

// ● .NET is the platform on which C# programs run. Think of it like this:
//     C#   = the language you write in
//     .NET = the engine + ready-made tools that run your code
//   Note: The old .NET Framework works only on Windows. The modern .NET (formerly .NET Core) works on Windows,
//         Linux and macOS. Today, "new projects" use modern .NET.

// ● .NET has two main parts:
//   1. CLR (Common Language Runtime): The engine that runs your program.
//   2. Class Library: A huge collection of ready-made code (classes) that you can use directly, e.g.
//      Console (printing), String (text), Math (calculations), DateTime (dates), File (reading/writing files), etc.
//      So you don't have to write everything from scratch.

// ● How a C# program runs (step by step):
//
//      C# code (.cs)    F# code (.fs)    VB code (.vb)
//           |                |                |
//      C# compiler      F# compiler      VB compiler
//           |                |                |
//           +----------------+----------------+
//                            |
//               CIL / IL (Intermediate Language)   <- stored in an assembly (.exe or .dll)
//                            |
//               CLR (JIT compiler)                 <- when the program runs
//                            |
//               Machine code (0101...)             <- what the CPU actually understands
//
//   Step 1 (Build time): The C# compiler converts your code into IL (Intermediate Language). IL is not machine code yet,
//                        it is a common "middle" language. Every .NET language (C#, F#, VB) gets converted to the same IL.
//   Step 2 (Build time): IL is saved in a file called an assembly. An assembly is either:
//                        .exe -> an application you can run
//                        .dll -> a library (reusable code that other programs can use)
//   Step 3 (Run time):   When you run the program, the CLR's JIT (Just-In-Time) compiler converts the IL into machine
//                        code for your computer's CPU, and then the program runs.
//
//   Why the extra IL step? Because the same .exe/.dll can run on any computer that has .NET (Windows, Linux, Mac, etc).
//   The CLR on each computer converts IL into the right machine code for that computer.

// ● What the CLR does for you:
//   1. JIT Compilation: Converts IL into machine code while the program runs.
//   2. Garbage Collection (GC): Automatically deletes objects from memory that are no longer used.
//                               (In C/C++, you have to free memory yourself. In C#, the GC does it for you.)
//   3. Type Safety: Makes sure you don't use data in the wrong way (e.g., treating a string as a number).
//   4. Exception Handling: Handles runtime errors in a common way (try/catch).
//   5. Thread Management: Lets your program do multiple tasks at the same time.
//   Code that runs under the CLR is called "managed code" (because the CLR manages it). The CLR is similar to
//   the JVM in Java.

// ● Project (.csproj): A project is a set of code files (.cs) that builds into ONE assembly (.exe or .dll).
//   The .csproj file defines the project. It stores the project's settings, like which .NET version to use,
//   whether to build an .exe or a .dll, and which libraries (NuGet packages) it needs.
//   By default, all .cs files in the project's folder (and its subfolders) are included in the project.

// ● Solution (.sln or the newer .slnx): A solution is a group of related projects, so you can open, build and
//   manage them together. e.g., a solution can have one project for the website and another for the database code.
//
//      Solution (.sln)
//        ├── Project 1 (.csproj)  ->  MyApp.exe
//        └── Project 2 (.csproj)  ->  MyLibrary.dll

// -------------------------------------------------------------------------------------------------------------------------

// ● using directive is used to import a namespace. System is the root namespace of .NET. System is a built-in namespace
//   provided by .NET which contains fundamental classes like Console, String, Math, DateTime, Exception, Random, etc.

// ● A namespace is a logical container for classes used to group related classes together and avoid name conflicts.

// ● class is a container for data and methods, which brings functionality to your program.
//   Every line of code that runs in C# must be inside a class. In our example, we named the class Program.

using System;

namespace HelloWorld
{
	// ● There can be multiple classes inside a namespace. For instance, we created Program class.
	class Program
	{
		// ● Main() is the entry point of a C# program. When you run a C# code, CLR looks for the Main method to start execution.
		//   Main() can be written inside any one of the classes in a project, but there should be only one Main() per project.
		//   If there are more, specify the entry point using <StartupObject> in the .csproj file.

		// ● Why static ? Because static members belong to class, they do not need any object to be invoked. When we run a
		//   C# program, there are no ojects created yet, and the OS starts the CLR, and CLR searches for Main() method to
		//   begin program execution. So, Main() method must be static so that CLR can invoke it without creating any object.

		// ● Why void ? Because Main method does not return any value to the runtime.
		//   However, Main can also return an int, which is passed to the operating system as the program's exit code
		//   (by convention, 0 means success and a non-zero value means an error). For async code, Main can return
		//   Task or Task<int>.

		// ● Why string[] args ? It is is used to receive command-line arguments passed during execution. args is an array
		//   of strings representing the arguments passed when executing the program from the command line.

		// ● The Main method can also be defined without any parameters, like this: static void Main() { ... }
		//   However, using string[] args allows you to handle command-line arguments if needed.
		static void Main(string[] args)
		{
			Console.WriteLine("Namaste Duniya1");   // prints output to the console and moves the cursor to the next line
			Console.Write("Namaste Duniya2");   // prints output to the console without moving the cursor to the next line
			Console.Write("Namaste Duniya3");
			Console.Write("\nNamaste Duniya4\nNamaste Duniya5");   // /n is used to print a new line in the console
		}
	}
}
