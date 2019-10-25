using AlgorithmDesignManual.Chapter03.BinarySearchTree;
using System;

namespace AlgorithmDesignManual.Chapter03.BinarySearchTree
{

	class TestBinarySearchTree
	{
		/*
		Tests tree routines Insert, Find and Delete. The output for this is as follows
		 Input Tree: 2 L->(1) R->(7 L->(4 L->(3) R->(6 L->(5))) R->(8))
		 Find(7):7 L->(4 L->(3) R->(6 L->(5))) R->(8)
		 Delete(3): 2 L->(1) R->(7 L->(4 R->(6 L->(5))) R->(8))
		 Delete(6): 2 L->(1) R->(7 L->(4 L->(3) R->(5)) R->(8))
		 Delete(4): 2 L->(1) R->(7 L->(5 L->(3) R->(6)) R->(8))
		 Delete(2): 3 L->(1) R->(7 L->(4 R->(6 L->(5))) R->(8))
		 */

		public static void RunTests()
		{
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
	}
}
