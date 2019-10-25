using System;

namespace AlgorithmDesignManual.Chapter03.PriorityQueue
{
	public class TestPriorityQueue
	{
		public static void RunTests()
		{
			var pq = new PriorityQueue<int>();
			var items = new int[] { 16, 32, 8, 4, 24, 23, 22, 21, 20, 19, 18, 17 };
			foreach (var item in items)
			{
				pq.Enqueue(item);
			}
			Console.WriteLine(pq);
			while (pq.Count > 0)
			{
				var dequed = pq.Dequeue();
				Console.WriteLine("Deque:{0} Tree:{1}", dequed, pq);
			}
		}
	}
}
