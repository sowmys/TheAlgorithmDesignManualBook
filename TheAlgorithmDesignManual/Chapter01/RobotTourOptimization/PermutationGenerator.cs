using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace AlgorithmDesignManaual.Chapter01.RobotTourOptimization
{
	/*
	Generates permutations of given array. This class uses Heap's algorithm as described
	at https://en.wikipedia.org/wiki/Heap%27s_algorithm. 
	 */
	public class PermutationGenerator<T> : IEnumerable<T[]>
	{
		private T[] values;
		public PermutationGenerator(T[] values)
		{
			this.values = values;
		}
		public IEnumerator<T[]> GetEnumerator()
		{
			return new PermutationGeneratorImpl(values);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new PermutationGeneratorImpl(values);
		}

		private class PermutationGeneratorImpl : IEnumerator<T[]>
		{
			private T[] values;
			private T[] currentValues;
			private int[] indexes;
			private int currentIndexIndex;

			public PermutationGeneratorImpl(T[] values)
			{
				this.values = values;
				this.currentValues = values.ToArray();
				this.indexes = new int[values.Length];
				this.currentIndexIndex = 0;
			}

			public T[] Current => currentValues;

			object IEnumerator.Current => currentValues;

			public void Dispose()
			{
			}

			public bool MoveNext()
			{
				if (currentIndexIndex == values.Length) return false;
				if (indexes[currentIndexIndex] < currentIndexIndex)
				{
					if ((currentIndexIndex & 1) == 0) //even
					{
						Swap(ref currentValues[0], ref currentValues[currentIndexIndex]);
					}
					else
					{
						Swap(ref currentValues[indexes[currentIndexIndex]], ref currentValues[currentIndexIndex]);
					}
					//Swap has occurred ending the for-loop. Simulate the increment of the for-loop counter
					indexes[currentIndexIndex] += 1;
					//Simulate recursive call reaching the base case by bringing the pointer to the base case analog in the array
					currentIndexIndex = 0;
				}
				else
				{
					//Calling generate(i+1, A) has ended as the for-loop terminated. Reset the state and simulate popping the stack by incrementing the pointer.
					indexes[currentIndexIndex] = 0;
					currentIndexIndex += 1;
				}

				return true;
			}

			public void Reset()
			{
				Array.Copy(values, currentValues, currentValues.Length);
				indexes = new int[values.Length];
				currentIndexIndex = 0;
			}

			private void Swap(ref T a, ref T b)
			{
				T temp = a;
				a = b;
				b = temp;
			}
		}
	}
}
