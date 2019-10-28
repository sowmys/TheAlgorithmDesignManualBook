using System;
using System.Collections.Generic;

namespace AlgorithmDesignManual.Chapter04.Heap {
	/*	 
		Heap implementation using array based binary tree. As described in the text, the tree has a children of a value
		at index n at 2n and 2n+1. i.e a parent of a value at m will be at int(m/2).

		In order to enable this logic the root has to be at index 1. Its two children will be at indices 2 and 3. 
		The children of 2 will be at indices 4 and 5 while 3 has its children at indices 6 and 7.

		Thus this approach requires the indices to be one-based while .net has its array zero-based. To bridge this the 
		access to .net list is done via helpers GetValue and SetValue which subtracts the one-based index to zero-based index.
	 */
	public class Heap {
		private List<int> values = new List<int>();
		private readonly int InvalidIndex = -1;

		public Heap(params int[] valuesToInsert) {
			foreach (var valueToInsert in valuesToInsert) {
				Add(valueToInsert);
			}
		}

		/*
			Adds a new value and readjusts the heap to keep the parent smaller than all its children.
		 */
		public void Add(int value) {
			values.Add(value);
			BubbleUp(values.Count);
		}

		public int GetMin() {
			int min;
			if (values.Count == 0) {
				throw new InvalidOperationException("No items in heap");
			} else {
				min = GetValue(1);
				SetValue(1, GetValue(values.Count));
				RemoveLast();
				BubbleDown(1);
			}
			return (min);
		}

		/*
			Returns true if the kth smallest value of the heap is greater or equal to the given value 'x' 
		 */
		public bool IsKthElementGreaterOrEqualTo(int x, int k) {
			int remainingCount = HeapCompare(1, k, x);
			return remainingCount > 0;
		}

		/*
			Moves startIndex forward until k items are scanned, a value greater than x is hit or all elements are visited
			Returns remainingCount which will be 0 if all K items are scanned. If a value greater than x is hit or all
			elements are visited then remainingCount is greater than 0
		 */
		private int HeapCompare(int startIndex, int remainingCount, int x) {
			if ((remainingCount <= 0) || (startIndex > Count)) { 
				return remainingCount;
			}

			if (GetValue(startIndex) < x) { 
				remainingCount = HeapCompare(GetFirstChild(startIndex), remainingCount - 1, x); 
				remainingCount = HeapCompare(GetSecondChild(startIndex), remainingCount, x); 
			}
			return (remainingCount);
		}

		public int Count {
			get {
				return values.Count;
			}
		}

		public override string ToString() {
			return string.Join(',', values);
		}

		private void BubbleUp(int index) {
			int parentIndex = GetParent(index);
			if (parentIndex == InvalidIndex) return; /* at root of heap, no parent */
			if (GetValue(parentIndex) > GetValue(index)) {
				Swap(index, parentIndex);
				BubbleUp(parentIndex);
			}
		}

		private int GetParent(int n) {
			return n == 1 ? InvalidIndex : n / 2;
		}

		private int GetFirstChild(int index) {
			return index * 2;
		}

		private int GetSecondChild(int index) {
			return index * 2 + 1;
		}

		private int GetMinChildIndex(int index) {
			int minIndex = index;
			int minValue = GetValue(index);
			int firstChildIndex = index * 2;
			if (firstChildIndex <= values.Count) {
				int currentValue = GetValue(index);
				int firstChildValue = GetValue(firstChildIndex);
				if (firstChildValue < currentValue) {
					minIndex = firstChildIndex;
					minValue = firstChildValue;
				}
				int secondChildIndex = firstChildIndex + 1;
				if (secondChildIndex < values.Count) {
					int secondChildValue = GetValue(secondChildIndex);
					if (secondChildValue < minValue) {
						minIndex = secondChildIndex;
					}
				}
			}

			return minIndex;
		}

		private void BubbleDown(int index) {
			if (Count == 0) return;
			int minChildIndex = GetMinChildIndex(index);
			if (minChildIndex != index) {
				Swap(index, minChildIndex);
				BubbleDown(minChildIndex);
			}
		}

		private void Swap(int a, int b) {
			int t = GetValue(a);
			SetValue(a, GetValue(b));
			SetValue(b, t);
		}

		private int GetValue(int index) {
			return values[index - 1];
		}

		private void SetValue(int index, int value) {
			values[index - 1] = value;
		}

		private void RemoveLast() {
			values.RemoveAt(values.Count - 1);
		}
	}
}
