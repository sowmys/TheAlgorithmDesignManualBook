using System.Collections.Generic;

namespace AlgorithmDesignManual.Chapter07.Backtracking {

	/*
	 The application specific abstractions for using with Backtracking controller
    */
	public interface IBacktrackingProblem<T> {
		bool Finished { get; }

		bool IsASolution(List<T> solution);
		IEnumerable<T> ConstuctCandidates(List<T> solution);
		void ProcessSolution(List<T> solution);
		void MakeMove(List<T> solution);
		void UnmakeMove(List<T> solution);
	}
}
