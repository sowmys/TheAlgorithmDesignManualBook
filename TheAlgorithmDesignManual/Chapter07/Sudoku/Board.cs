using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace AlgorithmDesignManual.Chapter07.Sudoku {
	class Board {		
		const int SubDim = 3;
		const int Dim = SubDim * SubDim;
		private readonly int[] cellValues = new int[Dim * Dim];
		private readonly HashSet<int>[] rowValues = new HashSet<int>[Dim];
		private readonly HashSet<int>[] colValues = new HashSet<int>[Dim];
		private readonly HashSet<int>[] sectionValues = new HashSet<int>[Dim];
		private int filledCells = 0;

		public Board() {
			for (int i = 0; i < Dim; i++) {
				rowValues[i] = new HashSet<int>();
				colValues[i] = new HashSet<int>();
				sectionValues[i] = new HashSet<int>();
			}
		}

		public bool IsDone => Dim * Dim == filledCells;
		public void SetInitialValues(int[] values) {
			for (int i = 0; i < values.Length; i++) {
				int value = values[i];
				if (value > 0) {
					int row = i / Dim;
					int col = i % Dim;
					SetValue(row, col, value);
				}
			}
		}

		public void SetValue(int row, int col, int value) {
			if (value < 1 || value > 9) throw new InvalidOperationException(string.Format("Value is {0} but must be between 1 and 9", value));
			int valueIndex = GetIndex(row, col);
			int oldValue = cellValues[valueIndex];
			if (oldValue != 0) throw new InvalidOperationException(string.Format("{0},{1} already has value {2}", row, col, oldValue));
			cellValues[valueIndex] = value;
			rowValues[row].Add(value);
			colValues[col].Add(value);
			sectionValues[GetSectionIndex(row, col)].Add(value);
			filledCells++;
		}

		public void UnsetValue(int row, int col) {
			int lastMove = GetIndex(row, col);
			int valueToRemove = cellValues[lastMove];
			cellValues[lastMove] = 0;
			rowValues[row].Remove(valueToRemove);
			colValues[col].Remove(valueToRemove);
			sectionValues[GetSectionIndex(row, col)].Remove(valueToRemove);
			filledCells--;
		}

		public int GetValue(int row, int col) {
			int valueIndex = GetIndex(row, col);
			return cellValues[valueIndex];
		}

		public void GetCellWithLeastPossibleValues(out int[] outPossibleValues, out int outRow, out int outCol) {
			outRow = outCol = -1;
			outPossibleValues = null;
			for (int row = 0; row < Dim; row++) {
				for (int col = 0; col < Dim; col++) {
					if (cellValues[GetIndex(row, col)] > 0) continue;
					var newPossibleValues = GetPossibleValues(row, col);
					if (outPossibleValues == null || outPossibleValues.Length > newPossibleValues.Length) {
						outPossibleValues = newPossibleValues;
						outRow = row;
						outCol = col;
					}
				}
			}
		}

		private int[] GetPossibleValues(int row, int col) {
			int valueIndex = GetIndex(row, col);
			int currentValue = cellValues[valueIndex];
			if (currentValue != 0) return new int[] { currentValue };
			else {
				HashSet<int> allValues = new HashSet<int>(Enumerable.Range(1, 9));
				return allValues.Except(rowValues[row])
								.Except(colValues[col])
								.Except(sectionValues[GetSectionIndex(row, col)]).ToArray();
			}
		}

		public override string ToString() {
			StringWriter stringWriter = new StringWriter();
			for (int i = 0; i < cellValues.Length; i++) {
				stringWriter.Write("{0} ", cellValues[i]);
				if ((i % Dim) == (Dim-1)) {
					stringWriter.WriteLine();
				}
			}
			return stringWriter.ToString();
		}

		private static int GetSectionIndex(int row, int col) {
			return row / SubDim * SubDim + col / SubDim;
		}
		private static int GetIndex(int row, int col) {
			return row * Dim + col;
		}

	}
}
