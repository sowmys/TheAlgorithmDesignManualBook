using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmDesignManual.Chapter07.Subset {
	public class TestSubsetGenerator {
		/**
			solution:
			solution: True
			solution: True,True
			solution: True,True,True<== IsASolution
			{ c a t}
			solution: True,True,False<== IsASolution
			{ c a}
			solution: True,False
			solution: True,False,True<== IsASolution
			{ c t}
			solution: True,False,False<== IsASolution
			{ c}
			solution: False
			solution: False,True
			solution: False,True,True<== IsASolution
			{ a t}
			solution: False,True,False<== IsASolution
			{ a}
			solution: False,False
			solution: False,False,True<== IsASolution
			{ t}
			solution: False,False,False<== IsASolution
			{}		 */
		public static void RunTests() {
			SubsetGenerator subsetGenerator = new SubsetGenerator("cat");
			subsetGenerator.GenerateSubsets();
		}
	}
}
