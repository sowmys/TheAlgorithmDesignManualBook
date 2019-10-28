using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmDesignManual.Chapter04.Heap {
	public class TestHeap {

		/*
		 Initial Heap: 4,8,16,20,18,17,22,32,21,24,19,23
			Min: 4, Heap: 8,18,16,20,23,17,22,32,21,24,19
			Min: 8, Heap: 16,18,17,20,23,19,22,32,21,24
			Min: 16, Heap: 17,18,19,20,23,24,22,32,21
			Min: 17, Heap: 18,20,19,21,23,24,22,32
			Min: 18, Heap: 19,20,24,21,23,32,22
			Min: 19, Heap: 20,21,24,22,23,32
			Min: 20, Heap: 21,22,24,32,23
			Min: 21, Heap: 22,23,24,32
			Min: 22, Heap: 23,32,24
			Min: 23, Heap: 24,32
			Min: 24, Heap: 32
			Min: 32, Heap:
		 */
		public static void RunTests() {
			Heap heap = new Heap(16, 32, 8, 4, 24, 23, 22, 21, 20, 19, 18, 17);
			Console.WriteLine("-------------------------------------");
			Console.WriteLine("Testing Priority Queue based on Heap");
			Console.WriteLine("-------------------------------------");
			Console.WriteLine("Initial Heap: {0}", heap);
			Console.WriteLine("Is 1st element >= 3: {0}", heap.IsKthElementGreaterOrEqualTo(3, 1));
			Console.WriteLine("Is 1st element >= 4: {0}", heap.IsKthElementGreaterOrEqualTo(4, 1));
			Console.WriteLine("Is 1st element >= 5: {0}", heap.IsKthElementGreaterOrEqualTo(5, 1));
			Console.WriteLine("Is 7th element >= 19: {0}", heap.IsKthElementGreaterOrEqualTo(19, 7));
			Console.WriteLine("Is 5th element >= 19: {0}", heap.IsKthElementGreaterOrEqualTo(19, 5));
			while (heap.Count > 0) {
				Console.WriteLine("Min: {0}, Heap: {1}", heap.GetMin(), heap);
			}
		}
	}
}
