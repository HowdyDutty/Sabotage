
/*

	This is an implementation of an immutable priorety queue.
	It is implemented with a SortedDictionary. This is a data
 	structure of key-value pairs. I am mainly going to use ints
 	as Keys and a Queue of Tiles as my value. 
	
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PriorityQueue<Key, Value>
{
	private SortedDictionary<Key, Queue<Value>> list = new SortedDictionary<Key, Queue<Value>>();

	public void Enqueue(Key priority, Value value)
	{
		Queue<Value> q;

		// If there is no Queue to Enqueue to, make it and add it to the dictionary.
		if (!list.TryGetValue(priority, out q))
		{
			q = new Queue<Value>();
			list.Add(priority, q);
		}
		q.Enqueue(value);
	}

	public Value Dequeue()
	{
		// If there is no first value, this line will throw an error. 
		var pair = list.First();
		var value = pair.Value.Dequeue();
		if (pair.Value.Count == 0)	// If theres nothing left in the Queue
		{
			list.Remove(pair.Key);	// Remove the entire Queue.
		}
		return value;
	}

	public bool isEmpty()
	{
		return (!list.Any());
	}
}
