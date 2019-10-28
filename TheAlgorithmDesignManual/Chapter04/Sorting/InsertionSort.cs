using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmDesignManual.Chapter04.Sorting {
	class InsertionSort<T> where T:IComparable<T>{
		public static void Sort(T[] s) {
			for (int i = 1; i < s.Length; i++) { 
				int j = i; 
				while ((j > 0) && (s[j].CompareTo(s[j - 1]) < 0)) {
					T t = s[j];
					s[j] = s[j - 1];
					s[j - 1] = t;
					j -= 1; 
				} 
			}
		}
	}
}
