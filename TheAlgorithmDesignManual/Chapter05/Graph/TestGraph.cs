using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmDesignManual.Chapter05.Graph {
	public class TestGraph {
		/*
		  Vertex Count: 6
			Seattle
			 2200-Newyork
			 1800-Chicago
			 150-Vancouver
			 180-Portland

			Portland
			 180-Seattle

			Vancouver
			 150-Seattle

			Newyork
			 2200-Seattle
			 700-Chicago
			 240-WashingtonDC

			WashingtonDC
			 240-Newyork

			Chicago
			 1800-Seattle
			 700-Newyork
		 */
		public static void RunTests() {
			Graph graph = new Graph(directed: false);
			graph.Add("Seattle", "Portland", 180);
			graph.Add("Seattle", "Vancouver", 150);
			graph.Add("Newyork", "WashingtonDC", 240);
			graph.Add("Chicago", "Newyork", 700);
			graph.Add("Chicago", "Seattle", 1800);
			graph.Add("Seattle", "Newyork", 2200);
			Console.WriteLine("Vertex Count: {0}", graph.VertexCount);
			graph.PrintGraph();
		}
	}
}
