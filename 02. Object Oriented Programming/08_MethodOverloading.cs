// ● Polymorphism: Poly = many, morph = forms. One name, many forms.
//                 e.g., You are a son at home, a student at school, and a customer at a shop. Same person, many forms.
//
// ● 2 types of Polymorphism:
//   1. Compile-time Polymorphism: Method Overloading. The compiler decides which method to call, while compiling.
//   2. Runtime Polymorphism:      Method Overriding. Decided while the program is running (next chapter).

// ● Problem: We want to add 2 ints, 3 ints and 2 doubles. Without overloading, we need different names:
//            AddTwoInts(), AddThreeInts(), AddDoubles() ... too many names to remember.
//
// ● Method Overloading: Many methods with the SAME name but DIFFERENT parameters, in the same class.
//                       The compiler picks the right method by looking at the arguments you pass.
//   Parameters can be different in:
//   1. Number of parameters:  Add(int a, int b)        and  Add(int a, int b, int c)
//   2. Type of parameters:    Add(int a, int b)        and  Add(double a, double b)
//   3. Order of parameters:   Show(string s, int n)    and  Show(int n, string s)
//
//   Only a different return type is NOT enough:
//      int Add(int a, int b)  and  double Add(int a, int b)   -> Error

// ● You already used overloading: Console.WriteLine() works with int, string, bool... because it is overloaded.
//   Constructor overloading (03_Constructors.cs) is also method overloading.


class Calculator
{
	public int Add(int a, int b)
	{
		return a + b;
	}

	// 1. Different number of parameters
	public int Add(int a, int b, int c)
	{
		return a + b + c;
	}

	// 2. Different type of parameters
	public double Add(double a, double b)
	{
		return a + b;
	}

	// 3. Different order of parameters
	public void Show(string name, int age)
	{
		Console.WriteLine("Name: " + name + ", Age: " + age);
	}

	public void Show(int age, string name)
	{
		Console.WriteLine("Age: " + age + ", Name: " + name);
	}
}

class Program
{
	static void Main()
	{
		Calculator calc = new Calculator();

		Console.WriteLine(calc.Add(10, 20));       // o/p: 30    (calls Add(int, int))
		Console.WriteLine(calc.Add(10, 20, 30));   // o/p: 60    (calls Add(int, int, int))
		Console.WriteLine(calc.Add(2.5, 3.0));     // o/p: 5.5   (calls Add(double, double))

		calc.Show("Ravi", 20);   // o/p: "Name: Ravi, Age: 20"
		calc.Show(20, "Ravi");   // o/p: "Age: 20, Name: Ravi"
	}
}
