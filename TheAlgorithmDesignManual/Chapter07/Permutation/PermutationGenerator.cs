using AlgorithmDesignManual.Chapter07.Backtracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmDesignManual.Chapter07.Permutation {

	/*
	PermutationGenerator implements IBacktrackingProblem<int>. The implementation constructs
	arrays of ints with each item taking values from 0 to n-1 such that the value is not present in any previous position. 
	If the length of the solution array is the same as the length of the inputset then the array is	ready to generate a permutation (IsASolution)

	There is no need for MakeMove, UnmakeMove or Finished in this problem.
	*/
	class PermutationGenerator : IBacktrackingProblem<int> {
		string inputSet;
		public PermutationGenerator(string inputSet) {
			this.inputSet = inputSet;
		}

		public bool Finished => false;

		public void GeneratePermutations() {
			BacktrackingDFS<int> backtracking = new BacktrackingDFS<int>(this);
			backtracking.Backtrack(new List<int>());
		}

		IEnumerable<int> IBacktrackingProblem<int>.ConstuctCandidates(List<int> solution) {
			bool[] indexIncluded = new bool[inputSet.Length]; //initialized to false
			for (int i = 0; i < solution.Count; i++) {
				indexIncluded[solution[i]] = true;
			}
			for (int i = 0; i < inputSet.Length; i++) {
				if (!indexIncluded[i]) {
					yield return i;
				}
			}
		}

		bool IBacktrackingProblem<int>.IsASolution(List<int> solution) {
			return solution.Count == inputSet.Length;
		}

		void IBacktrackingProblem<int>.MakeMove(List<int> solution) {
			
		}

		void IBacktrackingProblem<int>.ProcessSolution(List<int> solution) {
			Console.Write("{");
			for (int i = 0; i < solution.Count; i++) {
				int index= (int)solution[i];
				Console.Write(" {0}", inputSet[index]);
			}
			Console.WriteLine("}");
		}

		void IBacktrackingProblem<int>.UnmakeMove(List<int> solution) {
		}
	}
}
