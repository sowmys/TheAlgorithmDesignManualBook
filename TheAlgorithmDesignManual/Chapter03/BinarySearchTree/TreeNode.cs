using System.IO;
using System.Linq;

namespace AlgorithmDesignManual.Chapter03.BinarySearchTree {
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