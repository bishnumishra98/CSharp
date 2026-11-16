// ● Encapsulation: Keeping data and the methods that work on that data together in one class, and hiding the data
//                  from outside. Outside code can't touch the data directly. It must use the methods of the class.
//                  e.g., An ATM. You can't take cash directly from the machine. You must use the buttons (methods),
//                        and the ATM checks everything (PIN, balance) before giving cash.
//
// ● Problem: If a field is public, anyone can put any wrong value in it. e.g., account.Balance = -5000;
//   Solution: Make the field private, and give public methods to use it. These methods can check the value first.

// ● Access Modifiers: Keywords that decide who can use a class or its members.
//   1. public:    Can be used from anywhere.
//   2. private:   Can be used only inside the same class. (Default for class members)
//   3. protected: Can be used inside the same class and its child classes (see 07_Inheritance.cs).
//   4. internal:  Can be used anywhere inside the same project. (Default for classes)
//
//   Modifier      Same class    Child class    Other class (same project)    Other project
//   public        Yes           Yes            Yes                           Yes
//   private       Yes           No             No                            No
//   protected     Yes           Yes            No                            No (only child classes)
//   internal      Yes           Yes            Yes                           No


// Problem: public field, no protection
class BadBankAccount
{
	public int Balance;
}

// Solution: private field + public methods
class BankAccount
{
	private int balance;   // hidden from outside

	public void Deposit(int amount)
	{
		if (amount <= 0)
		{
			Console.WriteLine("Invalid amount");
			return;
		}
		balance += amount;
	}

	public void Withdraw(int amount)
	{
		if (amount > balance)
		{
			Console.WriteLine("Insufficient balance");
			return;
		}
		balance -= amount;
	}

	public int GetBalance()
	{
		return balance;
	}
}

class Program
{
	static void Main()
	{
		BadBankAccount bad = new BadBankAccount();
		bad.Balance = -5000;              // No one stops this wrong value
		Console.WriteLine(bad.Balance);   // o/p: -5000

		BankAccount account = new BankAccount();
		// account.balance = -5000;       // Error: 'balance' is private

		account.Deposit(1000);
		account.Deposit(-500);    // o/p: "Invalid amount"
		account.Withdraw(5000);   // o/p: "Insufficient balance"
		Console.WriteLine(account.GetBalance());   // o/p: 1000

		// Methods like GetBalance() are called getters, and methods that set a value are called setters.
		// In C#, we use Properties instead of getter/setter methods (next chapter).
	}
}
