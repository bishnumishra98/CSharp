// ● Common LINQ interview problems. Each problem has: the question, the idea, and the code.
//
//   1.  Second highest salary (and Nth highest)
//   2.  Duplicate elements
//   3.  Frequency of elements
//   4.  Employees by department
//   5.  Top N salaries (overall and per department)
//   6.  Flatten nested collections
//   7.  Missing elements
//   8.  Unique elements (appear only once) and first non-repeating character
//   9.  Employees earning more than their manager
//   10. Employees earning more than their department's average
//   11. Department with the highest total salary
//   12. Departments with no employees
//   13. Number of employees under each manager
//   14. Anagram and palindrome check
//   15. Longest word and reverse each word

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

		// ---------------- 1. Second highest salary ----------------
		// Idea: take unique salaries -> sort high to low -> skip 1 -> take the first.
		// Why Distinct? If 2 people share the top salary, without Distinct we would get the top salary again.

		decimal secondHighest = employees.Select(e => e.Salary).Distinct().OrderByDescending(s => s).Skip(1).First();
		Console.WriteLine(secondHighest);   // o/p: 1500000

		// Who earns it?
		var whoSecond = employees.Where(e => e.Salary == secondHighest).Select(e => e.EmployeeName);
		Console.WriteLine(string.Join(", ", whoSecond));   // o/p: "Amit"

		// Nth highest: Skip(n - 1). FirstOrDefault gives 0 (instead of an exception) if N is too big.
		int n = 4;
		decimal nthHighest = employees.Select(e => e.Salary).Distinct().OrderByDescending(s => s).Skip(n - 1).FirstOrDefault();
		Console.WriteLine(nthHighest);   // o/p: 1200000

		// Another way, without sorting: the biggest salary that is less than the max
		decimal max = employees.Max(e => e.Salary);
		Console.WriteLine(employees.Where(e => e.Salary < max).Max(e => e.Salary));   // o/p: 1500000

		// ---------------- 2. Duplicate elements ----------------
		// Idea: group same values together, keep groups with more than 1 item.

		int[] nums = { 1, 2, 3, 2, 4, 5, 3, 6, 3 };

		var duplicates = nums.GroupBy(x => x).Where(g => g.Count() > 1).Select(g => g.Key);
		Console.WriteLine(string.Join(", ", duplicates));   // o/p: "2, 3"

		// Employees having the same salary as someone else
		var sameSalary = employees.GroupBy(e => e.Salary)
								  .Where(g => g.Count() > 1)
								  .SelectMany(g => g)
								  .Select(e => e.EmployeeName);
		Console.WriteLine(string.Join(", ", sameSalary));   // o/p: "Bishnu, Arjun"

		// ---------------- 3. Frequency of elements ----------------
		// Idea: GroupBy + Count. Store in a Dictionary for quick lookup.

		Dictionary<int, int> freq = nums.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
		Console.WriteLine(string.Join(", ", freq.Select(kv => $"{kv.Key}:{kv.Value}")));   // o/p: "1:1, 2:2, 3:3, 4:1, 5:1, 6:1"

		// Character frequency in a string (a string is a collection of chars)
		string word = "programming";
		var charFreq = word.GroupBy(c => c).Select(g => $"{g.Key}:{g.Count()}");
		Console.WriteLine(string.Join(", ", charFreq));   // o/p: "p:1, r:2, o:1, g:2, a:1, m:2, i:1, n:1"

		// .NET 9+ shortcut: CountBy (gives KeyValuePair<key, count>)
		var charFreq2 = word.CountBy(c => c).Where(kv => kv.Value > 1).Select(kv => kv.Key);
		Console.WriteLine(string.Join(", ", charFreq2));   // o/p: "r, g, m"

		// Most frequent element
		Console.WriteLine(nums.GroupBy(x => x).MaxBy(g => g.Count())?.Key);   // o/p: 3

		// ---------------- 4. Employees by department ----------------
		// Idea: GroupJoin, so that departments with 0 employees also come.

		var byDept = departments.GroupJoin(employees,
										   d => d.DepartmentId,
										   e => e.DepartmentId,
										   (d, emps) => $"{d.DepartmentName}: {(emps.Any() ? string.Join(", ", emps.Select(e => e.EmployeeName)) : "-")}");

		foreach (string line in byDept)
		{
			Console.WriteLine(line);
		}
		// o/p:
		// IT: Bishnu, Amit, Sneha
		// HR: Rahul, Karan, Arjun
		// Finance: Priya, Neha
		// Marketing: -

		// ---------------- 5. Top N salaries ----------------

		// Top 3 employees
		var top3 = employees.OrderByDescending(e => e.Salary).Take(3).Select(e => e.EmployeeName);
		Console.WriteLine(string.Join(", ", top3));   // o/p: "Karan, Amit, Sneha"

		// Top 3 unique salaries
		var top3Salaries = employees.Select(e => e.Salary).Distinct().OrderByDescending(s => s).Take(3);
		Console.WriteLine(string.Join(", ", top3Salaries));   // o/p: "1800000, 1500000, 1400000"

		// Highest paid in each department
		var topPerDept = employees.GroupBy(e => e.DepartmentId).Select(g => g.MaxBy(e => e.Salary)!.EmployeeName);
		Console.WriteLine(string.Join(", ", topPerDept));   // o/p: "Amit, Karan, Neha"

		// Top 2 in each department: sort inside each group, take 2, then flatten
		var top2PerDept = employees.GroupBy(e => e.DepartmentId)
								   .SelectMany(g => g.OrderByDescending(e => e.Salary).Take(2))
								   .Select(e => e.EmployeeName);
		Console.WriteLine(string.Join(", ", top2PerDept));   // o/p: "Amit, Sneha, Karan, Arjun, Neha, Priya"

		// ---------------- 6. Flatten nested collections ----------------

		int[][] matrix = { new[] { 1, 2, 3 }, new[] { 4, 5 }, new[] { 6 } };
		Console.WriteLine(string.Join(", ", matrix.SelectMany(row => row)));   // o/p: "1, 2, 3, 4, 5, 6"

		Dictionary<string, List<string>> skills = new Dictionary<string, List<string>>
		{
			{ "Bishnu", new List<string> { "C#", "SQL" } },
			{ "Amit", new List<string> { "C#", "Azure" } },
			{ "Priya", new List<string> { "SQL", "Excel" } }
		};

		var allSkills = skills.SelectMany(kv => kv.Value).Distinct();
		Console.WriteLine(string.Join(", ", allSkills));   // o/p: "C#, SQL, Azure, Excel"

		// ---------------- 7. Missing elements ----------------
		// Idea: make the full range, then Except the given numbers.

		int[] given = { 1, 2, 4, 6, 7, 9, 10 };
		var missing = Enumerable.Range(1, 10).Except(given);
		Console.WriteLine(string.Join(", ", missing));   // o/p: "3, 5, 8"

		// ---------------- 8. Unique elements ----------------
		// NOTE: Distinct  = remove repeats, keep one copy of each   -> 1, 2, 3, 4, 5, 6
		//       Unique    = only those that appear exactly once     -> 1, 4, 5, 6

		Console.WriteLine(string.Join(", ", nums.Distinct()));   // o/p: "1, 2, 3, 4, 5, 6"

		var uniqueOnly = nums.GroupBy(x => x).Where(g => g.Count() == 1).Select(g => g.Key);
		Console.WriteLine(string.Join(", ", uniqueOnly));   // o/p: "1, 4, 5, 6"

		// First non-repeating character (GroupBy keeps the order of first appearance, so the first group with count 1 is the answer)
		char firstUnique = "swiss".GroupBy(c => c).First(g => g.Count() == 1).Key;
		Console.WriteLine(firstUnique);   // o/p: w

		// ---------------- 9. Employees earning more than their manager ----------------
		// Idea: self join (employee with manager), then compare salaries.

		var moreThanManager = employees.Join(employees, e => e.ManagerId, m => (int?)m.EmployeeId, (e, m) => new { e, m })
									   .Where(x => x.e.Salary > x.m.Salary)
									   .Select(x => $"{x.e.EmployeeName} ({x.e.Salary}) > {x.m.EmployeeName} ({x.m.Salary})");
		Console.WriteLine(string.Join(", ", moreThanManager));   // o/p: "Amit (1500000) > Sneha (1400000)"

		// ---------------- 10. Employees earning more than their department's average ----------------
		// Idea: find average of each department first (Dictionary), then compare each employee with it.

		Dictionary<int, decimal> deptAvg = employees.GroupBy(e => e.DepartmentId).ToDictionary(g => g.Key, g => g.Average(e => e.Salary));

		var aboveAvg = employees.Where(e => e.Salary > deptAvg[e.DepartmentId]).Select(e => e.EmployeeName);
		Console.WriteLine(string.Join(", ", aboveAvg));   // o/p: "Amit, Karan, Sneha, Neha"

		// ---------------- 11. Department with the highest total salary ----------------

		var richestDept = employees.GroupBy(e => e.DepartmentId)
								   .Select(g => new { DeptId = g.Key, Total = g.Sum(e => e.Salary) })
								   .MaxBy(x => x.Total);
		string richestName = departments.First(d => d.DepartmentId == richestDept!.DeptId).DepartmentName;
		Console.WriteLine($"{richestName}: {richestDept!.Total}");   // o/p: "IT: 4100000"

		// ---------------- 12. Departments with no employees ----------------

		var noEmployees = departments.Where(d => !employees.Any(e => e.DepartmentId == d.DepartmentId)).Select(d => d.DepartmentName);
		Console.WriteLine(string.Join(", ", noEmployees));   // o/p: "Marketing"

		// ---------------- 13. Number of employees under each manager ----------------

		var teamSize = employees.Where(e => e.ManagerId != null)
								.GroupBy(e => e.ManagerId)
								.Join(employees, g => g.Key, m => (int?)m.EmployeeId, (g, m) => $"{m.EmployeeName}: {g.Count()}");
		Console.WriteLine(string.Join(", ", teamSize));   // o/p: "Karan: 3, Sneha: 3"

		// ---------------- 14. Anagram and palindrome ----------------
		// Anagram: same letters, different order. Sort both and compare.
		// SequenceEqual: true if both collections have the same items in the same order.

		bool isAnagram = "listen".OrderBy(c => c).SequenceEqual("silent".OrderBy(c => c));
		Console.WriteLine(isAnagram);   // o/p: True

		bool isPalindrome = "madam".SequenceEqual("madam".Reverse());
		Console.WriteLine(isPalindrome);   // o/p: True

		// ---------------- 15. Longest word and reverse each word ----------------

		string sentence = "LINQ makes CSharp awesome";
		string[] words = sentence.Split(' ');

		Console.WriteLine(words.MaxBy(w => w.Length));   // o/p: "awesome"

		string reversedWords = string.Join(" ", words.Select(w => new string(w.Reverse().ToArray())));
		Console.WriteLine(reversedWords);   // o/p: "QNIL sekam prahSC emosewa"

		string reversedOrder = string.Join(" ", words.Reverse());
		Console.WriteLine(reversedOrder);   // o/p: "awesome CSharp makes LINQ"
	}
}
