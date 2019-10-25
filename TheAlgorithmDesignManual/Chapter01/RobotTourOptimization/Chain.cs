using System.Collections.Generic;


namespace AlgorithmDesignManaual.Chapter01.RobotTourOptimization
{
	public class Chain
	{
		private List<Point> points;

		public Chain(Point p)
		{
			points = new List<Point> { p };
		}

		public int Length => points.Count;

		public IEnumerable<Point> Points => points;

		public void MoveContentsFrom(Chain chain)
		{
			points.AddRange(chain.points);
			chain.points.Clear();
		}

		public int GetDistance(Chain target)
		{
			if (this.points.Count == 0 || target.points.Count == 0) return int.MaxValue;

			Point point1 = points[points.Count - 1];
			Point point2 = target.points[0];
			return point1.GetDistance(point2);
		}
	}
}
