using AlgorithmDesignManual.Chapter03.BinarySearchTree;
using System;

namespace AlgorithmDesignManual.Chapter03.PriorityQueue {
	/*
	 Priority Queue implemented using Binary Search Tree. For efficient peeking of the min value, it is cached
	 in minValueNode.
	 */
	public class PriorityQueue<T> where T:IComparable<T>{
		private TreeNode<T> minValueNode;
		private readonly BinarySearchTree<T> tree;

		public PriorityQueue() {
			tree = new BinarySearchTree<T>();
		}
		/*
		Inserting an element into the queue is inserting a node in Binary Search Tree. The complexity is O(log(n))
		*/
		public void Enqueue(T t) {
			TreeNode<T> insertedNode = tree.Insert(t);
			if (minValueNode == null) {
				minValueNode = insertedNode;
			}
			else if (t.CompareTo(minValueNode.Value) < 0) {
				minValueNode = insertedNode;
			}
		}

		/*
		 Peeking the min node is O(1)
		 */
		public T PeekMin() {
			return minValueNode.Value;
		}

		public int Count => tree.Count;

		/*
		 All of dequeuing cost is inside tree.GetSuccessor. Its worst case can be
		 either O(n) or O(log(n)) depending on if the tree is balanced or not
		 */
		public T Dequeue() {
			if (tree.Count == 0) {
				return default(T);
			}
			TreeNode<T> nextMinValueNode = tree.GetSuccessor(minValueNode);
			tree.Delete(minValueNode);
			T minValue = minValueNode.Value;
			minValueNode = nextMinValueNode;
			return minValue;
		}

		public override string ToString() {
			return tree.Root?.ToString();
		}
	}
}
