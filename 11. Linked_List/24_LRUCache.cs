// Leetcode: 146. LRU Cache   --->   Design a data structure that follows the constraints of a Least Recently Used (LRU)
// cache.
// Implement the LRUCache class:
// • LRUCache(int capacity) Initialize the LRU cache with positive size capacity.
// • int get(int key) Return the value of the key if the key exists, otherwise return -1.
// • void put(int key, int value) Update the value of the key if the key exists. Otherwise, add the key-value pair to the
//   cache. If the number of keys exceeds the capacity from this operation, evict the least recently used key.
// The functions get and put must each run in O(1) average time complexity.

// Example 1:
// Input: ["LRUCache", "put", "put", "get", "put", "get", "put", "get", "get", "get"]
//        [[2], [1, 1], [2, 2], [1], [3, 3], [2], [4, 4], [1], [3], [4]]
// Output: [null, null, null, 1, null, -1, null, -1, 3, 4]
// Explanation:
// LRUCache lRUCache = new LRUCache(2);
// lRUCache.put(1, 1); // cache is {1=1}
// lRUCache.put(2, 2); // cache is {1=1, 2=2}
// lRUCache.get(1);    // return 1  (now 1 is the most recently used, 2 is the least)
// lRUCache.put(3, 3); // LRU key was 2, evicts key 2, cache is {1=1, 3=3}
// lRUCache.get(2);    // returns -1 (not found)
// lRUCache.put(4, 4); // LRU key was 1, evicts key 1, cache is {4=4, 3=3}
// lRUCache.get(1);    // return -1 (not found)
// lRUCache.get(3);    // return 3
// lRUCache.get(4);    // return 4

// What is a cache ? A small, fast storage that keeps only a few items. When it is full and a new item comes, one old item
// must go. LRU = throw out the item that was used (get or put) the LONGEST time ago.


using System;
using System.Collections.Generic;

// Brute force: A List of (key, value) pairs, ordered from least recently used (front) to most recently used (back).
// • Get: search the list for the key. If found, move it to the back (it was just used).
// • Put: if the key exists, remove it. If the cache is full, remove the front (least recently used). Add the pair at the back.
// Problem: Searching the List and removing from it are O(n). The problem needs O(1).
// T.C: O(n) per get / put;   where n = capacity
// S.C: O(n)
public class LRUCache_List
{
	private readonly int capacity;
	private readonly List<(int key, int value)> items = new List<(int key, int value)>();

	public LRUCache_List(int capacity) { this.capacity = capacity; }

	public int Get(int key)
	{
		int i = items.FindIndex(p => p.key == key);
		if (i == -1) return -1;

		var pair = items[i];
		items.RemoveAt(i);
		items.Add(pair);   // most recently used goes to the back
		return pair.value;
	}

	public void Put(int key, int value)
	{
		int i = items.FindIndex(p => p.key == key);
		if (i != -1) items.RemoveAt(i);
		else if (items.Count == capacity) items.RemoveAt(0);   // evict the least recently used

		items.Add((key, value));
	}
}

// ------------------------------------------------------------------------------

// Optimal: Dictionary + Doubly linked list.
// • The doubly linked list keeps the ORDER of use: right after 'head' is the most recently used, right before 'tail' is
//   the least recently used. Moving or removing a node is O(1), because each node knows its prev and next.
// • The Dictionary maps key -> its node, so we can FIND any node in O(1) without walking the list.
// • 'head' and 'tail' are dummy nodes (never removed). With them, every real node always has a prev and a next, so there
//   are no null checks when adding or removing.
//
//      head <=> [most recent] <=> ... <=> [least recent] <=> tail
//
// • Get(key): not in the Dictionary -> -1. Else move its node to the front (just used), and return its value.
// • Put(key, value): key exists -> update the value and move the node to the front.
//                    New key -> if full, remove the node before 'tail' (least recent) from the list AND the Dictionary.
//                               Then add a new node at the front, and put it in the Dictionary.
// (The node stores its key too, so that when we evict a node from the list, we know which key to remove from the
//  Dictionary.)
// T.C: O(1) per get / put
// S.C: O(capacity)
public class LRUCache
{
	private class Node
	{
		public int key, value;
		public Node prev, next;
		public Node(int key, int value) { this.key = key; this.value = value; }
	}

	private readonly int capacity;
	private readonly Dictionary<int, Node> map = new Dictionary<int, Node>();
	private readonly Node head = new Node(0, 0), tail = new Node(0, 0);   // dummy nodes

	public LRUCache(int capacity)
	{
		this.capacity = capacity;
		head.next = tail;
		tail.prev = head;
	}

	public int Get(int key)
	{
		if (!map.TryGetValue(key, out Node node)) return -1;

		Remove(node);
		AddToFront(node);
		return node.value;
	}

	public void Put(int key, int value)
	{
		if (map.TryGetValue(key, out Node node))
		{
			node.value = value;
			Remove(node);
			AddToFront(node);
			return;
		}

		if (map.Count == capacity)
		{
			Node lru = tail.prev;   // least recently used
			Remove(lru);
			map.Remove(lru.key);
		}

		Node newNode = new Node(key, value);
		AddToFront(newNode);
		map[key] = newNode;
	}

	// Unlink a node: its neighbours point to each other.
	private void Remove(Node node)
	{
		node.prev.next = node.next;
		node.next.prev = node.prev;
	}

	// Insert a node right after head (most recently used position).
	// Set the new node's own links first, then change the neighbours' links. In the other order, we would lose head.next.
	private void AddToFront(Node node)
	{
		node.next = head.next;
		node.prev = head;
		head.next.prev = node;
		head.next = node;
	}
}

// Note: C# also has built-in pieces for this: LinkedList<T> (a doubly linked list) together with a
// Dictionary<int, LinkedListNode<T>>. Interviewers usually want the hand-written version above.


class Program
{
	static void Main()
	{
		LRUCache_List cache1 = new LRUCache_List(2);
		cache1.Put(1, 1);
		cache1.Put(2, 2);
		Console.Write(cache1.Get(1) + " ");   // 1
		cache1.Put(3, 3);                     // evicts 2
		Console.Write(cache1.Get(2) + " ");   // -1
		cache1.Put(4, 4);                     // evicts 1
		Console.Write(cache1.Get(1) + " ");   // -1
		Console.Write(cache1.Get(3) + " ");   // 3
		Console.WriteLine(cache1.Get(4));     // o/p: 1 -1 -1 3 4

		LRUCache cache2 = new LRUCache(2);
		cache2.Put(1, 1);
		cache2.Put(2, 2);
		Console.Write(cache2.Get(1) + " ");
		cache2.Put(3, 3);
		Console.Write(cache2.Get(2) + " ");
		cache2.Put(4, 4);
		Console.Write(cache2.Get(1) + " ");
		Console.Write(cache2.Get(3) + " ");
		Console.WriteLine(cache2.Get(4));     // o/p: 1 -1 -1 3 4
	}
}
