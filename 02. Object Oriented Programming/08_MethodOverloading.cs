// ● Polymorphism: Poly = many, morph = forms. One name, many forms.
//                 e.g., You are a son at home, a student at school, a customer at a shop. Same person, many forms.
//
// ● 2 types of Polymorphism:
//   1. Compile-time: Method Overloading. The compiler decides which method to call. (this chapter)
//   2. Runtime:      Method Overriding. Decided while the program runs. (next chapter)

// ● Method Overloading: Same method name, different parameters, in the same class.
//                       The compiler looks at the arguments you pass and picks the matching method.
//   Problem it solves: Without it, we need AddTwoInts(), AddThreeInts(), AddDoubles()... too many names.
//
// ● Parameters must be different in:
//   1. Number:  Add(int a, int b)      and  Add(int a, int b, int c)
//   2. Type:    Add(int a, int b)      and  Add(double a, double b)
//   3. Order:   Show(string s, int n)  and  Show(int n, string s)
//   Only a different return type is NOT enough:  int Add(int a, int b)  and  double Add(int a, int b)  -> Error
//
// ● You already use it: Console.WriteLine() works with int, string, bool... because it is overloaded.


class Calculator
{
	public int Add(int a, int b)
	{
		return a + b;
	}

	public int Add(int a, int b, int c)      // different number
	{
		return a + b + c;
	}

	public double Add(double a, double b)    // different type
	{
		return a + b;
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
	}
}
