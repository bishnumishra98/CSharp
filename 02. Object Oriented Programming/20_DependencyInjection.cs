// ● Dependency: If class A needs class B to do its work, then B is a dependency of A.
//               e.g., A Car needs an Engine to drive. So Engine is a dependency of Car.

// ● Problem (Tight Coupling): Car creates the Engine by itself using 'new' inside it.
//      class Car
//      {
//          Engine engine = new Engine();
//      }
//   Car depends directly on Engine. If Engine changes, Car also gets affected. This is called tight coupling.
//   I.   Car is stuck with PetrolEngine forever. It can't use a DieselEngine or an ElectricEngine,
//        without changing the Car class code.
//   II.  Hard to test: We can't give Car a fake (test) engine while testing.
//   III. It breaks the Dependency Inversion Principle (D in SOLID, see 21_SOLIDPrinciples.cs):
//        "Classes should depend on interfaces, not on fixed classes."

// ● Dependency Injection (DI): An object gets the things it needs (dependencies) from outside,
//                              instead of creating them by itself.
//   In simple words: Car does NOT make its Engine. The Engine is given to the Car.

// ● Steps of DI:
//   1. Create an interface:        IEngine
//   2. Create its implementations: PetrolEngine, DieselEngine, ElectricEngine
//   3. Inject the dependency:      Car takes IEngine in its constructor (Constructor Injection)
//
// ● Benefits:
//   1. Loose coupling:   Car works with ANY engine. It doesn't care which one.
//   2. Easy replacement: To change the engine, just pass a different one. No change in the Car class.
//   3. Easy testing:     We can pass a fake engine while testing.

// ● Constructor Injection (shown here) is the most common way of DI.
// ● In ASP.NET Core, .NET creates these objects and injects them into constructors automatically.


// Problem: Tight Coupling
class Engine
{
	public void Start()
	{
		Console.WriteLine("Petrol engine started");
	}
}

class BadCar
{
	private Engine engine = new Engine();   // Car creates its own Engine

	public void Drive()
	{
		engine.Start();
	}
}

// Solution: Dependency Injection

// Step 1: Create interface
interface IEngine
{
	void Start();
}

// Step 2: Create implementations
class PetrolEngine : IEngine
{
	public void Start()
	{
		Console.WriteLine("Petrol engine started");
	}
}

class DieselEngine : IEngine
{
	public void Start()
	{
		Console.WriteLine("Diesel engine started");
	}
}

class ElectricEngine : IEngine
{
	public void Start()
	{
		Console.WriteLine("Electric engine started");
	}
}

// Step 3: Inject dependency (Constructor Injection)
class Car
{
	private readonly IEngine _engine;   // _ (underscore) is a common naming style for private fields in C#

	public Car(IEngine engine)          // Engine comes from outside
	{
		_engine = engine;
	}

	public void Drive()
	{
		_engine.Start();
	}
}

class Program
{
	static void Main()
	{
		// Problem: can only use a petrol engine
		BadCar badCar = new BadCar();
		badCar.Drive();   // o/p: "Petrol engine started"

		// Solution: we decide from outside which engine the car gets
		Car petrolCar = new Car(new PetrolEngine());
		Car dieselCar = new Car(new DieselEngine());
		Car electricCar = new Car(new ElectricEngine());

		petrolCar.Drive();     // o/p: "Petrol engine started"
		dieselCar.Drive();     // o/p: "Diesel engine started"
		electricCar.Drive();   // o/p: "Electric engine started"
	}
}
