using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace RobotTourOptimization
{
	/*
	Chap 1: Robot Tour Optimization
		This implements three methods for touring through all points and return to the starting place.
		1. NearestNeighbor: Hueristic logic that, at each step, the next point is choosen to be the one that is the closest.
		2. Closest Pair: Another hueristic that builds a chain by linking points are that are closest
		3. All Permutation: Exhaustive search method that iterates through every possible permutations of the points to choose one with the least total distance
	 */
	class Program
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
		static void Main(string[] args)
		{
			Point[][] examplePoints = new[] {
				new[] { new Point(0, 0), new Point(10, 0), new Point(2, 2), new Point(1, 0) },
				new[] { new Point(0,0), new Point (-10, 0), new Point(-1, 0), new Point(1, 0), new Point(4, 0) },
				new[] { new Point(0,0), new Point(0, 10), new Point(10, 0), new Point(10, 10), new Point(20, 0), new Point(20, 10) },
			};
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

		public class PermutationGenerator<T> : IEnumerable<T[]>
		{
			private T[] values;
			public PermutationGenerator(T[] values)
			{
				this.values = values;
			}
			public IEnumerator<T[]> GetEnumerator()
			{
				return new PermutationGeneratorImpl(values);
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return new PermutationGeneratorImpl(values);
			}

			private class PermutationGeneratorImpl : IEnumerator<T[]>
			{
				private T[] values;
				private T[] currentValues;
				private int[] indexes;
				private int currentIndexIndex;

				public PermutationGeneratorImpl(T[] values)
				{
					this.values = values;
					this.currentValues = values.ToArray();
					this.indexes = new int[values.Length];
					this.currentIndexIndex = 0;
				}

				public T[] Current => currentValues;

				object IEnumerator.Current => currentValues;

				public void Dispose()
				{

				}

				public bool MoveNext()
				{
					if (currentIndexIndex == values.Length) return false;
					if (indexes[currentIndexIndex] < currentIndexIndex)
					{
						if ((currentIndexIndex & 1) == 0) //even

							Swap(ref currentValues[0], ref currentValues[currentIndexIndex]);

						else
							Swap(ref currentValues[indexes[currentIndexIndex]], ref currentValues[currentIndexIndex]);
						//Swap has occurred ending the for-loop. Simulate the increment of the for-loop counter
						indexes[currentIndexIndex] += 1;
						//Simulate recursive call reaching the base case by bringing the pointer to the base case analog in the array
						currentIndexIndex = 0;
					}
					else
					{
						//Calling generate(i+1, A) has ended as the for-loop terminated. Reset the state and simulate popping the stack by incrementing the pointer.
						indexes[currentIndexIndex] = 0;
						currentIndexIndex += 1;
					}

					return true;
				}

				public void Reset()
				{
					Array.Copy(values, currentValues, currentValues.Length);
					indexes = new int[values.Length];
					currentIndexIndex = 0;
				}

				private void Swap(ref T a, ref T b)
				{
					T temp = a;
					a = b;
					b = temp;
				}
			}
		}
	}
}
