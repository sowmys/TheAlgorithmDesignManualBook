using AlgorithmDesignManual.Chapter03.BinarySearchTree;
using System;

namespace AlgorithmDesignManual.Chapter03.PriorityQueue {
	public class PriorityQueue<T> where T:IComparable<T>{
		private TreeNode<T> minValueNode;
		private BinarySearchTree<T> tree;

		public PriorityQueue() {
			tree = new BinarySearchTree<T>();
		}

		public void Enqueue(T t) {
			var insertedNode = tree.Insert(t);
			if (minValueNode == null) {
				minValueNode = insertedNode;
			}
			else if (t.CompareTo(minValueNode.Value) < 0) {
				minValueNode = insertedNode;
			}
		}

		public T PeekMin() {
			return minValueNode.Value;
		}

		public int Count => tree.Count;

		public T Dequeue() {
			if (tree.Count == 0) {
				return default(T);
			}
			var nextMinValueNode = tree.GetSuccessor(minValueNode);
			tree.Delete(minValueNode);
			var minValue = minValueNode.Value;
			minValueNode = nextMinValueNode;
			return minValue;
		}

		public override string ToString() {
			return tree.ToString();
		}
	}
}
