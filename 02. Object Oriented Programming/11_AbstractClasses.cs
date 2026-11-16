// ● Abstraction: Showing only what an object does, and hiding how it does it.
//                e.g., You press a button on a TV remote. You don't know how it works inside, and you don't need to.

// ● Problem: We make a parent class Shape with an Area() method. But what should Shape's Area() return?
//            A "shape" alone has no area. Only a real shape (Circle, Rectangle) has an area.
//   I.  So we return 0, which is meaningless.
//   II. Anyone can write 'new Shape()', which makes no sense. Which shape is it?
//   III. A child class may forget to write its own Area(), and nobody will tell us.
//
// ● Solution: Abstract class and abstract method.
//   1. Abstract class:  An incomplete class, made only to be a parent. You can't create its object.
//                       e.g., new Shape() -> Error
//   2. Abstract method: A method with no body (only its name). Child classes MUST override it.
//                       If a child class forgets, the compiler gives an error.

// ● Rules:
//   1. Use the 'abstract' keyword for both class and method.
//   2. An abstract class can have both abstract methods (no body) and normal methods (with body).
//   3. An abstract class can have fields, properties and constructors too.
//   4. An abstract method can be written only inside an abstract class.
//   5. The child class must override all abstract methods using 'override'.


abstract class Shape
{
	public string Color { get; set; }

	public Shape(string color)
	{
		Color = color;
	}

	// Abstract method: no body, each shape will write its own
	public abstract double Area();

	// Normal method: shared by all shapes
	public void ShowColor()
	{
		Console.WriteLine("Color: " + Color);
	}
}

class Circle : Shape
{
	public double Radius { get; set; }

	public Circle(string color, double radius) : base(color)
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
	public double Length { get; set; }
	public double Width { get; set; }

	public Rectangle(string color, double length, double width) : base(color)
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
		// Shape shape = new Shape("Blue");   // Error: can't create an object of an abstract class

		Shape circle = new Circle("Red", 2);
		circle.ShowColor();                 // o/p: "Color: Red"
		Console.WriteLine(circle.Area());   // o/p: 12.56

		Shape rectangle = new Rectangle("Green", 4, 5);
		rectangle.ShowColor();                 // o/p: "Color: Green"
		Console.WriteLine(rectangle.Area());   // o/p: 20

		// We only know "every shape has an Area()". How it is calculated is hidden inside each shape.
	}
}
