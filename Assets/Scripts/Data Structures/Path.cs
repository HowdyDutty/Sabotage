
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class Path<T> : IEnumerable<T>
{
	public T LastStep 				{ get; private set; }
	public Path<T> PreviousSteps	{ get; private set; }
	public int TotalCost 			{ get; private set; }
	private int numTilesInPath;

	// Constructors.
	private Path(T lastStep, Path<T> previousSteps, int totalCost)
	{
		LastStep = lastStep;
		PreviousSteps = previousSteps;
		TotalCost = totalCost;
	}

	public Path(T start) : this(start, null, 0) {}

	// Methods.
	public Path<T> AddStep(T step, int stepCost)
	{
		return new Path<T>(step, this, TotalCost + stepCost);
	}

	public IEnumerator<T> GetEnumerator()
	{
		List<T> reverseOrder = new List<T>();
		for (Path<T> p = this; p.PreviousSteps != null; p = p.PreviousSteps)
		{
			reverseOrder.Add(p.LastStep);
		}
		reverseOrder.Reverse();

		foreach (T t in reverseOrder)
		{
			yield return t;
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	public int getNumTilesInPath()
	{
		for (Path<T> p = this; p.PreviousSteps != null; p = p.PreviousSteps)
		{
			numTilesInPath++;
		}
		return numTilesInPath;
	}
}