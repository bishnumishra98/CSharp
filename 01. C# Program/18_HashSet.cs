// ● HashSet: A HashSet is a collection of UNIQUE items (no duplicates), with no fixed order. It is used to quickly check
//            "is this item present or not ?" and to remove duplicates. Like Dictionary, but it keeps only keys, no values.
//            HashSet<T> is in the System.Collections.Generic namespace.
//            (Hashing works the same way as in Dictionary; see '17_Dictionary.cs'.)

// ● C++ vs C# names:
//   C++ unordered_set   ->  C# HashSet<T>     (hash table, no order)
//   C++ set             ->  C# SortedSet<T>   (balanced BST (Red-Black tree), items always sorted)

// ● Time complexity:
//   Operation               |   HashSet                    |   SortedSet
//   ------------------------------------------------------------------------------
//   Add / Remove / Search   |   Avg: O(1), worst: O(n)     |   O(logn)
//   Min / Max               |   O(n)                       |   O(logn)
//   Order of items          |   Not guaranteed             |   Always sorted (ascending)
//   Use HashSet by default (faster). Use SortedSet when you need sorted items, or the smallest/largest item quickly.

// ● Ways to declare and initialize a HashSet:
//   - Empty set:
//     HashSet<int> s = new HashSet<int>();
//   - With values (duplicates are dropped automatically):
//     HashSet<int> s = new HashSet<int> { 50, 20, 10, 20 };   // s = { 50, 20, 10 }
//   - From an array or list (a quick way to remove duplicates):
//     HashSet<int> s = new HashSet<int>(arr);
//   - Copy of another set:
//     HashSet<int> s2 = new HashSet<int>(s1);

// ● Utility functions and properties of HashSet:
//
//   I.   Adding & Removing:
//        - Add(item): Adds the item and returns true. If it is already present, does nothing and returns false.
//                     (This return value is handy in DSA: "if (!set.Add(x))" means x is a duplicate.)
//        - Remove(item): Removes the item. Returns true if removed, false if it was not present.
//        - Clear(): Removes all items.
//
//   II.  Searching:
//        - Contains(item): Returns true if the item is present. O(1). (List.Contains() is O(n); that's the big difference.)
//
//   III. Properties:
//        - Count: Number of items.
//
//   IV.  Set operations (change the set itself):
//        - UnionWith(other): Keeps the items present in either set.        A = {1, 2, 3}, B = {2, 3, 4}  ->  {1, 2, 3, 4}
//        - IntersectWith(other): Keeps only the items present in both.     ->  {2, 3}
//        - ExceptWith(other): Removes the items that are present in other. ->  {1}
//
//   V.   Set checks (return true/false):
//        - IsSubsetOf(other): Are all items of this set also in other ?
//        - IsSupersetOf(other): Are all items of other also in this set ?
//        - Overlaps(other): Is at least one item common ?
//        - SetEquals(other): Do both have exactly the same items ?
//
//   VI.  SortedSet extras:
//        - Min, Max: Smallest and largest item.
//        - Reverse(): Items from largest to smallest.
//        - GetViewBetween(low, high): All items between low and high (both included).
//        - Descending order set: new SortedSet<int>(Comparer<int>.Create((a, b) => b.CompareTo(a)))
//          (Same as C++ set<int, greater<int>>.)


// 1. Creating and adding
HashSet<int> s = new HashSet<int>();

s.Add(10);
s.Add(20);
s.Add(30);
Console.WriteLine(s.Add(20));   // o/p: "False" (20 is already present)

Console.WriteLine("Count: " + s.Count);   // o/p: "Count: 3"

// 2. Searching
Console.WriteLine(s.Contains(20));   // o/p: "True"
Console.WriteLine(s.Contains(99));   // o/p: "False"

// 3. Removing
Console.WriteLine(s.Remove(10));   // o/p: "True"
Console.WriteLine(s.Remove(99));   // o/p: "False"

// 4. Iterating
foreach (int item in s)
{
	Console.Write(item + " ");   // o/p: "20 30 " (order is not guaranteed)
}
Console.WriteLine();

s.Clear();
Console.WriteLine("Count after clear: " + s.Count);   // o/p: "Count after clear: 0"

// ------------------------------------------------------------------------------

// 5. Removing duplicates from an array
int[] arr = { 50, 20, 10, 30, 40, 20, 40, 50 };
HashSet<int> unique = new HashSet<int>(arr);
Console.WriteLine(string.Join(", ", unique));   // o/p: "50, 20, 10, 30, 40"

// 6. Most used DSA pattern: Finding the first duplicate
int[] nums = { 3, 1, 4, 1, 5, 9 };
HashSet<int> seen = new HashSet<int>();
foreach (int x in nums)
{
	if (!seen.Add(x))   // Add() returns false, so x was already seen
	{
		Console.WriteLine("First duplicate: " + x);   // o/p: "First duplicate: 1"
		break;
	}
}

// ------------------------------------------------------------------------------

// 7. Set operations
HashSet<int> a = new HashSet<int> { 1, 2, 3 };
HashSet<int> b = new HashSet<int> { 2, 3, 4 };

HashSet<int> all = new HashSet<int>(a);   // work on a copy, so 'a' stays the same
all.UnionWith(b);
Console.WriteLine("Union: " + string.Join(", ", all));   // o/p: "Union: 1, 2, 3, 4"

HashSet<int> common = new HashSet<int>(a);
common.IntersectWith(b);
Console.WriteLine("Intersection: " + string.Join(", ", common));   // o/p: "Intersection: 2, 3"

HashSet<int> onlyA = new HashSet<int>(a);
onlyA.ExceptWith(b);
Console.WriteLine("Only in a: " + string.Join(", ", onlyA));   // o/p: "Only in a: 1"

Console.WriteLine(common.IsSubsetOf(a));   // o/p: "True"
Console.WriteLine(a.Overlaps(b));          // o/p: "True"

// ------------------------------------------------------------------------------

// 8. SortedSet: Same functions as HashSet, but items are always kept in sorted order.
SortedSet<int> ss = new SortedSet<int> { 50, 20, 10, 30, 40, 20, 40, 50 };
Console.WriteLine(string.Join(", ", ss));                          // o/p: "10, 20, 30, 40, 50"
Console.WriteLine("Min: " + ss.Min + ", Max: " + ss.Max);          // o/p: "Min: 10, Max: 50"
Console.WriteLine(string.Join(", ", ss.Reverse()));                // o/p: "50, 40, 30, 20, 10"
Console.WriteLine(string.Join(", ", ss.GetViewBetween(20, 40)));   // o/p: "20, 30, 40"

SortedSet<int> desc = new SortedSet<int>(arr, Comparer<int>.Create((x, y) => y.CompareTo(x)));
Console.WriteLine(string.Join(", ", desc));   // o/p: "50, 40, 30, 20, 10"
