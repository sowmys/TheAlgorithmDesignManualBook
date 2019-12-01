namespace AlgorithmDesignManual.Chapter07.Sudoku {
	public class Move {
		public int Row { get; set; }
		public int Col { get; set; }
		public int Value { get; set; }

		public override string ToString() {
			return $"({Row}, {Col}, {Value})";
		}
	}
}
