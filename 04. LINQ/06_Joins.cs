// ● Join: Combines 2 collections using a common key. (Like JOIN in SQL)
//   Here employees and departments are connected by DepartmentId.

// ● I. Join (INNER JOIN): Gives only those pairs where the key matches on BOTH sides.
//   - Marketing has no employee, so Marketing will NOT come in the result.
//   - Method syntax: outer.Join(inner, outerKey, innerKey, (outerItem, innerItem) => result)
//        employees.Join(departments,
//                       e => e.DepartmentId,                  // key from employees
//                       d => d.DepartmentId,                  // key from departments
//                       (e, d) => $"{e.EmployeeName} - {d.DepartmentName}")
//   - Query syntax (easier to read):
//        from e in employees
//        join d in departments on e.DepartmentId equals d.DepartmentId
//        select $"{e.EmployeeName} - {d.DepartmentName}"
//     NOTE: Use 'equals', not '=='. The left side of 'equals' must be from the first collection.

// ● II. GroupJoin: For each item of the 1st collection, gives a GROUP of matching items from the 2nd collection.
//   - The group can be empty. So Marketing comes with 0 employees.
//   - Perfect for "each department with its list of employees".
//        departments.GroupJoin(employees,
//                              d => d.DepartmentId,
//                              e => e.DepartmentId,
//                              (d, emps) => ...)          // emps = all employees of department d

// ● III. LEFT JOIN: ALL items from the left side, and matching items from the right side (or null if no match).
//   C# had no LeftJoin method before .NET 10. The classic way is GroupJoin + DefaultIfEmpty():
//        from d in departments
//        join e in employees on d.DepartmentId equals e.DepartmentId into emps   // 'into' makes it a GroupJoin
//        from e in emps.DefaultIfEmpty()                                          // empty group -> one null
//        select $"{d.DepartmentName} - {e?.EmployeeName ?? "No employee"}"
//   - DefaultIfEmpty(): if the group is empty, gives one default item (null), so the left item is not lost.
//   - .NET 10 added LeftJoin() and RightJoin() methods. Interviews still ask for the GroupJoin way.

// ● IV. Self join: Joining a collection with itself. Example: employee with their manager (manager is also an employee).
// ● V.  Joining multiple collections: Just write one 'join' after another (query syntax is much cleaner here).

// ● NOTE: Both keys must be of the same type. ManagerId is int? and EmployeeId is int, so we write (int?)m.EmployeeId.

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

		// ---------------- I. Join (INNER JOIN) ----------------

		var innerMethod = employees.Join(departments,
										 e => e.DepartmentId,
										 d => d.DepartmentId,
										 (e, d) => $"{e.EmployeeName} - {d.DepartmentName}");
		Console.WriteLine(string.Join(", ", innerMethod));
		// o/p: "Bishnu - IT, Rahul - HR, Amit - IT, Priya - Finance, Karan - HR, Sneha - IT, Neha - Finance, Arjun - HR"
		// (No Marketing, because no employee has DepartmentId 4)

		var innerQuery = from e in employees
						 join d in departments on e.DepartmentId equals d.DepartmentId
						 where d.DepartmentName == "HR"
						 select $"{e.EmployeeName} - {d.DepartmentName}";
		Console.WriteLine(string.Join(", ", innerQuery));   // o/p: "Rahul - HR, Karan - HR, Arjun - HR"

		// ---------------- II. GroupJoin ----------------

		var deptWithEmployees = departments.GroupJoin(employees,
													  d => d.DepartmentId,
													  e => e.DepartmentId,
													  (d, emps) => $"{d.DepartmentName} ({emps.Count()}): {string.Join(", ", emps.Select(e => e.EmployeeName))}");

		foreach (string line in deptWithEmployees)
		{
			Console.WriteLine(line);
		}
		// o/p:
		// IT (3): Bishnu, Amit, Sneha
		// HR (3): Rahul, Karan, Arjun
		// Finance (2): Priya, Neha
		// Marketing (0):

		// ---------------- III. LEFT JOIN (GroupJoin + DefaultIfEmpty) ----------------

		var leftJoin = from d in departments
					   join e in employees on d.DepartmentId equals e.DepartmentId into emps
					   from e in emps.DefaultIfEmpty()
					   select $"{d.DepartmentName} - {e?.EmployeeName ?? "No employee"}";

		foreach (string line in leftJoin)
		{
			Console.WriteLine(line);
		}
		// o/p:
		// IT - Bishnu
		// IT - Amit
		// IT - Sneha
		// HR - Rahul
		// HR - Karan
		// HR - Arjun
		// Finance - Priya
		// Finance - Neha
		// Marketing - No employee

		// Same thing using the new LeftJoin method (.NET 10+)
		var leftJoinNew = departments.LeftJoin(employees,
											   d => d.DepartmentId,
											   e => e.DepartmentId,
											   (d, e) => $"{d.DepartmentName} - {e?.EmployeeName ?? "No employee"}");
		Console.WriteLine(leftJoinNew.Last());   // o/p: "Marketing - No employee"

		// ---------------- IV. Self join (employee -> manager) ----------------

		var withManager = from e in employees
						  join m in employees on e.ManagerId equals (int?)m.EmployeeId
						  select $"{e.EmployeeName} -> {m.EmployeeName}";
		Console.WriteLine(string.Join(", ", withManager));
		// o/p: "Bishnu -> Karan, Rahul -> Karan, Amit -> Sneha, Priya -> Sneha, Neha -> Karan, Arjun -> Sneha"
		// (Karan and Sneha are missing, because they have no manager. Inner join drops them.)

		var withManagerLeft = from e in employees
							  join m in employees on e.ManagerId equals (int?)m.EmployeeId into managers
							  from m in managers.DefaultIfEmpty()
							  where m == null   // show only the employees who got no match (left join gives them a null manager)
							  select $"{e.EmployeeName} -> {m?.EmployeeName ?? "No manager"}";
		Console.WriteLine(string.Join(", ", withManagerLeft));   // o/p: "Karan -> No manager, Sneha -> No manager"

		// ---------------- V. Joining multiple collections ----------------
		// employee + their department + their manager

		var report = from e in employees
					 join d in departments on e.DepartmentId equals d.DepartmentId
					 join m in employees on e.ManagerId equals (int?)m.EmployeeId
					 orderby d.DepartmentName, e.EmployeeName
					 select $"{e.EmployeeName} | {d.DepartmentName} | Manager: {m.EmployeeName}";

		foreach (string line in report)
		{
			Console.WriteLine(line);
		}
		// o/p:
		// Neha | Finance | Manager: Karan
		// Priya | Finance | Manager: Sneha
		// Arjun | HR | Manager: Sneha
		// Rahul | HR | Manager: Karan
		// Amit | IT | Manager: Sneha
		// Bishnu | IT | Manager: Karan
	}
}
