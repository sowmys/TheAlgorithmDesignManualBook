using AlgorithmDesignManual.Chapter07.Backtracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmDesignManual.Chapter07.Subset {

	/*
	SubsetGenerator implements IBacktrackingProblem<bool>. The implementation constructs
	arrays of booleans with two values for each item {true, false). If the length of the 
	boolean array is the same as the length of the inputset then the array is
	ready to generate a subset (IsASolution)

	There is no need for MakeMove, UnmakeMove or Finished in this problem.
	*/
	class SubsetGenerator : IBacktrackingProblem<bool> {
		string inputSet;
		public SubsetGenerator(string inputSet) {
			this.inputSet = inputSet;
		}

		public bool Finished => false;

		public void GenerateSubsets() {
			BacktrackingDFS<bool> backtracking = new BacktrackingDFS<bool>(this);
			backtracking.Backtrack(new List<bool>());
		}

		IEnumerable<bool> IBacktrackingProblem<bool>.ConstuctCandidates(List<bool> solution) {
			yield return true;
			yield return false;
		}

		bool IBacktrackingProblem<bool>.IsASolution(List<bool> solution) {
			return solution.Count == inputSet.Length;
		}

		void IBacktrackingProblem<bool>.MakeMove(List<bool> solution) {
			
		}

		void IBacktrackingProblem<bool>.ProcessSolution(List<bool> solution) {
			Console.Write("{");
			for (int i = 0; i < solution.Count; i++) {
				bool includeItemInSubset = (bool)solution[i];
				if (includeItemInSubset) {
					Console.Write(" {0}", inputSet[i]);
				}
			}
			Console.WriteLine("}");
		}

		void IBacktrackingProblem<bool>.UnmakeMove(List<bool> solution) {
		}
	}
}
