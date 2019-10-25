using System;
using System.Collections.Generic;
using System.IO;

namespace AlgorithmDesignManual.Chapter03.BinarySearchTree {
	public class BinarySearchTree<T> where T : IComparable<T> {
		public BinarySearchTree(params T[] elements) {
			foreach (T element in elements) {
				Insert(element);
			}
		}
		public TreeNode<T> Root { get; private set; }
		public int Count { get; private set; }

		public TreeNode<T> Insert(T element) {
			if (Root == null) {
				Root = new TreeNode<T> { Value = element };
				Count = Count + 1;
				return Root;
			}

			TreeNode<T> parentNode = FindNode(element, out int cmpRes);
			if (cmpRes == 0) {
				return parentNode;
			}

			TreeNode<T> insertedNode = new TreeNode<T> { Value = element, Parent = parentNode, Depth = parentNode.Depth + 1 };
			if (cmpRes > 0) {
				parentNode.Left = insertedNode;
			}
			else {
				parentNode.Right = insertedNode;
			}

			Count = Count + 1;
			return insertedNode;
		}

		public TreeNode<T> Find(T element) {
			TreeNode<T> node = FindNode(element, out int cmpRes);
			if (cmpRes == 0) {
				return node;
			}

			throw new KeyNotFoundException($"Element {element} not found in the tree");
		}

		private TreeNode<T> FindNode(T element, out int cmpRes) {
			cmpRes = 0;
			if (Root == null) {
				return null;
			}
			TreeNode<T> node = Root;
			while (true) {
				cmpRes = node.Value.CompareTo(element);
				if (cmpRes == 0) {
					return node;
				}
				if (cmpRes > 0) {
					if (node.Left == null) {
						return node;
					}
					node = node.Left;
				}
				else {
					if (node.Right == null) {
						return node;
					}
					node = node.Right;
				}
			}
		}

		/*
		There are three cases. The deleted node
		 has no children: It is deleted by clearing its reference from its parent. 
		 Has one child: The only child replaces the deleted node. 
		 Has two children: Replace the value of the deleted node with the value of the immediate successor in sorted order and delete the immediate successor.
		*/
		public void Delete(TreeNode<T> nodeToDelete) {
			TreeNode<T> parent = nodeToDelete.Parent;
			if (nodeToDelete.Left == null && nodeToDelete.Right == null) {//No Child
				ReplaceChild(parent, nodeToDelete, null);
			}
			else if (nodeToDelete.Left == null) {//Only right child, replace node to delete with the only child
				ReplaceChild(parent, nodeToDelete, nodeToDelete.Right);
			}
			else if (nodeToDelete.Right == null) {//Only left child, replace node to delete with the only child
				ReplaceChild(parent, nodeToDelete, nodeToDelete.Left);
			}
			else {//Both children present, set the Value of nodeToDelete with the immediate successor and delete that node recursively
				TreeNode<T> successor = GetSuccessor(nodeToDelete);
				nodeToDelete.Value = successor.Value;
				Delete(successor);
			}

			Count = Count - 1;
		}

		public TreeNode<T> GetSuccessor(TreeNode<T> node) {
			TreeNode<T> successor = node.Right;
			if (successor == null) {
				return node.Parent;
			}

			while (successor.Left != null) {
				successor = successor.Left;
			}

			return successor;
		}

		/*
		Replace the appropriate chid or the root
		 */
		private void ReplaceChild(TreeNode<T> parent, TreeNode<T> oldChild, TreeNode<T> newChild) {
			if (parent == null) {// root node
				Root = newChild;
			}
			else if (ReferenceEquals(parent.Left, oldChild)) {
				parent.Left = newChild;
			}
			else if (ReferenceEquals(parent.Right, oldChild)) {
				parent.Right = newChild;
			}
			else {
				throw new KeyNotFoundException($"Node {oldChild.Value} is not a child of {parent.Value}");
			}

			if (newChild != null) {
				newChild.Parent = parent;
			}
		}

		public override string ToString() {
			if (Root == null) {
				return "<null>";
			}
			StringWriter stringWriter = new StringWriter();
			Root.TreePrint(String.Empty, stringWriter);
			return stringWriter.ToString();
		}
	}
}