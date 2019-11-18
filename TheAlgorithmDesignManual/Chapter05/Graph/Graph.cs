using AlgorithmDesignManual.Chapter05.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmDesignManual.Chapter05.Graph {
	/*
	 Adjacency List implementation below differs from Skiena's design in two ways
		1. Edge Node's are labeled with string instead of an integer
		2. The nodes are contained in a Dictionary (Map) instead of an array
	 */
	public class Graph {
		private readonly Dictionary<string, EdgeNode> edges = new Dictionary<string, EdgeNode>();
		private Dictionary<string, int> degrees = new Dictionary<string, int>();
		private int edgeCount;
		private bool directed;

		public Graph(bool directed) {
			this.edgeCount = 0;
			this.directed = directed;
		}

		public int VertexCount => edges.Count;

		public IReadOnlyDictionary<string, EdgeNode> Edges => edges;

		public void Add(string x, string y, int weight) {
			Add(x, y, weight, this.directed);
		}

		private void Add(string x, string y, int weight, bool directed) {
			EdgeNode oldEdgeNode;
			edges.TryGetValue(x, out oldEdgeNode);
			EdgeNode newEdgeNode = new EdgeNode(y, weight, oldEdgeNode);
			edges[x] = newEdgeNode;
			if (oldEdgeNode != null) {
				degrees[x] = degrees[x] + 1;
			} else {
				degrees.Add(x, 1);
			}
			if (directed == false) {
				Add(y, x, weight, directed: true);
			} else {
				edgeCount++;
			}
		}

		public void PrintGraph() {
			foreach(var pair in edges) {
				Console.WriteLine("{0}", pair.Key);
				EdgeNode edgeNode = pair.Value;
				while (edgeNode != null) {
					Console.WriteLine(" {0}-{1}", edgeNode.Weight, edgeNode.Target);
					edgeNode = edgeNode.Next;
				}
				Console.WriteLine();
			}
		}
	}
}
