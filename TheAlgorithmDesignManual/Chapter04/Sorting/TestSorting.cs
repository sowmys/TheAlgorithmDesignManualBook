using System;

namespace AlgorithmDesignManual.Chapter04.Sorting {
	public class TestSorting {
		/*
			Testing InsertionSort`1
			Before: 4,8,3,12,9,1,32,2
			After: 1,2,3,4,8,9,12,32
			Testing MergeSort`1
			Before: 4,8,3,12,9,1,32,2
			After: 1,2,3,4,8,9,12,32
			Testing Quicksort`1
			Before: 4,8,3,12,9,1,32,2
			After: 1,2,3,4,8,9,12,32
			Searched ....Index of value 3 is 2

		 */
		public static void RunTests() {
			int[] s = new int[] { 4, 8, 3, 12, 9, 1, 32, 2 };
			Console.WriteLine("---------------");
			Console.WriteLine("Testing Sorting");
			Console.WriteLine("---------------");

			TestSortMethod((int[])s.Clone(), InsertionSort<int>.Sort);
			TestSortMethod((int[])s.Clone(), MergeSort<int>.Sort);
			TestSortMethod(s, Quicksort<int>.Sort);
			Console.WriteLine("Searched ....Index of value {0} is {1}", 3, BinarySearch<int>.Search(s, 3));
			Console.WriteLine("Searched ....Index of value {0} is {1}", 5, BinarySearch<int>.Search(s, 5));
		}

		private static void TestSortMethod(int[] s, Action<int[]> sortMethod) {
			Console.WriteLine("Testing {0}", sortMethod.Method.DeclaringType.Name);
			Console.WriteLine("Before: {0}", string.Join(',', s));
			sortMethod(s);
			Console.WriteLine("After: {0}", string.Join(',', s));
		}
	}
}
