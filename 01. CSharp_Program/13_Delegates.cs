// ● Delegates: A delegate is a type-safe function pointer in C#.
//              Type-safe means that the signature of the method being referenced must match the signature defined by the delegate.
//              In simple words, a delegate is a variable that can store the reference of a method. You can then call that method
//              through the delegate.

// ● Steps to use a delegate:
//   1. Declare a delegate type.
//   2. Create an instance of the delegate and assign a method matches the signature of the delegate.
//   3. Invoke the delegate

using System;
class Program
{
    static void Add(int a, int b)
    {
        Console.WriteLine(a + b);
    }

    // 1. Declare a delegate type.
    public delegate void MyDelegate(int x, int y);

    static void Main()
    {
        // 2. Create an instance of the delegate and assign a method matches the signature of the delegate.
        MyDelegate obj = Add;

        // 3. Invoke the delegate
        obj(10, 20);   // o/p: 30
    }
}
