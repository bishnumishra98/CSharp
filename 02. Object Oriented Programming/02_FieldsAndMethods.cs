// ● Members: Everything written inside a class is called a member of that class. e.g., fields, methods, constructors.

// ● Field: A variable declared directly inside a class (not inside any method). It stores the data of an object.
//          Every object gets its own copy of the fields.
//
// ● Local Variable: A variable declared inside a method. It can be used only inside that method.
//
// ● Field vs Local Variable:
//   1. Default value: Fields get a default value automatically (int -> 0, bool -> false).
//                     Local variables don't. You must give them a value before using them.
//   2. Lifetime:      Fields live as long as the object lives. Local variables die when the method ends.

// ● Method: A function written inside a class. It tells what an object can do. Methods can use the fields of the class.

// ● const and readonly: Both create fields whose value can NOT be changed later.
//   1. const:    Value is given at declaration only. It is the same for all objects, forever.
//                e.g., Interest rate for all accounts.
//   2. readonly: Value is given at declaration OR inside a constructor. So each object can have a different value,
//                but once set, it can't be changed.
//                e.g., Account number: different for each account, but never changes.


class BankAccount
{
	// Fields
	public string Owner = "";
	public int Balance;                        // default value is 0

	public const double InterestRate = 3.5;    // const: same for all accounts, fixed forever
	public readonly int AccountNumber;         // readonly: set once per object, then fixed

	// Constructor (explained in the next chapter): runs when the object is created
	public BankAccount(int accountNumber)
	{
		AccountNumber = accountNumber;   // readonly field can be set here
	}

	// Method: takes something, returns nothing
	public void Deposit(int amount)
	{
		int oldBalance = Balance;   // local variable: lives only inside this method
		Balance = Balance + amount;
		Console.WriteLine("Balance changed from " + oldBalance + " to " + Balance);
	}

	// Method: takes nothing, returns something
	public int GetBalance()
	{
		return Balance;
	}
}

class Program
{
	static void Main()
	{
		BankAccount account = new BankAccount(101);
		account.Owner = "Ravi";

		Console.WriteLine(account.Balance);        // o/p: 0   (default value of int field)

		account.Deposit(500);                      // o/p: "Balance changed from 0 to 500"
		Console.WriteLine(account.GetBalance());   // o/p: 500

		// const belongs to the class (same for everyone), so it is used with the class name
		Console.WriteLine(BankAccount.InterestRate);   // o/p: 3.5
		Console.WriteLine(account.AccountNumber);      // o/p: 101

		// account.AccountNumber = 102;      // Error: readonly field can't be changed after the object is created
		// BankAccount.InterestRate = 4.0;   // Error: const can never be changed
		// Console.WriteLine(oldBalance);    // Error: oldBalance is a local variable of Deposit()
	}
}
