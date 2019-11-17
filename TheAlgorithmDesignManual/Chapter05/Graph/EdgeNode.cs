using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmDesignManual.Chapter05.Graph {
	public class EdgeNode {
		public EdgeNode(string target, int weight, EdgeNode next) {
			Target = target;
			Weight = weight;
			Next = next;
		}
		public string Target { get; private set; } // This was denoted as y 
		public int Weight { get; private set; }
		public EdgeNode Next { get; private set; }
	}
}
