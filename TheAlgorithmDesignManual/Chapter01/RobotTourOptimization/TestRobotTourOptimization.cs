using AlgorithmDesignManaual.Chapter01.RobotTourOptimization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmDesignManual.Chapter01.RobotTourOptimization
{
	/*
	Chap 1: Robot Tour Optimization
		This implements three methods for touring through all points and return to the starting place.
		1. NearestNeighbor: Hueristic logic that, at each step, the next point is choosen to be the one that is the closest.
		2. Closest Pair: Another hueristic that builds a chain by linking points are that are closest
		3. All Permutation: Exhaustive search method that iterates through every possible permutations of the points to choose one with the least total distance
	 */
	public class TestRobotTourOptimization
	{
		/*
		Runs each example through all the touring logic. Here is the output
			Testing GetNearestNeighborTour
			Route: (0, 0) -> (1, 0) -> (2, 2) -> (10, 0), Distance: 174
			Route: (0, 0) -> (1, 0) -> (-1, 0) -> (4, 0) -> (-10, 0), Distance: 326
			Route: (0, 0) -> (10, 0) -> (20, 0) -> (20, 10) -> (10, 10) -> (0, 10), Distance: 600
			Testing GetClosestPairTour
			Route: (10, 0) -> (2, 2) -> (1, 0) -> (0, 0), Distance: 174
			Route: (4, 0) -> (1, 0) -> (0, 0) -> (-1, 0) -> (-10, 0), Distance: 288
			Route: (20, 10) -> (20, 0) -> (10, 0) -> (10, 10) -> (0, 10) -> (0, 0), Distance: 1000
			Testing GetAllPermutationsTour
			Route: (0, 0) -> (2, 2) -> (10, 0) -> (1, 0), Distance: 158
			Route: (0, 0) -> (-10, 0) -> (-1, 0) -> (1, 0) -> (4, 0), Distance: 210
			Route: (10, 10) -> (0, 10) -> (0, 0) -> (10, 0) -> (20, 0) -> (20, 10), Distance: 600

		As can be seen only AllPermutations provides optimal path for all cases.
		 */
		public static void RunTests()
		{
			Point[][] examplePoints = new[] {
				new[] { new Point(0, 0), new Point(10, 0), new Point(2, 2), new Point(1, 0) },
				new[] { new Point(0,0), new Point (-10, 0), new Point(-1, 0), new Point(1, 0), new Point(4, 0) },
				new[] { new Point(0,0), new Point(0, 10), new Point(10, 0), new Point(10, 10), new Point(20, 0), new Point(20, 10) },
			};
			Console.WriteLine("-------------------------------");
			Console.WriteLine("Testing Robot Tour Optimization");
			Console.WriteLine("-------------------------------");
			TestTour(examplePoints, GetNearestNeighborTour);
			TestTour(examplePoints, GetClosestPairTour);
			TestTour(examplePoints, GetAllPermutationsTour);
		}

		private static void TestTour(Point[][] examples, Func<IEnumerable<Point>, IEnumerable<Point>> tour)
		{
			Console.WriteLine("Testing " + tour.Method.Name);
			foreach (var example in examples)
			{
				Point[] tourRoute = tour(example).ToArray(); ;
				Console.WriteLine("Route: {0}, Distance: {1}", string.Join(" -> ", (IEnumerable<Point>)tourRoute), GetTotalDistance(tourRoute));
			}
		}

		public static IEnumerable<Point> GetNearestNeighborTour(IEnumerable<Point> inputPoints)
		{
			HashSet<Point> pointsSeen = new HashSet<Point>();
			Point currentPoint = inputPoints.First();

			while (currentPoint != null)
			{
				pointsSeen.Add(currentPoint);
				yield return currentPoint;
				IEnumerable<Point> unseenPoints = inputPoints.Where(inputPoint => !pointsSeen.Contains(inputPoint));
				Point nearestPoint = unseenPoints.Aggregate(seed: (Point)null, func: (p1, p2) => currentPoint.GetDistance(p1) < currentPoint.GetDistance(p2) ? p1 : p2);
				currentPoint = nearestPoint;
			}
		}

		public static IEnumerable<Point> GetClosestPairTour(IEnumerable<Point> inputPoints)
		{
			List<Chain> vertexChains = inputPoints.Select(inputPoint => new Chain(inputPoint)).ToList();
			Chain largestChain = vertexChains[0];
			while (largestChain.Length < vertexChains.Count)
			{
				var chainPairs = vertexChains.SelectMany(source => vertexChains, (source, target) => new { source, target }).Where(pair => !ReferenceEquals(pair.source, pair.target));
				var closestChainPair = chainPairs.Aggregate((chainPair1, chainPair2) => chainPair1.source.GetDistance(chainPair1.target) < chainPair2.source.GetDistance(chainPair2.target) ? chainPair1 : chainPair2);
				Chain sourceChain = closestChainPair.source;
				sourceChain.MoveContentsFrom(closestChainPair.target);
				if (largestChain.Length < sourceChain.Length)
				{
					largestChain = sourceChain;
				}
			}

			return largestChain.Points;
		}

		public static IEnumerable<Point> GetAllPermutationsTour(IEnumerable<Point> inputPoints)
		{
			Point[] inputPointsArray = inputPoints.ToArray();
			IEnumerable<Point[]> permutations = new PermutationGenerator<Point>(inputPointsArray);
			int distanceForChoosenPermutation = int.MaxValue;
			Point[] choosenPermutation = inputPointsArray;
			foreach (Point[] permutation in permutations)
			{
				int distanceForThisPermutation = GetTotalDistance(permutation);
				if (distanceForThisPermutation < distanceForChoosenPermutation)
				{
					distanceForChoosenPermutation = distanceForThisPermutation;
					choosenPermutation = permutation.ToArray();
				}
			}

			return choosenPermutation;
		}

		private static int GetTotalDistance(Point[] permutation)
		{
			int distanceForThisPermutation = 0;
			for (int i = 0; i < permutation.Length; i++)
			{
				Point sourcePoint = permutation[i];
				Point targetPoint = permutation[(i + 1) % permutation.Length];
				distanceForThisPermutation += sourcePoint.GetDistance(targetPoint);
			}

			return distanceForThisPermutation;
		}
	}
}
