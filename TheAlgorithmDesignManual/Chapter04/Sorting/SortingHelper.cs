using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmDesignManual.Chapter04.Sorting {
	class SortingHelper<T> {
		public static void Swap(ref T a, ref T b) {
			T t = a;
			a = b;
			b = t;
		}
	}
}
