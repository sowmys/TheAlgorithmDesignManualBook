using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmDesignManual.Chapter07.Backtracking {
	/*
	 The backtracking orchestrator the recurses to wards solution step by step.
	 It uses interface BacktrackingProblem to abstract specific backtracking problem.
	 Some examples of backtracking problem are Subset Generation, Permutation Generation, Sudoku solver etc.
	 */ 
	class BacktrackingDFS<T> {
		public BacktrackingDFS(IBacktrackingProblem<T> backtrackingProblem) {
			BacktrackingProblem = backtrackingProblem;
		}

		public IBacktrackingProblem<T> BacktrackingProblem { get; private set; }

		public void Backtrack(List<T> solution) {
			//Console.Write("solution[{0}]: {1}", solution.Count, solution.Count/*, string.Join(",", solution)*/);
			if (BacktrackingProblem.IsASolution(solution)) {
				//Console.WriteLine("<== IsASolution");
				BacktrackingProblem.ProcessSolution(solution);
				return;
			}
			var possibleSteps = BacktrackingProblem.ConstuctCandidates(solution).ToArray();
			//Console.WriteLine("PossibleValuesCount: {0}", possibleSteps.Length);
			foreach (var possibleStep in possibleSteps) {
				solution.Add(possibleStep);
				BacktrackingProblem.MakeMove(solution);
				Backtrack(solution);
				BacktrackingProblem.UnmakeMove(solution);
				//Note: In the book an index 'k' is used to mark the end of solution. Here items added and removed from tail.
				solution.RemoveAt(solution.Count - 1);
				if (BacktrackingProblem.Finished) {
					return;
				}
			}
		}
	}
}
