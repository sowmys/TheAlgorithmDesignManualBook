using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AlgorithmDesignManual.Chapter04.Sorting {
	public class MergeSort<T> where T:IComparable<T> {
		public static void Sort(T[] s) {
			Sort(s, 0, s.Length-1);
		}

		private static void Sort(T[] s, int low, int high) {
			if (low >= high) {
				return;
			}

			int middle = (low + high) / 2;
			Sort(s, low, middle);
			Sort(s, middle + 1, high);
			Merge(s, low, middle, high);
		}

		private static void Merge(T[] s, int low, int middle, int high) {
			Queue<T> buffer1 = new Queue<T>(), buffer2 = new Queue<T>(); /* buffers to hold elements for merging */
			int i;
			for (i = low; i <= middle; i++) {
				buffer1.Enqueue(s[i]);
			}
			for (i = middle + 1; i <= high; i++) {
				buffer2.Enqueue(s[i]);
			}
			i = low;
			while (buffer1.Count != 0 && buffer2.Count != 0) {
				s[i++] = buffer1.Peek().CompareTo(buffer2.Peek()) < 0 ? buffer1.Dequeue() : buffer2.Dequeue();
			}
			while (buffer1.Count > 0) {
				s[i++] = buffer1.Dequeue();
			}
			while (buffer2.Count > 0) {
				s[i++] = buffer2.Dequeue();
			}
		}
	}
}
