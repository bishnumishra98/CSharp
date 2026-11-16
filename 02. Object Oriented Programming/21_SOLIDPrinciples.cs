// ● SOLID: 5 rules for writing classes that are easy to understand, change and extend.
//   S - Single Responsibility Principle
//   O - Open/Closed Principle
//   L - Liskov Substitution Principle
//   I - Interface Segregation Principle
//   D - Dependency Inversion Principle
//   For each one: first we break the rule (Bad), then we fix it (Good).


// ----------------------------------------------------------------------------------------------------------------------------------
// 1. S - Single Responsibility Principle: A class should do only ONE job. It should have only one reason to change.
// ----------------------------------------------------------------------------------------------------------------------------------

// Bad: Invoice does 3 jobs: calculating, printing and saving.
//      If the print format changes, or the database changes, we have to change the Invoice class every time.
class BadInvoice
{
	public double Amount { get; set; }

	public double CalculateTotal() { return Amount * 1.18; }                  // job 1: calculate (with 18% tax)
	public void Print() { Console.WriteLine("Total: " + CalculateTotal()); }   // job 2: print
	public void SaveToDatabase() { Console.WriteLine("Saved to database"); }  // job 3: save
}

// Good: One class, one job
class Invoice
{
	public double Amount { get; set; }
	public double CalculateTotal() { return Amount * 1.18; }
}

class InvoicePrinter
{
	public void Print(Invoice invoice) { Console.WriteLine("Total: " + invoice.CalculateTotal()); }
}

class InvoiceRepository
{
	public void Save(Invoice invoice) { Console.WriteLine("Saved to database"); }
}


// ----------------------------------------------------------------------------------------------------------------------------------
// 2. O - Open/Closed Principle: A class should be open for extension, but closed for modification.
//                               Add new features by adding new code, not by changing old, working code.
// ----------------------------------------------------------------------------------------------------------------------------------

// Bad: To add a new customer type "Gold", we must open this class and add one more 'else if'.
//      Every change risks breaking the old working code.
class BadDiscountCalculator
{
	public double GetDiscount(string customerType, double amount)
	{
		if (customerType == "Regular")
			return amount * 0.1;
		else if (customerType == "Premium")
			return amount * 0.2;
		return 0;
	}
}

// Good: A new customer type = a new class. Old classes are never touched.
interface IDiscount
{
	double GetDiscount(double amount);
}

class RegularDiscount : IDiscount
{
	public double GetDiscount(double amount) { return amount * 0.1; }
}

class PremiumDiscount : IDiscount
{
	public double GetDiscount(double amount) { return amount * 0.2; }
}

class GoldDiscount : IDiscount   // new feature added, without changing any old class
{
	public double GetDiscount(double amount) { return amount * 0.3; }
}


// ----------------------------------------------------------------------------------------------------------------------------------
// 3. L - Liskov Substitution Principle: A child class should be usable in place of its parent class, without breaking
//                                       anything. A child should not remove or break what the parent promises.
// ----------------------------------------------------------------------------------------------------------------------------------

// Bad: Bird promises Fly(). But a Penguin is a Bird that can't fly, so it throws an error.
//      Any code that loops over birds and calls Fly() will crash when it meets a Penguin.
class BadBird
{
	public virtual void Fly() { Console.WriteLine("Flying"); }
}

class BadPenguin : BadBird
{
	public override void Fly() { throw new Exception("Penguin can't fly"); }
}

// Good: Only birds that can fly get Fly()
class Bird
{
	public void Eat() { Console.WriteLine("Bird is eating"); }
}

class FlyingBird : Bird
{
	public void Fly() { Console.WriteLine("Bird is flying"); }
}

class Sparrow : FlyingBird { }

class Penguin : Bird { }   // Penguin is a Bird, but not a FlyingBird. No one will ask it to fly.


// ----------------------------------------------------------------------------------------------------------------------------------
// 4. I - Interface Segregation Principle: Don't force a class to write methods it doesn't need.
//                                         Many small interfaces are better than one big interface.
// ----------------------------------------------------------------------------------------------------------------------------------

// Bad: One big interface. A Robot works, but doesn't eat. Still, it is forced to write Eat().
interface IBadWorker
{
	void Work();
	void Eat();
}

class BadRobot : IBadWorker
{
	public void Work() { Console.WriteLine("Robot is working"); }
	public void Eat() { throw new Exception("Robot doesn't eat"); }   // forced to write a useless method
}

// Good: Small interfaces. Each class picks only what it needs.
interface IWorkable
{
	void Work();
}

interface IEatable
{
	void Eat();
}

class Human : IWorkable, IEatable
{
	public void Work() { Console.WriteLine("Human is working"); }
	public void Eat() { Console.WriteLine("Human is eating"); }
}

class Robot : IWorkable
{
	public void Work() { Console.WriteLine("Robot is working"); }
}


// ----------------------------------------------------------------------------------------------------------------------------------
// 5. D - Dependency Inversion Principle: Big (high-level) classes should depend on interfaces, not on fixed classes.
//                                        (Dependency Injection in 20_DependencyInjection.cs is how we do this.)
// ----------------------------------------------------------------------------------------------------------------------------------

// Bad: Switch is stuck with LightBulb. It can't switch on a Fan without changing the Switch class.
class LightBulb
{
	public void TurnOn() { Console.WriteLine("Light bulb is on"); }
}

class BadSwitch
{
	private LightBulb bulb = new LightBulb();
	public void Press() { bulb.TurnOn(); }
}

// Good: Switch depends on an interface. It can switch on anything that is ISwitchable.
interface ISwitchable
{
	void TurnOn();
}

class Bulb : ISwitchable
{
	public void TurnOn() { Console.WriteLine("Bulb is on"); }
}

class Fan : ISwitchable
{
	public void TurnOn() { Console.WriteLine("Fan is on"); }
}

class Switch
{
	private readonly ISwitchable device;

	public Switch(ISwitchable device)
	{
		this.device = device;
	}

	public void Press() { device.TurnOn(); }
}


class Program
{
	static void Main()
	{
		// 1. S
		Invoice invoice = new Invoice { Amount = 100 };
		new InvoicePrinter().Print(invoice);     // o/p: "Total: 118"
		new InvoiceRepository().Save(invoice);   // o/p: "Saved to database"

		// 2. O
		IDiscount discount = new GoldDiscount();
		Console.WriteLine(discount.GetDiscount(1000));   // o/p: 300

		// 3. L
		// BadBird badBird = new BadPenguin();
		// badBird.Fly();                        // Crash: "Penguin can't fly"
		FlyingBird sparrow = new Sparrow();
		sparrow.Fly();                           // o/p: "Bird is flying"
		Bird penguin = new Penguin();
		penguin.Eat();                           // o/p: "Bird is eating"

		// 4. I
		IWorkable[] workers = { new Human(), new Robot() };
		foreach (IWorkable worker in workers)
			worker.Work();
		// o/p: "Human is working"
		//      "Robot is working"

		// 5. D
		new Switch(new Bulb()).Press();   // o/p: "Bulb is on"
		new Switch(new Fan()).Press();    // o/p: "Fan is on"
	}
}
