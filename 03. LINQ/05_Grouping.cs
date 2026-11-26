// ● GroupBy(key): Puts items with the same key into one group. (Like GROUP BY in SQL)
//
//   employees.GroupBy(e => e.DepartmentId) gives:
//        Key = 1  ->  Bishnu, Amit, Sneha
//        Key = 2  ->  Rahul, Karan, Arjun
//        Key = 3  ->  Priya, Neha
//
//   - The result is a collection of groups. Each group is an IGrouping<TKey, TElement>:
//     I.  g.Key : the common value of the group (here DepartmentId).
//     II. g itself is a collection of the items of that group. So we can loop on it, or use Count(), Sum(), Max()... on it.
//   - Groups come in the order in which their key was first seen.
//   - The key can be anything: a property, a calculated value (salary band), or even null.

// ● Common patterns:
//   I.   Group + aggregate (count / total / average per group):
//        employees.GroupBy(e => e.DepartmentId).Select(g => $"{g.Key}: {g.Count()}")
//   II.  Filter groups (like HAVING in SQL): add Where AFTER GroupBy.
//        employees.GroupBy(e => e.DepartmentId).Where(g => g.Count() >= 3)
//   III. Choose what goes inside each group: GroupBy(key, elementSelector)
//        employees.GroupBy(e => e.DepartmentId, e => e.EmployeeName)    // groups of names, not Employees
//   IV.  Sort groups: add OrderBy AFTER GroupBy (e.g., biggest department first).
//        employees.GroupBy(e => e.DepartmentId).OrderByDescending(g => g.Count())

using System;
using System.Collections.Generic;
using System.Linq;

class Employee
{
	public int EmployeeId { get; set; }
	public string EmployeeName { get; set; } = "";
	public decimal Salary { get; set; }
	public int DepartmentId { get; set; }
	public int? ManagerId { get; set; }
}

class Department
{
	public int DepartmentId { get; set; }
	public string DepartmentName { get; set; } = "";
}

class Program
{
	static void Main()
	{
		List<Employee> employees = new List<Employee>
		{
			new Employee { EmployeeId = 1, EmployeeName = "Bishnu", Salary = 1200000, DepartmentId = 1, ManagerId = 5 },
			new Employee { EmployeeId = 2, EmployeeName = "Rahul", Salary = 800000, DepartmentId = 2, ManagerId = 5 },
			new Employee { EmployeeId = 3, EmployeeName = "Amit", Salary = 1500000, DepartmentId = 1, ManagerId = 6 },
			new Employee { EmployeeId = 4, EmployeeName = "Priya", Salary = 600000, DepartmentId = 3, ManagerId = 6 },
			new Employee { EmployeeId = 5, EmployeeName = "Karan", Salary = 1800000, DepartmentId = 2, ManagerId = null },
			new Employee { EmployeeId = 6, EmployeeName = "Sneha", Salary = 1400000, DepartmentId = 1, ManagerId = null },
			new Employee { EmployeeId = 7, EmployeeName = "Neha", Salary = 900000, DepartmentId = 3, ManagerId = 5 },
			new Employee { EmployeeId = 8, EmployeeName = "Arjun", Salary = 1200000, DepartmentId = 2, ManagerId = 6 }
		};

		List<Department> departments = new List<Department>
		{
			new Department { DepartmentId = 1, DepartmentName = "IT" },
			new Department { DepartmentId = 2, DepartmentName = "HR" },
			new Department { DepartmentId = 3, DepartmentName = "Finance" },
			new Department { DepartmentId = 4, DepartmentName = "Marketing" }
		};

		// ---------------- Basic GroupBy ----------------

		var byDept = employees.GroupBy(e => e.DepartmentId);

		foreach (var g in byDept)
		{
			Console.WriteLine($"{g.Key}: {string.Join(", ", g.Select(e => e.EmployeeName))}");
		}
		// o/p:
		// 1: Bishnu, Amit, Sneha
		// 2: Rahul, Karan, Arjun
		// 3: Priya, Neha

		// ---------------- Group + aggregate (by department) ----------------

		// Helper: department name from its Id
		Func<int, string> deptName = id => departments.First(d => d.DepartmentId == id).DepartmentName;

		foreach (var g in byDept)
		{
			Console.WriteLine($"{deptName(g.Key)} -> Count: {g.Count()}, Total: {g.Sum(e => e.Salary)}, " +
							  $"Average: {g.Average(e => e.Salary):F2}, Highest paid: {g.MaxBy(e => e.Salary)?.EmployeeName}");
		}
		// o/p:
		// IT -> Count: 3, Total: 4100000, Average: 1366666.67, Highest paid: Amit
		// HR -> Count: 3, Total: 3800000, Average: 1266666.67, Highest paid: Karan
		// Finance -> Count: 2, Total: 1500000, Average: 750000.00, Highest paid: Neha

		// ---------------- Filter groups (HAVING) ----------------

		var bigDepts = employees.GroupBy(e => e.DepartmentId)
								.Where(g => g.Count() >= 3)
								.Select(g => deptName(g.Key));
		Console.WriteLine(string.Join(", ", bigDepts));   // o/p: "IT, HR"

		// ---------------- Group by a calculated key (category / salary band) ----------------

		var bands = employees.GroupBy(e => e.Salary >= 1500000 ? "High" : e.Salary >= 1000000 ? "Medium" : "Low");

		foreach (var g in bands)
		{
			Console.WriteLine($"{g.Key}: {string.Join(", ", g.Select(e => e.EmployeeName))}");
		}
		// o/p:   (order = first time each key is seen: Bishnu is Medium, Rahul is Low, Amit is High)
		// Medium: Bishnu, Sneha, Arjun
		// Low: Rahul, Priya, Neha
		// High: Amit, Karan

		// ---------------- Group by salary (same salary) ----------------

		var sameSalary = employees.GroupBy(e => e.Salary).Where(g => g.Count() > 1);

		foreach (var g in sameSalary)
		{
			Console.WriteLine($"{g.Key}: {string.Join(", ", g.Select(e => e.EmployeeName))}");   // o/p: "1200000: Bishnu, Arjun"
		}

		// ---------------- Group by a nullable key (manager) ----------------

		var byManager = employees.GroupBy(e => e.ManagerId, e => e.EmployeeName);   // groups contain names only

		foreach (var g in byManager)
		{
			Console.WriteLine($"{g.Key?.ToString() ?? "No manager"}: {string.Join(", ", g)}");
		}
		// o/p:
		// 5: Bishnu, Rahul, Neha
		// 6: Amit, Priya, Arjun
		// No manager: Karan, Sneha

		// ---------------- Sort groups (biggest department first, then by Id) ----------------

		var countPerDept = employees.GroupBy(e => e.DepartmentId)
									.OrderByDescending(g => g.Count())
									.ThenBy(g => g.Key)
									.Select(g => $"{deptName(g.Key)}: {g.Count()}");
		Console.WriteLine(string.Join(", ", countPerDept));   // o/p: "IT: 3, HR: 3, Finance: 2"

		// ---------------- Grouping simple values ----------------

		string[] words = { "apple", "bat", "cat", "ant", "ball", "axe" };

		var byFirstLetter = words.GroupBy(w => w[0]);

		foreach (var g in byFirstLetter)
		{
			Console.WriteLine($"{g.Key}: {string.Join(", ", g)}");
		}
		// o/p:
		// a: apple, ant, axe
		// b: bat, ball
		// c: cat
	}
}
