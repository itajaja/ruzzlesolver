using System;

namespace RuzzleSolver
{ 

	class RuzzleSolver
	{
		public static void Main (string[] args)
		{
			RuzzleSolver solver = new RuzzleSolver();
			Console.WriteLine ("Hello World!");
		}

		public void initPuzzle(){
			board = new char[3,3]{{'c','a','s'},{'a','a','o'},{'a','a','a'}};
		}
		
	    private char[,] board;
	}
}
