using Ch3_BinarySearchTree;
using Ch3_PriorityQueue;
using System;
using System.Linq;

internal class Program {
	private static void Main(string[] args) {
		//TestBinarySearchTree();
		TestPriorityQueue();
	}

	/*
	Tests tree routines Insert, Find and Delete. The output for this is as follows
	 Input Tree: 2 L->(1) R->(7 L->(4 L->(3) R->(6 L->(5))) R->(8))
	 Find(7):7 L->(4 L->(3) R->(6 L->(5))) R->(8)
	 Delete(3): 2 L->(1) R->(7 L->(4 R->(6 L->(5))) R->(8))
	 Delete(6): 2 L->(1) R->(7 L->(4 L->(3) R->(5)) R->(8))
	 Delete(4): 2 L->(1) R->(7 L->(5 L->(3) R->(6)) R->(8))
	 Delete(2): 3 L->(1) R->(7 L->(4 R->(6 L->(5))) R->(8))
	 */
	private static void TestBinarySearchTree() {
		int[] treeValues = new int[] { 2, 1, 7, 4, 8, 3, 6, 5 };
		BinarySearchTree<int> binTree = new BinarySearchTree<int>(treeValues);
		Console.WriteLine(binTree);
		Console.WriteLine("Input Tree: {0}", binTree.Root);
		TreeNode<int> node15 = binTree.Find(7);
		Console.WriteLine("Find(7):{0}", node15);
		binTree.Delete(binTree.Find(3));
		Console.WriteLine("Delete(3): {0}", binTree.Root);
		binTree = new BinarySearchTree<int>(treeValues);
		binTree.Delete(binTree.Find(6));
		Console.WriteLine("Delete(6): {0}", binTree.Root);
		binTree = new BinarySearchTree<int>(treeValues);
		binTree.Delete(binTree.Find(4));
		Console.WriteLine("Delete(4): {0}", binTree.Root);
		binTree = new BinarySearchTree<int>(treeValues);
		binTree.Delete(binTree.Find(2));
		Console.WriteLine("Delete(2): {0}", binTree.Root);
	}

	private static void TestPriorityQueue() {
		var pq = new PriorityQueue<int>();
		var items = new int[]{ 16, 32, 8, 4, 24, 23, 22, 21, 20, 19, 18, 17};
		foreach (var item in items) {
			pq.Enqueue(item);
		}
		Console.WriteLine(pq);
		while (pq.Count > 0) {
			var dequed = pq.Dequeue();
			Console.WriteLine("Deque:{0} Tree:{1}", dequed, pq);
		}
	}
}