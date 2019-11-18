using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmDesignManual.Chapter07.Permutation {
	public class TestPermutationGenerator {
		/**
		solution: 0
		solution: 0,1
		solution: 0,1,2<== IsASolution
		{ c a t}
		solution: 0,2
		solution: 0,2,1<== IsASolution
		{ c t a}
		solution: 1
		solution: 1,0
		solution: 1,0,2<== IsASolution
		{ a c t}
		solution: 1,2
		solution: 1,2,0<== IsASolution
		{ a t c}
		solution: 2
		solution: 2,0
		solution: 2,0,1<== IsASolution
		{ t c a}
		solution: 2,1
		solution: 2,1,0<== IsASolution
		{ t a c}
		*/
		public static void RunTests() {
			PermutationGenerator permutationGenerator = new PermutationGenerator("cat");
			permutationGenerator.GeneratePermutations();
		}
	}
}
