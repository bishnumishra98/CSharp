// ● Abstraction: Showing what an object does, and hiding how it does it.
//                e.g., You press a TV remote button. You don't know how it works inside, and you don't need to.

// ● Problem: Parent class Shape has an Area() method. But what should it return?
//            A "shape" alone has no area. Only a real shape (Circle, Rectangle) has one.
//   I.   So we return 0, which is meaningless.
//   II.  Anyone can write 'new Shape()'. Which shape is it?
//   III. A child may forget to write its own Area(), and nobody warns us.
//
// ● Solution: the 'abstract' keyword.
//   1. Abstract class:  An incomplete class, made only to be a parent. You can't create its object. (new Shape() -> Error)
//   2. Abstract method: A method with no body. Every child MUST override it, or the compiler gives an error.
//                       It can be written only inside an abstract class.
//
// ● An abstract class can still have normal methods, fields and constructors. Only abstract methods have no body.

// ● abstract method vs virtual method:
//                abstract method       virtual method (09_MethodOverriding.cs)
//   Body         No                    Yes (a default version)
//   Override     Must                  Optional


abstract class Shape
{
	public abstract double Area();   // abstract: no body, each shape writes its own

	public void Show()               // normal method: shared by all shapes
	{
		Console.WriteLine("Area is " + Area());
	}
}

class Circle : Shape
{
	public double Radius;

	public Circle(double radius)
	{
		Radius = radius;
	}

	public override double Area()
	{
		return 3.14 * Radius * Radius;
	}
}

class Rectangle : Shape
{
	public double Length;
	public double Width;

	public Rectangle(double length, double width)
	{
		Length = length;
		Width = width;
	}

	public override double Area()
	{
		return Length * Width;
	}
}

class Program
{
	static void Main()
	{
		// Shape s = new Shape();   // Error: can't create an object of an abstract class

		Shape circle = new Circle(2);
		circle.Show();      // o/p: "Area is 12.56"

		Shape rectangle = new Rectangle(4, 5);
		rectangle.Show();   // o/p: "Area is 20"
	}
}
