// ● EF Core (Entity Framework Core): A library that lets us talk to a database using C# classes and LINQ, instead of
//   writing SQL by hand. EF Core converts our LINQ into SQL, runs it on the database, and gives back C# objects.
//
//   class AppDbContext : DbContext
//   {
//       public DbSet<Employee> Employees { get; set; }      // DbSet = a table
//   }
//
//   var rich = context.Employees.Where(e => e.Salary > 1000000).ToList();
//   // EF Core sends: SELECT * FROM Employees WHERE Salary > 1000000

// ==================================================================================================================

// ● I. Deferred vs Immediate execution
//   - Deferred (lazy): Writing a query does NOT run it. It only stores the "recipe". It runs when we actually
//     use the result (foreach, ToList, Count...). And it runs AGAIN every time we use it.
//     Deferred methods: Where, Select, OrderBy, GroupBy, Join, Take, Skip, Distinct...
//   - Immediate: Runs right away and gives the final result.
//     Immediate methods: ToList, ToArray, ToDictionary, ToLookup, Count, Sum, Max, First, Single, Any...
//   - Rule of thumb: if the method gives back a collection (IEnumerable), it's deferred. If it gives back a
//     List / single value / bool, it's immediate.
//
//   Why it matters:
//   - Problem: Using a deferred query 2 times runs it 2 times. On a database, that's 2 trips to the database.
//   - Solution: Call ToList() once and reuse the list.

// ● II. IEnumerable<T> vs IQueryable<T>
//
//   Point          | IEnumerable<T>                          | IQueryable<T>
//   Namespace      | System.Collections.Generic              | System.Linq
//   Runs where     | In our app's memory (C#)                | At the data source (database)
//   Filtering      | Brings ALL rows first, then filters     | Filters in the database (WHERE in SQL)
//                  | in memory                               |
//   Lambda becomes | Normal compiled code (Func<T, bool>)    | Expression tree (Expression<Func<T, bool>>), which
//                  |                                         | EF Core reads and converts to SQL
//   Used for       | List, array, in-memory data             | Database queries (EF Core DbSet)
//
//   Problem:
//        IEnumerable<Employee> all = context.Employees;            // treated as IEnumerable
//        var rich = all.Where(e => e.Salary > 1000000).ToList();    // loads the WHOLE table, then filters in C#
//   Solution:
//        IQueryable<Employee> all = context.Employees;             // stays IQueryable
//        var rich = all.Where(e => e.Salary > 1000000).ToList();    // SQL: ... WHERE Salary > 1000000
//
//   Same mistake in another form: calling ToList() / AsEnumerable() too early.
//        context.Employees.ToList().Where(...)     // Bad: whole table comes into memory
//        context.Employees.Where(...).ToList()     // Good: only matching rows come

// ● III. ToList() vs ToListAsync()
//   - ToList(): Runs the query and WAITS. The thread is blocked until the database replies.
//   - ToListAsync(): Runs the query without blocking. The thread is free to do other work (serve other web requests)
//     while the database works. Must be used with 'await'.
//        List<Employee> list = await context.Employees.Where(e => e.DepartmentId == 1).ToListAsync();
//   - Async versions of other immediate methods: FirstOrDefaultAsync, SingleOrDefaultAsync, CountAsync, AnyAsync,
//     SumAsync, MaxAsync, ToDictionaryAsync...
//   - They come from 'using Microsoft.EntityFrameworkCore;' and work only on IQueryable from EF Core (not on a List).
//   - In web APIs (ASP.NET Core), always prefer the Async versions.

// ● IV. AsNoTracking()
//   - By default, EF Core "tracks" every object it loads. It keeps a copy to find what changed, so that
//     SaveChanges() can update the database.
//   - For read-only queries (just showing data), tracking is a waste of memory and time.
//   - AsNoTracking() tells EF Core: "I will not update these, don't track them". Faster and lighter.
//        var list = await context.Employees.AsNoTracking().Where(e => e.Salary > 1000000).ToListAsync();
//   - Don't use it if you are going to change the objects and call SaveChanges().

// ● V. SQL translation
//   - EF Core converts the LINQ into SQL:
//        var result = await context.Employees
//                                  .AsNoTracking()
//                                  .Where(e => e.Salary > 1000000)
//                                  .OrderByDescending(e => e.Salary)
//                                  .Select(e => new { e.EmployeeName, e.Salary })
//                                  .Take(3)
//                                  .ToListAsync();
//     SQL generated (SQL Server, roughly):
//        SELECT TOP(3) [e].[EmployeeName], [e].[Salary]
//        FROM [Employees] AS [e]
//        WHERE [e].[Salary] > 1000000.0
//        ORDER BY [e].[Salary] DESC
//   - Where -> WHERE, OrderBy -> ORDER BY, Select -> SELECT columns, Take -> TOP / LIMIT, Skip -> OFFSET,
//     GroupBy -> GROUP BY, Join -> INNER JOIN, Count -> COUNT(*), Any -> EXISTS.
//   - See the SQL without running it: query.ToQueryString()
//   - Use Select to bring only the columns you need, not the whole row.
//   - Not everything can be converted. Our own C# method inside Where can't become SQL:
//        context.Employees.Where(e => IsRich(e))   // throws: "The LINQ expression ... could not be translated"
//     Fix: write the condition directly (e => e.Salary > 1000000), or filter in memory AFTER ToList() (only if the data is small).
//   - Related data: use Include() to load it in the same query, else you may get the "N + 1 problem"
//     (1 query for employees + 1 query for EACH employee's department).
//        context.Employees.Include(e => e.Department).ToListAsync();

// ==================================================================================================================
// The code below runs WITHOUT a database. It shows deferred execution and IQueryable using in-memory lists.
// (EF Core needs a NuGet package and a database, so its code is shown in comments above.)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

class Employee
{
	public int EmployeeId { get; set; }
	public string EmployeeName { get; set; } = "";
	public decimal Salary { get; set; }
	public int DepartmentId { get; set; }
	public int? ManagerId { get; set; }
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

		// ---------------- Deferred execution: query runs when used, not when written ----------------

		var richQuery = employees.Where(e => e.Salary > 1500000);   // nothing runs here, only the recipe is stored
		List<Employee> richList = richQuery.ToList();               // runs NOW, result is saved

		employees.Add(new Employee { EmployeeId = 9, EmployeeName = "Rohit", Salary = 2000000, DepartmentId = 1 });   // added AFTER

		Console.WriteLine(string.Join(", ", richQuery.Select(e => e.EmployeeName)));   // o/p: "Karan, Rohit"   (runs again, sees Rohit)
		Console.WriteLine(string.Join(", ", richList.Select(e => e.EmployeeName)));    // o/p: "Karan"          (old saved result)

		employees.RemoveAt(employees.Count - 1);   // remove Rohit

		// ---------------- Deferred query runs again on every use ----------------

		int checks = 0;
		var counted = employees.Where(e => { checks++; return e.DepartmentId == 1; });   // counts how many times the condition runs

		Console.WriteLine(checks);   // o/p: 0    (not run yet)

		counted.Count();
		counted.Count();
		Console.WriteLine(checks);   // o/p: 16   (8 employees x 2 times. On a database, 2 trips!)

		checks = 0;
		var once = counted.ToList();   // run once, store it
		once.Count();
		once.Count();
		Console.WriteLine(checks);   // o/p: 8    (ran only once)

		// ---------------- Func vs Expression (IEnumerable vs IQueryable) ----------------

		Func<Employee, bool> func = e => e.Salary > 1000000;                    // compiled code: can only be run
		Expression<Func<Employee, bool>> expr = e => e.Salary > 1000000;        // data: can be read and converted to SQL

		Console.WriteLine(expr);        // o/p: "e => (e.Salary > 1000000)"
		Console.WriteLine(expr.Body);   // o/p: "(e.Salary > 1000000)"

		IQueryable<Employee> queryable = employees.AsQueryable().Where(e => e.Salary > 1000000).OrderBy(e => e.EmployeeName);
		Console.WriteLine(queryable.Expression);
		// o/p: "System.Collections.Generic.List`1[Employee].Where(e => (e.Salary > 1000000)).OrderBy(e => e.EmployeeName)"
		// An IQueryable keeps the whole query as an expression tree. EF Core reads this tree and builds SQL from it.

		Console.WriteLine(string.Join(", ", queryable.Select(e => e.EmployeeName)));   // o/p: "Amit, Arjun, Bishnu, Karan, Sneha"
	}
}
