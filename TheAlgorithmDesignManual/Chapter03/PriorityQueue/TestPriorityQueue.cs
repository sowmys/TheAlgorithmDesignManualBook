using System;

namespace AlgorithmDesignManual.Chapter03.PriorityQueue {
	/*
	Tests the priority queue by inserting elements to the queue and then dequeing them in the order of 
	the priority (Low number is high priority).
    */
	public class TestPriorityQueue {
		/*
			Initial Tree:16 L-(8 L-(4)) R-(32 L-(24 L-(23 L-(22 L-(21 L-(20 L-(19 L-(18 L-(17)))))))))
			Deque:4 Tree:16 L-(8) R-(32 L-(24 L-(23 L-(22 L-(21 L-(20 L-(19 L-(18 L-(17)))))))))
			Deque:8 Tree:16 R-(32 L-(24 L-(23 L-(22 L-(21 L-(20 L-(19 L-(18 L-(17)))))))))
			Deque:16 Tree:32 L-(24 L-(23 L-(22 L-(21 L-(20 L-(19 L-(18 L-(17))))))))
			Deque:17 Tree:32 L-(24 L-(23 L-(22 L-(21 L-(20 L-(19 L-(18)))))))
			Deque:18 Tree:32 L-(24 L-(23 L-(22 L-(21 L-(20 L-(19))))))
			Deque:19 Tree:32 L-(24 L-(23 L-(22 L-(21 L-(20)))))
			Deque:20 Tree:32 L-(24 L-(23 L-(22 L-(21))))
			Deque:21 Tree:32 L-(24 L-(23 L-(22)))
			Deque:22 Tree:32 L-(24 L-(23))
			Deque:23 Tree:32 L-(24)
			Deque:24 Tree:32
			Deque:32 Tree:
		 */
		public static void RunTests() {
			var pq = new PriorityQueue<int>();
			var items = new int[] { 16, 32, 8, 4, 24, 23, 22, 21, 20, 19, 18, 17 };
			foreach (var item in items) {
				pq.Enqueue(item);
			}
			Console.WriteLine("----------------------------------------------------");
			Console.WriteLine("Testing Priority Queue based on Binary Search Tree");
			Console.WriteLine("----------------------------------------------------");
			Console.WriteLine("Initial Tree: {0}", pq);
			while (pq.Count > 0) {
				var dequed = pq.Dequeue();
				Console.WriteLine("Deque:{0} Tree:{1}", dequed, pq);
			}
		}
	}
}
