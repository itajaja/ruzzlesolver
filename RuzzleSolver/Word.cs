using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RuzzleSolver
{
	/// <summary>
	/// A word on the board. it keeps track of the tiles selected by the board
	/// </summary>
	public class Word
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="RuzzleSolver.Word"/> class.
		/// </summary>
		public Word (){
			word = new StringBuilder();
			points = new List<Point>();
		}

		/// <summary>
		/// Gets the point list. The returned value is immutable
		/// </summary>
		public ReadOnlyCollection<Point> Points{
			get { return this.points.AsReadOnly(); }
		}

		/// <summary>
		/// Gets the head point
		/// </summary>
		public Point Head{
			get { return this.points[points.Count-1]; }
		}

		public string String{
			get {return this.word.ToString();}
		}

		/// <summary>
		/// Adds a letter to the word.
		/// </summary>
		/// <param name='p'>
		/// The coordinates of the letter
		/// </param>
		/// <param name='c'>
		/// Letter to be added.
		/// </param>
		public void addLetter(Point p, char c){
			points.Add(p);
			word.Append(c);
		}

		/// <summary>
		/// Removes the last letter of the word.
		/// </summary>
		public void removeLetter(){
			points.RemoveAt(points.Count-1);
			word.Remove(word.Length-1,1);
		}

		private StringBuilder word;
		private List<Point> points;
	}

	/// <summary>
	/// Represents a point in 2D coordinates.
	/// </summary>
	public class Point:IEquatable<Point>{

		public Point(int x, int y){
			this.x = x;
			this.y = y;
		}

		public Point(){}

		public bool Equals (Point other){
			return (other.x==this.x && other.y==this.y);
		}
		public int x;
		public int y;
	}
}

