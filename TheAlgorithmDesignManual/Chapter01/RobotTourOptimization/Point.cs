namespace AlgorithmDesignManaual.Chapter01.RobotTourOptimization
{
	public class Point
	{
		public Point(int x, int y) { X = x; Y = y; }
		public int X { get; private set; }
		public int Y { get; private set; }

		public int GetDistance(Point p2)
		{
			if (p2 == null) return int.MaxValue; //Max distance for non existent points
			int dx = p2.X - X, dy = p2.Y - Y;
			return dx * dx + dy * dy; //Actually, square of the distance
		}

		public override string ToString()
		{
			return $"({X}, {Y})";
		}
	}
}
