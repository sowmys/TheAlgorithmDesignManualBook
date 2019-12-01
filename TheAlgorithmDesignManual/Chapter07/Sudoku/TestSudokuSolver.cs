using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmDesignManual.Chapter07.Sudoku {
	class TestSudokuSolver {
		public static void RunTests() {
			int[] puzzle1 = {
				8,0,0, 0,1,0, 0,3,0,
				9,2,5, 0,0,0, 8,0,0,
				1,0,7, 9,0,0, 2,0,0,
				0,0,0, 1,0,4, 5,0,0,
				6,0,0, 0,2,0, 0,0,3,
				0,0,4, 3,0,9, 0,0,0,
				0,0,3, 0,0,2, 1,0,4,
				0,0,9, 0,0,0, 3,5,8,
				0,8,0, 0,3,0, 0,0,2
			};
			Solve(puzzle1);
			int[] puzzle2 = {
				0,0,6, 4,0,0, 1,0,0,
				7,0,0, 6,0,0, 3,4,0,
				0,0,0, 0,0,0, 0,0,9,
				0,6,0, 0,2,0, 0,0,5,
				3,1,0, 0,0,0, 0,7,4,
				5,0,0, 0,7,0, 0,9,0,
				9,0,0, 0,0,0, 0,0,0,
				0,4,5, 0,0,8, 0,0,2,
				0,0,1, 0,0,6, 4,0,0
			};
			Solve(puzzle2);
		}

		private static void Solve(int[] puzzle1) {
			Board board = new Board();
			board.SetInitialValues(puzzle1);
			SudokuSolver sudokuSolver = new SudokuSolver();
			sudokuSolver.Solve(board);
		}
	}
}
