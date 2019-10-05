using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ch3_BinarySearchTree {
	internal class Program {
		/*
		Tests tree routines Insert, Find and Delete. The output for this is as follows
			Input Tree: 2 L->(1) R->(7 L->(4 L->(3) R->(6 L->(5))) R->(8))
			Find(7):7 L->(4 L->(3) R->(6 L->(5))) R->(8)
			Delete(3): 2 L->(1) R->(7 L->(4 R->(6 L->(5))) R->(8))
			Delete(6): 2 L->(1) R->(7 L->(4 L->(3) R->(5)) R->(8))
			Delete(4): 2 L->(1) R->(7 L->(5 L->(3) R->(6)) R->(8))
			Delete(2): 3 L->(1) R->(7 L->(4 R->(6 L->(5))) R->(8))
		 */
		private static void Main(string[] args) {
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

	public class BinarySearchTree<T> where T : IComparable<T> {
		public BinarySearchTree(params T[] elements) {
			foreach (T element in elements) {
				Insert(element);
			}
		}
		public TreeNode<T> Root { get; private set; }

		public bool Insert(T element) {
			if (Root == null) {
				Root = new TreeNode<T> { Value = element };
				return true;
			}

			TreeNode<T> parentNode = FindNode(element, out int cmpRes);
			if (cmpRes == 0) {
				return false;
			}

			TreeNode<T> insertedNode = new TreeNode<T> { Value = element, Parent = parentNode, Depth = parentNode.Depth + 1 };
			if (cmpRes > 0) {
				parentNode.Left = insertedNode;
			}
			else {
				parentNode.Right = insertedNode;
			}

			return true;
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
				TreeNode<T> successor = nodeToDelete.Right;
				while (successor.Left != null) {
					successor = successor.Left;
				}
				nodeToDelete.Value = successor.Value;
				Delete(successor);
			}
		}

		/*
		Replace the appropriate chid or the root
		 */
		private void ReplaceChild(TreeNode<T> parent, TreeNode<T> oldChild, TreeNode<T> newChild) {
			if (parent == null) {// root node
				Root = newChild;
			}
			if (ReferenceEquals(parent.Left, oldChild)) {
				parent.Left = newChild;
			}
			else if (ReferenceEquals(parent.Right, oldChild)) {
				parent.Right = newChild;
			}
			else {
				throw new KeyNotFoundException($"Node {oldChild.Value} is not a child of {parent.Value}");
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

	public class DepthFirstPreorderTraverser<T> : IEnumerable<TreeNode<T>> {
		private readonly TreeNode<T> root;

		public DepthFirstPreorderTraverser(TreeNode<T> root) {
			this.root = root;
		}

		public IEnumerator<TreeNode<T>> GetEnumerator() {
			return new DepthFirstPreorderTraverserImpl(root);
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return new DepthFirstPreorderTraverserImpl(root);
		}

		private class DepthFirstPreorderTraverserImpl : IEnumerator<TreeNode<T>> {
			private readonly TreeNode<T> root;
			private TreeNode<T> current;
			private bool traversed;

			public DepthFirstPreorderTraverserImpl(TreeNode<T> root) {
				this.root = root;
			}

			public TreeNode<T> Current => current;

			object IEnumerator.Current => current;

			public void Dispose() { }

			public bool MoveNext() {
				if (traversed) {
					return false;
				}
				if (current == null) {
					current = root;
					return true;
				}

				if (current.Left != null) {
					current = current.Left;
					return true;
				}
				if (current.Right != null) {
					current = current.Right;
					return true;
				}

				TreeNode<T> parent = current.Parent;
				while (parent != null) {
					if (parent.Right != null && !ReferenceEquals(parent.Right, current)) {
						current = parent.Right;
						return true;
					}

					current = parent;
					parent = parent.Parent;
				}
				traversed = true;
				return false;
			}

			public void Reset() {
				current = null;
				traversed = false;
			}
		}
	}
	public class TreeNode<T> {
		public T Value { get; set; }
		public int Depth { get; set; } // Useful for printing and debugging
		public TreeNode<T> Parent { get; set; }
		public TreeNode<T> Left { get; set; }
		public TreeNode<T> Right { get; set; }

		public override string ToString() {
			StringWriter stringWriter = new StringWriter();
			OnelinePrint(stringWriter);
			return stringWriter.ToString();
		}

		public void OnelinePrint(StringWriter stringWriter) {
			stringWriter.Write(Value);
			if (Left != null) {
				stringWriter.Write(" L-(" + Left + ")");
			}

			if (Right != null) {
				stringWriter.Write(" R-(" + Right + ")");
			}
		}

		public void TreePrint(string indent, StringWriter stringWriter) {
			stringWriter.Write(indent);
			if (Parent != null) {
				if (ReferenceEquals(Parent.Right, this)) {
					stringWriter.Write("R-");
					indent += "  ";
				}
				else {
					stringWriter.Write("L-");
					indent += "| ";
				}
			}
			stringWriter.WriteLine(Value);
			if (Left != null) {
				Left.TreePrint(indent, stringWriter);
			}
			if (Right != null) {
				Right.TreePrint(indent, stringWriter);
			}
		}
	}
}
