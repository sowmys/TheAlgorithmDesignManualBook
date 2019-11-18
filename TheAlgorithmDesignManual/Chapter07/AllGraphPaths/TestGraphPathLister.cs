using AlgorithmDesignManual.Chapter05.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmDesignManual.Chapter07.AllGraphPaths {
	public class TestGraphPathLister {
		/**
		solution: cat
		solution: cat,bat
		solution: cat,bat,bag
		solution: cat,bat,bag,big
		solution: cat,bat,bag,big,dig
		solution: cat,bat,bag,big,dig,dog<== IsASolution
		cat-bat-bag-big-dig-dog
		solution: cat,cot
		solution: cat,cot,cog
		solution: cat,cot,cog,dog<== IsASolution
		cat-cot-cog-dog
		solution: cat,cot,con
		solution: cat,cot,dot
		*/
		public static void RunTests() {
			Graph graph = new Graph(directed: false);
			graph.Add("cat", "cot", 1);
			graph.Add("cat", "bat", 1);
			graph.Add("cot", "dot", 1);
			graph.Add("bat", "bag", 1);
			graph.Add("cot", "con", 1);
//			graph.Add("dot", "dog", 1);
			graph.Add("cot", "cog", 1);
			graph.Add("cog", "dog", 1);
			graph.Add("bag", "big", 1);
			graph.Add("big", "dig", 1);
			graph.Add("dig", "dog", 1);

			GraphPathLister graphPathLister = new GraphPathLister(graph, "cat", "dog");
			graphPathLister.GeneratePaths();
		}
	}
}
