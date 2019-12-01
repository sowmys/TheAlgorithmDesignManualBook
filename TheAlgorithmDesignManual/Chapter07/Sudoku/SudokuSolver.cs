using AlgorithmDesignManual.Chapter07.Backtracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmDesignManual.Chapter07.Sudoku {
	class SudokuSolver : IBacktrackingProblem<Move> {
		Board board;

		public void Solve(Board board) {
			this.board = board;
			BacktrackingDFS<Move> backtracking = new BacktrackingDFS<Move>(this);
			backtracking.Backtrack(new List<Move>());
		}

		bool IBacktrackingProblem<Move>.Finished => false;

		IEnumerable<Move> IBacktrackingProblem<Move>.ConstuctCandidates(List<Move> solution) {
			int row, col;
			int[] possibleValues;
			board.GetCellWithLeastPossibleValues(out possibleValues, out row, out col);
			if (possibleValues != null) {
				foreach (var possibleValue in possibleValues) {
					yield return new Move { Row = row, Col = col, Value = possibleValue };
				}
			}
		}

		bool IBacktrackingProblem<Move>.IsASolution(List<Move> solution) {
			return board.IsDone;
		}

		void IBacktrackingProblem<Move>.MakeMove(List<Move> solution) {
			Move lastMove = solution[solution.Count - 1];
			board.SetValue(lastMove.Row, lastMove.Col, lastMove.Value);
		}

		void IBacktrackingProblem<Move>.ProcessSolution(List<Move> solution) {
			Console.WriteLine(board.ToString());
		}

		void IBacktrackingProblem<Move>.UnmakeMove(List<Move> solution) {
			Move lastMove = solution[solution.Count - 1];
			board.UnsetValue(lastMove.Row, lastMove.Col);
		}
	}
}
