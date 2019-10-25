using System.Collections;
using System.Collections.Generic;

namespace AlgorithmDesignManual.Chapter03.BinarySearchTree {
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
}