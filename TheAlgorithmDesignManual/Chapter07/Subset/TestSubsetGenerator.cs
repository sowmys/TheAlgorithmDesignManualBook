using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmDesignManual.Chapter07.Subset {
	public class TestSubsetGenerator {
		/**
		{ c a t}
		{ c a}
		{ c t}
		{ c}
		{ a t}
		{ a}
		{ t}
		{}
		 */
		public static void RunTests() {
			SubsetGenerator subsetGenerator = new SubsetGenerator("cat");
			subsetGenerator.GenerateSubsets();
		}
	}
}
