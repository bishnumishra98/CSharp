// ● Exception: An error that happens while the program is running. It stops (crashes) the program.
//   Common exceptions:
//   1. DivideByZeroException:      10 / 0
//   2. NullReferenceException:     Using an object which is null
//   3. IndexOutOfRangeException:   arr[10] when the array has only 3 items
//   4. FormatException:            int.Parse("abc")

// ● Problem: When an exception happens, the program crashes and stops. The lines after it never run.
// ● Solution: Exception Handling. We catch the error and handle it, so the program keeps running.
//   1. try:     Put the code that may cause an error inside try.
//   2. catch:   Runs only if an error happens in try. You can have many catch blocks for different errors.
//               Keep 'catch (Exception ex)' last, because it catches every type of error.
//   3. finally: Always runs, error or no error. Used for cleanup work.
//   4. throw:   Used to throw an error yourself, when something is wrong. e.g., withdrawing more than the balance.

// ● Exceptions are classes. All of them inherit from the Exception class. ex.Message tells what went wrong.

// ● Custom Exception: You can make your own exception by inheriting the Exception class.
//                     e.g., InsufficientBalanceException tells the exact problem better than a general error.


// Custom exception
class InsufficientBalanceException : Exception
{
	public InsufficientBalanceException(string message) : base(message) { }   // sends message to Exception class
}

class BankAccount
{
	public int Balance { get; private set; } = 1000;

	public void Withdraw(int amount)
	{
		if (amount > Balance)
			throw new InsufficientBalanceException("Balance is " + Balance + ", can't withdraw " + amount);

		Balance -= amount;
	}
}

class Program
{
	static void Main()
	{
		// Without try-catch, this line would crash the program:
		// int[] arr = { 1, 2, 3 };
		// Console.WriteLine(arr[10]);   // Crash: IndexOutOfRangeException

		// 1. try-catch-finally
		try
		{
			int a = 10, b = 0;
			Console.WriteLine(a / b);
			Console.WriteLine("This line will not run");
		}
		catch (DivideByZeroException ex)
		{
			Console.WriteLine("Error: " + ex.Message);   // o/p: "Error: Attempted to divide by zero."
		}
		finally
		{
			Console.WriteLine("Finally always runs");   // o/p: "Finally always runs"
		}

		// 2. Multiple catch blocks
		try
		{
			int number = int.Parse("abc");
		}
		catch (FormatException)
		{
			Console.WriteLine("Please enter a valid number");   // o/p: "Please enter a valid number"
		}
		catch (Exception ex)   // any other error. Always keep it last.
		{
			Console.WriteLine("Something went wrong: " + ex.Message);
		}

		// 3. throw + custom exception
		BankAccount account = new BankAccount();
		try
		{
			account.Withdraw(5000);
		}
		catch (InsufficientBalanceException ex)
		{
			Console.WriteLine(ex.Message);   // o/p: "Balance is 1000, can't withdraw 5000"
		}

		Console.WriteLine("Program is still running");   // o/p: "Program is still running"
	}
}
