using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmDesignManual.Chapter04.Sorting {
	public class Quicksort<T> where T : IComparable<T> {
		public static void Sort(T[] s) {
			Sort(s, 0, s.Length - 1);
		}

		private static void Sort(T[] s, int low, int high) {
			if ((high - low) > 0) {
				int p = Partition(s, low, high);
				Sort(s, low, p - 1);
				Sort(s, p + 1, high);
			}
		}

		private static int Partition(T[] s, int low, int high) {
			int p = high; /* pivot element index */
			int firstHigh = low;/* divider position for pivot element */
			for (int i = low; i < high; i++) {
				if (s[i].CompareTo(s[p]) < 0) {
					SortingHelper<T>.Swap(ref s[i], ref s[firstHigh]);
					firstHigh++;
				}
			}
			SortingHelper<T>.Swap(ref s[p], ref s[firstHigh]);
			return (firstHigh);
		}
	}
}
