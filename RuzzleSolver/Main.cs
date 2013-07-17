using System;
using System.Collections.Generic;
using System.Text;

namespace RuzzleSolver
{

	/// <summary>
	/// Ruzzle solver main class.
	/// </summary>
	class RuzzleSolver
	{

		public static void Main (string[] args)
		{
			RuzzleSolver solver = new RuzzleSolver();
			solver.initPuzzle("ciaoocabboccatan");
			solver.readDictionary(@"/home/giacomo/workspace/RuzzleSolver/RuzzleSolver/dictionary-IT.txt");
			solver.printBoard();
			Console.WriteLine("solving...");
			solver.solve();
			Console.WriteLine("solutions:");
			solver.printWords();
		}

		/// <summary>
		/// Inits the puzzle with the specified chars.
		/// </summary>
		public void initPuzzle(string chars){
			if (chars.Length != boardSize*boardSize)
				throw new InvalidOperationException("string is "+chars.Length+" characters long, and should be "+boardSize*boardSize);
			board = new char[boardSize,boardSize];
			for(int i = 0; i< chars.Length; i++) {
				board[i/boardSize,i%boardSize] = chars[i];
			}

		}

		/// <summary>
		/// Prints the board on the console.
		/// </summary>
		public void printBoard(){
			for (int i = 0; i < boardSize; i++) {
				for (int j = 0; j < boardSize; j++) {
					Console.Write(board[i,j]);
				}
				Console.WriteLine();
			}
		}

		public void printWords(){
			Console.WriteLine(words.Count + "words found.");
			words.Sort(new lenghtComparer());
			words.Reverse();
			foreach (var i in words) {
				Console.WriteLine(i);
			}
		}

		private class lenghtComparer:IComparer<string>{
			public int Compare(string x, string y){
            	return x.Length.CompareTo(y.Length);
			}
		}

		/// <summary>
		/// Solve this instance.
		/// </summary>
		public void solve(){
			words = new List<string>();
			for (int i = 0; i < boardSize; i++) {
				for (int j = 0; j < boardSize; j++) {
					Console.WriteLine("char " + board[i,j]);
					Word w = new Word();
					w.addLetter(new Point(i,j),board[i,j]);
					expand(w);
				}
			}
		}

		private void expand(Word w){
			Point head = w.Head;
			if(head.y > 0)
				addChar(w,new Point(head.x,head.y-1));
			if(head.x > 0)
				addChar(w,new Point(head.x-1,head.y));
			if(head.y < boardSize-1)
				addChar(w,new Point(head.x,head.y+1));
			if(head.x < boardSize-1)
				addChar(w,new Point(head.x+1,head.y));
			if(head.x > 0 && head.y > 0)
				addChar(w,new Point(head.x-1,head.y-1));
			if(head.x > 0 && head.y < boardSize-1)
				addChar(w,new Point(head.x-1,head.y+1));
			if(head.x < boardSize-1 && head.y > 0)
				addChar(w,new Point(head.x+1,head.y-1));
			if(head.x < boardSize-1 && head.y < boardSize-1)
				addChar(w,new Point(head.x +1,head.y+1));
		}

		/// <summary>
		/// Adds the char at the specified point to the word
		/// </summary>
		private void addChar(Word w, Point p){
			if(!w.Points.Contains(p)){
				w.addLetter(p,board[p.x,p.y]);
				List<string> st = dictionary.FindAll(s => (s.Length >= w.String.Length && s.Substring(0,w.String.Length) == w.String));//.BinarySearch(w.String,new PrefixComparer());
				if(st.Count == 0)
					w.removeLetter();
				else{
					if(st.Contains(w.String))
						words.Add(w.String);
					expand(w);
					w.removeLetter();
				}
			}
		}

		/// <summary>
		/// Reads the dictionary from an external file.
		/// </summary>
		/// <param name='path'>
		/// the path to the dictionary file
		/// </param>
		public void readDictionary(string path){
			dictionary = new List<string>(System.IO.File.ReadAllLines(path));
		}

	    private char[,] board;
		private const int boardSize = 4;
		private List<string> dictionary;
		private List<string> words;
	}
}
