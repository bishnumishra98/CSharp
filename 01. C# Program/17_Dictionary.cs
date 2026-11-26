// ● Dictionary: A Dictionary is a collection of key-value pairs. Every key is unique, and each key points to one value.
//               It is used to store and find data quickly by its key. e.g., name -> phone number, char -> its count.
//               Dictionary<TKey, TValue> is in the System.Collections.Generic namespace.
//               (C++ calls this a "map". C# has no class named Map; Dictionary is its equivalent.)

// ● C++ vs C# names:
//   C++ unordered_map   ->  C# Dictionary<TKey, TValue>         (hash table, no order)
//   C++ map             ->  C# SortedDictionary<TKey, TValue>   (balanced BST (Red-Black tree), keys always sorted)

// ● Time complexity:
//   Operation               |   Dictionary                 |   SortedDictionary
//   ------------------------------------------------------------------------------
//   Add / Remove / Search   |   Avg: O(1), worst: O(n)     |   O(logn)
//   Order of keys           |   Not guaranteed             |   Always sorted (ascending)
//   Use Dictionary by default (faster). Use SortedDictionary only when you need the keys in sorted order.

// ● Ways to declare and initialize a Dictionary:
//   - Empty dictionary:
//     Dictionary<int, string> d = new Dictionary<int, string>();
//   - With values (collection initializer):
//     Dictionary<string, int> ages = new Dictionary<string, int> { { "Alice", 25 }, { "Bob", 30 } };
//     Dictionary<string, int> ages = new Dictionary<string, int> { ["Alice"] = 25, ["Bob"] = 30 };   // same thing

// ● Utility functions and properties of Dictionary:
//
//   I.   Adding & Updating:
//        - Add(key, value): Adds a new pair. Throws an exception if the key already exists.
//        - TryAdd(key, value): Adds a new pair and returns true. If the key already exists, does nothing and returns false.
//        - d[key] = value: Adds the pair if the key is new, else updates (overwrites) the old value. No exception.
//
//   II.  Reading values:
//        - d[key]: Returns the value of the key. Throws KeyNotFoundException if the key is not present.
//        - TryGetValue(key, out value): Returns true and gives the value if the key is present, else returns false.
//        - GetValueOrDefault(key): Returns the value if the key is present, else the default value (0 for int, null for string).
//        Note for C++ users: In C++, um[key] on a missing key silently CREATES the key with value 0. In C#, reading d[key]
//        on a missing key does NOT create it; it throws an exception. So check with ContainsKey() / TryGetValue() first.
//
//   III. Searching:
//        - ContainsKey(key): Returns true if the key is present. O(1).
//        - ContainsValue(value): Returns true if the value is present. O(n), because it checks every value.
//
//   IV.  Removing:
//        - Remove(key): Removes the pair with that key. Returns true if removed, false if the key was not present.
//        - Clear(): Removes all pairs.
//
//   V.   Properties:
//        - Count: Number of key-value pairs.
//        - Keys: All the keys.
//        - Values: All the values.
//
//   VI.  Iterating:
//        - foreach (var pair in d): Each 'pair' is a KeyValuePair<TKey, TValue>, with pair.Key and pair.Value.
//        - foreach (var (key, value) in d): Same thing, but splits the pair into 2 variables directly (deconstruction).
//        Note: You can't Add/Remove items inside a foreach over the same dictionary. It throws an exception.

// ● Hashing (how a Dictionary works inside):
//   1. Hash function: Converts a key into a number (hash code). In C#, every object has GetHashCode() for this.
//      From the hash code, the Dictionary picks a "bucket" (slot) to keep the pair in. That's why search is O(1):
//      it directly jumps to the bucket instead of checking every item.
//   2. Collision: 2 different keys can get the same bucket. There are 2 common ways to handle it:
//      i.  Chaining (open hashing): Each bucket keeps a small list of all pairs that landed there. C# Dictionary uses this.
//      ii. Open addressing (closed hashing): If the bucket is full, look for another empty bucket:
//          -> Linear probing:    next index = (h + i) % n
//          -> Quadratic probing: next index = (h + i^2) % n
//          -> Double hashing:    next index = (h1(key) + i * h2(key)) % n
//          where h = hash of the key, i = attempt number (1, 2, 3, ...), n = number of buckets
//   3. Load factor = number of items / number of buckets. When it gets too high, collisions increase. So the Dictionary
//      grows (about 2x buckets) and re-places all items. That's why Add is O(1) on average, but sometimes slower.
//   4. Worst case O(n): If many keys land in the same bucket, searching that bucket's list takes O(n). Rare in practice.
//   Note: For your own class as a key, override Equals() and GetHashCode() (or use a 'record', which does it for you).
//         Otherwise, two objects with the same data are treated as different keys.


// 1. Creating and adding
Dictionary<int, string> d = new Dictionary<int, string>();

d.Add(1, "one");
d.Add(2, "two");
d[3] = "three";   // adds, as key 3 is new
d[3] = "THREE";   // updates, as key 3 already exists
Console.WriteLine(d.TryAdd(2, "TWO"));   // o/p: "False" (key 2 already exists, value stays "two")
// d.Add(2, "TWO");   // Error: ArgumentException, as key 2 already exists

Console.WriteLine("Count: " + d.Count);   // o/p: "Count: 3"

// 2. Reading values
Console.WriteLine(d[1]);   // o/p: "one"
// Console.WriteLine(d[10]);   // Error: KeyNotFoundException, as key 10 doesn't exist

if (d.TryGetValue(3, out var value)) Console.WriteLine("Key 3 has: " + value);   // o/p: "Key 3 has: THREE"

Console.WriteLine(d.GetValueOrDefault(10, "not found"));   // o/p: "not found"

// 3. Searching
Console.WriteLine(d.ContainsKey(2));        // o/p: "True"
Console.WriteLine(d.ContainsValue("ten"));  // o/p: "False"

// 4. Iterating
foreach (var pair in d)
{
	Console.WriteLine(pair.Key + ": " + pair.Value);   // o/p: "1: one", "2: two", "3: THREE"
}

foreach (var (key, val) in d)
{
	Console.WriteLine(key + " -> " + val);   // o/p: "1 -> one", "2 -> two", "3 -> THREE"
}

Console.WriteLine("Keys: " + string.Join(", ", d.Keys));       // o/p: "Keys: 1, 2, 3"
Console.WriteLine("Values: " + string.Join(", ", d.Values));   // o/p: "Values: one, two, THREE"

// 5. Removing
Console.WriteLine(d.Remove(2));    // o/p: "True"
Console.WriteLine(d.Remove(20));   // o/p: "False"
Console.WriteLine("Count after remove: " + d.Count);   // o/p: "Count after remove: 2"

d.Clear();
Console.WriteLine("Count after clear: " + d.Count);   // o/p: "Count after clear: 0"

// ------------------------------------------------------------------------------

// 6. Most used DSA pattern: Counting frequency (how many times each item comes)
string str = "aabccdefabcafedbcafedfa";
Dictionary<char, int> freq = new Dictionary<char, int>();

foreach (char ch in str)
{
	freq[ch] = freq.GetValueOrDefault(ch) + 1;   // if 'ch' is new, GetValueOrDefault gives 0, so it becomes 1
}

foreach (var (ch, count) in freq)
{
	Console.Write(ch + ":" + count + " ");   // o/p: "a:6 b:3 c:4 d:3 e:3 f:4 "
}
Console.WriteLine();

// ------------------------------------------------------------------------------

// 7. SortedDictionary: Same functions as Dictionary, but the keys are always kept in sorted order.
SortedDictionary<int, string> sd = new SortedDictionary<int, string>();
sd[3] = "three";
sd[1] = "one";
sd[4] = "four";
sd[2] = "two";

Console.WriteLine(string.Join(", ", sd));   // o/p: "[1, one], [2, two], [3, three], [4, four]"

// Note: A normal Dictionary often prints in the order you added items, but this is NOT guaranteed (it can change after
// removing items). If you need a fixed order, use SortedDictionary (sorted by key), or sort the keys yourself.
