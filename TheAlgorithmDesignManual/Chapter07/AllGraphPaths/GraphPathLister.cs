using AlgorithmDesignManual.Chapter05.Graph;
using AlgorithmDesignManual.Chapter07.Backtracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmDesignManual.Chapter07.AllGraphPaths {
	class GraphPathLister : IBacktrackingProblem<string> {
		readonly Graph graph;
		readonly string s;
		readonly string t;

		public GraphPathLister(Graph graph, string s, string t) {
			this.graph = graph;
			this.s = s;
			this.t = t;
		}

		public void GeneratePaths() {
			BacktrackingDFS<string> backtracking = new BacktrackingDFS<string>(this);
			backtracking.Backtrack(new List<string>());
		}

		bool IBacktrackingProblem<string>.Finished => false;

		IEnumerable<string> IBacktrackingProblem<string>.ConstuctCandidates(List<string> solution) {
			HashSet<string> inSolution = new HashSet<string>();
			for (int i = 0; i < solution.Count; i++) {
				inSolution.Add(solution[i]);
			}
			if (solution.Count == 0) {
				yield return s;
			} else {
				var last = solution[solution.Count - 1];
				var edge = graph.Edges[last];
				while (edge != null) {
					if (!inSolution.Contains(edge.Target)) {
						yield return edge.Target;
					}
					edge = edge.Next;
				}
			}
		}

		bool IBacktrackingProblem<string>.IsASolution(List<string> solution) {
			return solution.Count > 0 &&  solution[solution.Count - 1] == t;
		}

		void IBacktrackingProblem<string>.MakeMove(List<string> solution) {
		}

		void IBacktrackingProblem<string>.ProcessSolution(List<string> solution) {
			Console.WriteLine(string.Join("-", solution));
		}

		void IBacktrackingProblem<string>.UnmakeMove(List<string> solution) {
		}
	}
}
