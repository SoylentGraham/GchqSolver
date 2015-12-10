using UnityEngine;
using System.Collections;
using System.Collections.Generic;


class RowMeta
{
	public int[]	mBlockLengths;

	public RowMeta(int[] Lengths)
	{
		mBlockLengths = Lengths;
	}
};



public class Solver : MonoBehaviour {

	public Texture2D	mTexture;
	List<RowMeta>		mRows = new List<RowMeta>();
	List<RowMeta>		mCols = new List<RowMeta>();
	int					mRowWidth = 25;
	int 				mIteration = 0;

	void Start () {
	

		mRows.Add( new RowMeta( new int[]{ 7, 3, 1, 1, 7 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 1, 2, 2, 1, 1 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 3, 1, 3, 1, 1, 3, 1 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 3, 1, 1, 6, 1, 3, 1 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 3, 1, 5, 2, 1, 3, 1 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 1, 2, 1, 1 } ) );
		mRows.Add( new RowMeta( new int[]{ 7, 1, 1, 1, 1, 1, 7 } ) );
		mRows.Add( new RowMeta( new int[]{ 3, 3 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 2, 3, 1, 1, 3, 1, 1, 2 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 1, 3, 2, 1, 1 } ) );
		mRows.Add( new RowMeta( new int[]{ 4, 1, 4, 2, 1, 2 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 1, 1, 1, 1, 4, 1, 3 } ) );
		mRows.Add( new RowMeta( new int[]{ 2, 1, 1, 1, 1, 2, 5 } ) );
		mRows.Add( new RowMeta( new int[]{ 3, 2, 2, 6, 3, 1 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 9, 1, 1, 2, 1 } ) );
		mRows.Add( new RowMeta( new int[]{ 2, 1, 2, 2, 3, 1 } ) );
		mRows.Add( new RowMeta( new int[]{ 3, 1, 1, 1, 1, 5, 1 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 2, 2, 5 } ) );
		mRows.Add( new RowMeta( new int[]{ 7, 1, 2, 1, 1, 1, 3 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 1, 2, 1, 2, 2, 1 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 3, 1, 4, 5, 1 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 3, 1, 3, 10, 2 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 3, 1, 1, 6, 6 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 1, 2, 1, 1, 2 } ) );
		mRows.Add( new RowMeta( new int[]{ 7, 2, 1, 2, 5 } ) );


		/*
		mCols.Add( new RowMeta( new int[]{ 7, 3, 1, 1, 7 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 1, 2, 2, 1, 1 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 3, 1, 3, 1, 1, 3, 1 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 3, 1, 1, 6, 1, 3, 1 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 3, 1, 5, 2, 1, 3, 1 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 1, 2, 1, 1 } ) );
		mRows.Add( new RowMeta( new int[]{ 7, 1, 1, 1, 1, 1, 7 } ) );
		mRows.Add( new RowMeta( new int[]{ 3, 3 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 2, 3, 1, 1, 3, 1, 1, 2 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 1, 3, 2, 1, 1 } ) );
		mRows.Add( new RowMeta( new int[]{ 4, 1, 4, 2, 1, 2 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 1, 1, 1, 1, 4, 1, 3 } ) );
		mRows.Add( new RowMeta( new int[]{ 2, 1, 1, 1, 1, 2, 5 } ) );
		mRows.Add( new RowMeta( new int[]{ 3, 2, 2, 6, 3, 1 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 9, 1, 1, 2, 1 } ) );
		mRows.Add( new RowMeta( new int[]{ 2, 1, 2, 2, 3, 1 } ) );
		mRows.Add( new RowMeta( new int[]{ 3, 1, 1, 1, 1, 5, 1 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 2, 2, 5 } ) );
		mRows.Add( new RowMeta( new int[]{ 7, 1, 2, 1, 1, 1, 3 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 1, 2, 1, 2, 2, 1 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 3, 1, 4, 5, 1 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 3, 1, 3, 10, 2 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 3, 1, 1, 6, 6 } ) );
		mRows.Add( new RowMeta( new int[]{ 1, 1, 2, 1, 1, 2 } ) );
		mRows.Add( new RowMeta( new int[]{ 7, 2, 1, 2, 5 } ) );
		*/
	}



	bool BuildCombo(List<int> Starts)
	{
		int LastStart = 0;
		for (int i=0; i<Starts.Count; i++) {
			if (Starts [i] != -1)
			{
				LastStart = Starts [i];
				continue;
			}

			//	need to calc a new start for i

		}
		return true;
	}

	void Assert(bool Condition,string Error)
	{
		if (!Condition)
			throw new System.Exception ("Assert: " + Error);
	}

	int GenerateRowMask(RowMeta Row,int[] Starts)
	{
		int RowMask = 0;
		Assert (Row.mBlockLengths.Length == Starts.Length, "Row size doesn't match starts size");
		for (int i=0; i<Starts.Length; i++) {
			//	gr: check for overlap
			Assert (Starts [i] >= 0, "Start not initialised");
			for (int b=0; b<Row.mBlockLengths[i]; b++)
				RowMask |= 1 << (Starts [i] + b);
		}
		return RowMask;
	}

	int[] GenerateStarts(RowMeta Row,int[] BaseStarts)
	{
		int[] Starts = new int[Row.mBlockLengths.Length];
		BaseStarts.CopyTo (Starts, 0);

		//	work out all the starts we need to generate
		for ( int b=0;	b<Row.mBlockLengths.Length;	b++ )
		{
			//	already has a start
			if ( Starts[b]!=-1 )
				continue;

			int NewStart = 0;
			if ( b>0 )
			{
				NewStart = Starts[b-1];
				NewStart += 1;	//	white gap
				NewStart += Row.mBlockLengths[b-1];
			}
			Starts[b] = NewStart;
		}

		int LastIndex = Starts.Length - 1;
		int Tail = Starts [LastIndex] + Row.mBlockLengths [LastIndex];// + 1;	//	don't +1 as tail doesnt NEED a whitespace

		//	row exceeds bounds, any others will also surely fail
		if ( Tail > mRowWidth )
		{
			//Debug.Log("Tail "+Tail+ " > mRowWidth " + mRowWidth);
			return null;
		}

		return Starts;
	}

	bool AddUniqueRowMask(List<int> RowMasks,int RowMask)
	{
		//	find dupe
		for (int i=0; i<RowMasks.Count; i++)
			if (RowMasks [i] == RowMask)
				return false;

		RowMasks.Add( RowMask );
		return true;
	}


	List<int> GenerateRowMasks(RowMeta Row)
	{
		List<int> RowMasks = new List<int>();

		List<int[]> StartLists = new List<int[]> ();

		//	init first
		int RowWidth = Row.mBlockLengths.Length;
		int CurrentIndex = 0;
		{
			int[] InitialList = new int[RowWidth];
			for (int i=0; i<RowWidth; i++)
				InitialList [i] = -1;
			StartLists.Add (InitialList);
		}


		while ( CurrentIndex < RowWidth )
		{
			//	grab last list
			int[] NewList = new int[RowWidth];
			StartLists[StartLists.Count-1].CopyTo( NewList, 0 );

			//	increment (insert white gap)
			//	this will set initial one to 0,-1,-1-1--1-1
			NewList[CurrentIndex] += 1;

			//	run 
			//	if okay, save base list
			//	else if oob currentindex++
			int[] Starts = GenerateStarts(Row,NewList);

			//	failed, skip and go to next index
			if ( Starts == null )
			{
				CurrentIndex++;
				continue;
			}

			//	valid, save
			StartLists.Add( NewList );

			int RowMask = GenerateRowMask( Row, Starts );
			AddUniqueRowMask( RowMasks, RowMask );

			//break;
		}
		return RowMasks;
	}

	void RenderRowMask(int RowMask,int Row,Texture2D Tex)
	{
		int y = Row;
		for (int x=0; x<mRowWidth; x++) {
			bool On = (RowMask & (1 << x)) != 0;
			Color Colour = On ? Color.black : Color.white;
			Tex.SetPixel (x, Tex.height-1 - y, Colour );
		}
	}

	void Update () {

		//if (mIteration > 0)
		//	return;
		mIteration++;
	
	
		//	clear texture
		for (int y=0; y<mTexture.height; y++)
			for (int x=0; x<mTexture.width; x++)
				mTexture.SetPixel (x, y, new Color (x / (float)mTexture.width, y / (float)mTexture.height, 0));

		//	generate an iteration and render it
		for (int r=0; r<mRows.Count; r++) {
			List<int> RowMasks = GenerateRowMasks (mRows [r]);

			Debug.Log("Row " + r + " generated " + RowMasks.Count );


			//	error, didn't generate any combos
			if ( RowMasks.Count == 0 )
				continue;

			//	pick a combo and render it
			RenderRowMask( RowMasks[Mathf.Min(mIteration, RowMasks.Count-1)], r, mTexture );
		}

		mTexture.Apply ();

	}

}
