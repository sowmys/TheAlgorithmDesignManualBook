using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmDesignManual.Chapter04.Sorting {
	public class BinarySearch<T> where T:IComparable<T> {
		public static int Search(T[] s, T key) {
			return Search(s, key, 0, s.Length - 1);
		}
		private static int Search(T[] s, T key, int low, int high) {
			if (low > high) return (-1); /* key not found */
			int middle = (low + high) / 2; /* index of middle element */
			int compareResult = s[middle].CompareTo(key);
			if (compareResult == 0) 
				return (middle);
			if (compareResult > 0) 
				return (Search(s, key, low, middle - 1)); 
			else 
				return (Search(s, key, middle + 1, high));
		}
	}
}
