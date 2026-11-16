// ● Methods: Methods are blocks of code that perform a specific task and can be called from other parts of the program.
//            They help in organizing code, improving readability, and promoting code reuse.
//
// ● Types of Methods:
//   1. Takes nothing, returns nothing
//   2. Takes something, returns nothing
//   3. Takes nothing, returns something
//   4. Takes something, returns something
//
// ● Parameters can be passed to methods in the following ways:
//   1. By Value: The default way of passing parameters. A copy of the value is passed to the method. Changes made to the
//                parameter inside the method do not affect the original value.
//   2. By Reference: The ref keyword is used to pass an argument to a method by reference. It allows the method to modify the
//					  original value of the argument.
//                    In C#, you pass a parameter by reference using the 'ref', 'out', or 'in' keywords in both the method definition
//                    and the method call.

// 1. By Value: Example of a method that takes two integers as parameters and returns their sum

int Add(int a, int b)
{
	return a + b;
}

int result = Add(5, 10);
Console.WriteLine("The sum is: " + result);   // o/p: "The sum is: 15"


// 2. By Reference

//    I. Using 'ref' keyword (Read and Write): Use ref when you want the method to both read and modify an existing variable's value.
//                                             The variable must be initialized before you pass it. Use it when you want to allow the
//                                             method to modify the original value of the argument.
void Modify(ref int a, ref int b)
{
	a *= 2;
	b *= 2;
}

int num1 = 5, num2 = 10;
Console.WriteLine("Before Modify: num1 = " + num1 + ", num2 = " + num2);   // o/p: "Before Modify: num1 = 5, num2 = 10"
Modify(ref num1, ref num2);
Console.WriteLine("After Modify: num1 = " + num1 + ", num2 = " + num2);   // o/p: "After Modify: num1 = 10, num2 = 20"

//    II. Using 'out' keyword (Write Only): Use out when a method needs to initialize and return a value through the parameter. The
//                                          variable does not need to be initialized beforehand, and the method must assign a value
//                                          to it before returning. It is mostly used when a method needs to return multiple values.
void AssignValue(out int a, out int b)
{
	a = 100;
	b = 200;
}

int num3, num4;
AssignValue(out num3, out num4);
Console.WriteLine("After AssignValue: num3 = " + num3 + ", num4 = " + num4);   // o/p: "After AssignValue: num3 = 100, num4 = 200"

//    III. Using 'in' keyword (Read Only): Use in when a method needs to read the value of a parameter but not modify it.
//                                         The variable does not need to be initialized beforehand. It is mostly used when
//                                         passing large structs to avoid unnecessary copying. It also provides immutability
//                                         guarantees, ensuring that the method cannot modify the original value of the argument.
void DisplayValue(in int a, in int b)
{
	Console.WriteLine("Inside DisplayValue: a = " + a + ", b = " + b);
}

int num5 = 50, num6 = 75;
DisplayValue(in num5, in num6);   // o/p: "Inside DisplayValue: a = 50, b = 75"
