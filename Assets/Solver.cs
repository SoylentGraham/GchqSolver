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

	public Texture2D	mTextureRows;
	public Texture2D	mTextureCols;
	public Texture2D	mTextureMerged;
	List<RowMeta>		mRows = new List<RowMeta>();
	List<RowMeta>		mCols = new List<RowMeta>();
	int					mRowWidth = 25;

	[Range(0,1000)]
	public int			mIterationsPerLoop = 1;
	[Range(0,91000)]
	public int			mIteration = 0;

	List<List<int>>		mRowMasksPerRow;
	List<List<int>>		mRowMasksPerCol;
	int					mTotalTotal = 0;

	
	void Start () {

		//	http://www.bbc.co.uk/news/uk-35058761
	

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



		mCols.Add( new RowMeta( new int[]{ 7, 2, 1, 1, 7 } ) );
		mCols.Add( new RowMeta( new int[]{ 1, 1, 2, 2, 1, 1 } ) );
		mCols.Add( new RowMeta( new int[]{ 1, 3, 1, 3, 1, 3, 1, 3, 1 } ) );
		mCols.Add( new RowMeta( new int[]{ 1, 3, 1, 1, 5, 1, 3, 1 } ) );
		mCols.Add( new RowMeta( new int[]{ 1,3, 1, 1, 4, 1, 3, 1 } ) );
		mCols.Add( new RowMeta( new int[]{ 1, 1, 1,2, 1,1 } ) );
		mCols.Add( new RowMeta( new int[]{ 7,1, 1, 1, 1, 1, 7 } ) );
		mCols.Add (new RowMeta (new int[]{ 1, 1, 3 }));
		mCols.Add (new RowMeta (new int[]{ 2, 1, 2,1, 8,2,1 }));
		mCols.Add( new RowMeta( new int[]{ 2,2, 1, 2, 1, 1, 1, 2 } ) );
		mCols.Add( new RowMeta( new int[]{ 1, 7, 3, 2, 1 } ) );
		mCols.Add( new RowMeta( new int[]{ 1, 2, 3, 1, 1, 1, 1, 1 } ) );
		mCols.Add( new RowMeta( new int[]{ 4, 1, 1, 2, 6 } ) );
		mCols.Add( new RowMeta( new int[]{ 3, 3, 1, 1, 1, 3, 1 } ) );
		mCols.Add( new RowMeta( new int[]{ 1, 2, 5, 2, 2 } ) );
		mCols.Add( new RowMeta( new int[]{ 2, 2, 1, 1, 1, 1, 1, 2, 1 } ) );
		mCols.Add( new RowMeta( new int[]{ 1, 3, 3, 2, 1, 8, 1 } ) );
		mCols.Add( new RowMeta( new int[]{ 6, 2, 1 } ) );
		mCols.Add( new RowMeta( new int[]{ 7, 1, 4, 1, 1, 3 } ) );
		mCols.Add( new RowMeta( new int[]{ 1, 1, 1, 1, 4 } ) );
		mCols.Add( new RowMeta( new int[]{ 1, 3, 1, 3, 7, 1 } ) );
		mCols.Add( new RowMeta( new int[]{ 1, 3, 1, 1, 1, 2, 1, 1, 4 } ) );
		mCols.Add( new RowMeta( new int[]{ 1, 3, 1, 4, 3, 3 } ) );
		mCols.Add( new RowMeta( new int[]{ 1, 1, 2, 2, 2, 6, 1 } ) );
		mCols.Add( new RowMeta( new int[]{ 7, 1, 3, 2, 1, 1 } ) );



		mRowMasksPerRow = GenerateRowMasks (mRows, true);
		mRowMasksPerCol = GenerateRowMasks (mCols, false);
		mTotalTotal = 0;
		for ( int a=0;	a<mRowMasksPerRow.Count;	a++ )
			for ( int b=0;	b<mRowMasksPerRow[a].Count;	b++ )
				for ( int c=0;	c<mRowMasksPerCol.Count;	c++ )
					for ( int d=0;	d<mRowMasksPerCol[c].Count;	d++ )
						mTotalTotal++;
		
		Debug.Log ("There are " + mTotalTotal + " combinations");

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

	bool ValidateRow(int[] Starts,RowMeta Row,int RowIndex)
	{
		//	todo: validate against known columns
		//	todo: validate against provided grids
		//	todo: move overflow check here?

		//	validate no overlaps (easier than re-organising the generations
		for (int b=0; b<Starts.Length; b++) {
			int Start = Starts [b];
			int End = Starts [b] + Row.mBlockLengths [b] + 1; //	+1 for white gap

			bool Overlap = false;

			//	our end should always be <= to anything that follows us
			for (int c=b+1; c<Starts.Length; c++) {
				if ( Starts[c] < End )
					Overlap = true;
			}

			if ( Overlap )
				return false;
		}

		return true;
	}

	bool ValidateCoord(int x, int y, bool IsOn )
	{
		if (IsOn)
			return true;

		//	check for ones which are OFF which NEED to be ON
		List<Vector2> MustBeOns = new List<Vector2> ();
		MustBeOns.Add (new Vector2 (3, 3));
		MustBeOns.Add (new Vector2 (4, 3));
		MustBeOns.Add (new Vector2 (12, 3));
		MustBeOns.Add (new Vector2 (13, 3));
		MustBeOns.Add (new Vector2 (21, 3));

		MustBeOns.Add (new Vector2 (6, 8));
		MustBeOns.Add (new Vector2 (7, 8));
		MustBeOns.Add (new Vector2 (10, 8));
		MustBeOns.Add (new Vector2 (14, 8));
		MustBeOns.Add (new Vector2 (15, 8));
		MustBeOns.Add (new Vector2 (18, 8));

		MustBeOns.Add (new Vector2 (6, 16));
		MustBeOns.Add (new Vector2 (11, 16));
		MustBeOns.Add (new Vector2 (16, 16));
		MustBeOns.Add (new Vector2 (20, 16));

		MustBeOns.Add (new Vector2 (3, 21));
		MustBeOns.Add (new Vector2 (4, 21));
		MustBeOns.Add (new Vector2 (9, 21));
		MustBeOns.Add (new Vector2 (10, 21));
		MustBeOns.Add (new Vector2 (15, 21));
		MustBeOns.Add (new Vector2 (20, 21));
		MustBeOns.Add (new Vector2 (21, 21));

		foreach (Vector2 MustBeOn in MustBeOns) {
			if (MustBeOn == new Vector2 (x, y))
			{
				Debug.LogWarning("Coord is missing: " + x + ","+ y);
				return false;
			}
		}

		return true;
	}


	bool ValidateRowMask(int RowMask,int RowIndex,bool IsRowNotColumn)
	{
		for (int i=0; i<mRowWidth; i++) {
			int y = IsRowNotColumn ? RowIndex : i;
			int x = IsRowNotColumn ? i : RowIndex;
			bool IsOn = (RowMask & (1 << i)) != 0;

			if ( !ValidateCoord( x, y, IsOn ) )
				return false;
		}
		return true;
	}


	List<int> GenerateRowMasks(RowMeta Row,int RowIndex,bool IsRowNotCol)
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

			//	validate row
			//	gr: if this fails we've moved along too far too?
			if ( !ValidateRow( Starts, Row, RowIndex ) )
			{
				CurrentIndex++;
				continue;
			}

			int RowMask = GenerateRowMask( Row, Starts );
			if ( !ValidateRowMask( RowMask, RowIndex, IsRowNotCol ) )
			{
				CurrentIndex++;
				continue;
			}

			//	valid, save
			StartLists.Add( NewList );

			AddUniqueRowMask( RowMasks, RowMask );
		}

		if (RowMasks.Count == 0)
			Debug.LogError ( (IsRowNotCol ? "row":"col" ) + " " + RowIndex + " generated 0 row masks");

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

	int GetRowMasksPerRowTotal(List<List<int>> RowMasksPerRow)
	{
		int LocalTotal = 0;
		for (int i=0; i<RowMasksPerRow.Count; i++)
			LocalTotal += RowMasksPerRow [i].Count;
		return LocalTotal;
	}

		
	void GenerateTexture(Texture2D Tex,List<List<int>> RowMasksPerRow,int RenderIteration)
	{
		int RunningTotal = 1;

		//	generate an iteration and render it
		for (int r=0; r<RowMasksPerRow.Count; r++) {
			List<int> RowMasks = RowMasksPerRow[r];

			int RowMatches = RowMasks.Count;
			//Debug.Log("Row " + r + " generated " + RowMasks.Count );
			//	error, didn't generate any combos
			if ( RowMatches == 0 )
				continue;
			//if ( RowMatches > 1 )
			//	continue;

			int RenderRowIteration = (RenderIteration / RunningTotal) % RowMatches;
			RunningTotal *= RowMatches;

			//	pick a combo and render it
			//int RenderRowIteration = RenderIteration % RunningTotal;
			//RenderRowIteration = RowMatches-1;
			RenderRowMask( RowMasks[RenderRowIteration], r, Tex );
		
		}

		Debug.Log ("Running total: " + RunningTotal);
		
		Tex.Apply ();
	}

	void GenerateTexture(Texture2D Tex,List<int> RowMasks)
	{
		//	clear texture
		for (int y=0; y<Tex.height; y++)
			for (int x=0; x<Tex.width; x++)
				Tex.SetPixel (x, y, new Color (x / (float)Tex.width, y / (float)Tex.height, 0));
		
		for (int r=0; r<RowMasks.Count; r++) {
			int RowMask = RowMasks[r];
			RenderRowMask( RowMask, r, Tex );
		}
		
		Tex.Apply ();
	}

	List<List<int>> GenerateRowMasks(List<RowMeta> Rows,bool IsRowNotCol)
	{
		List<List<int>> RowsMasksPerRow = new List<List<int>> ();

		//	generate an iteration and render it
		for (int r=0; r<Rows.Count; r++) {
			List<int> RowMasks = GenerateRowMasks (Rows [r], r, IsRowNotCol);

			RowsMasksPerRow.Add( RowMasks );
		}
		return RowsMasksPerRow;
	}

	List<int> GetRowMaskItertion(List<List<int>> RowMasksPerRow,int Iteration)
	{
		List<int> Output = new List<int> ();
		int RunningTotal = 1;

		int LocalTotal = GetRowMasksPerRowTotal (RowMasksPerRow);
		Debug.Log(Iteration + "/" + LocalTotal);

		//	generate an iteration and render it
		for (int r=0; r<RowMasksPerRow.Count; r++) {
			List<int> RowMasks = RowMasksPerRow[r];


			int LocalIt = Iteration;
			LocalIt /= RunningTotal;

			int RenderRowIteration = LocalIt % RowMasks.Count;
			RunningTotal *= RowMasks.Count;

			Output.Add( RowMasks[RenderRowIteration] );
		}

		return Output;
	}

	void GenerateMergedTexture(Texture2D Tex,List<int> RowRowMasks,List<int> ColRowMasks)
	{
		//	clear texture
		for (int y=0; y<Tex.height; y++)
			for (int x=0; x<Tex.width; x++)
				Tex.SetPixel (x, y, new Color (x / (float)Tex.width, y / (float)Tex.height, 0));

		int GoodCount = 0;

		for (int i=0; i<RowRowMasks.Count*ColRowMasks.Count;	i++)
		{
			int x = i % RowRowMasks.Count;
			int y = i / RowRowMasks.Count;

			//	get col value
			bool ColOn = (ColRowMasks[x] & (1<<y))!=0;
			bool RowOn = (RowRowMasks[y] & (1<<x))!=0;
			bool Goodxy = ColOn == RowOn;
			Color Colour = new Color32( 0, 0, 0, 255 );

			Colour.r = ColOn ? 255 : 0;
			Colour.g = RowOn ? 255 : 0;
			Colour.b = Goodxy ? 255 : 0;

			Tex.SetPixel( x,y, Colour );

			if ( Goodxy )
				GoodCount ++;
		}

		float GoodCountf = GoodCount / (float)(RowRowMasks.Count * ColRowMasks.Count);
		if (GoodCountf > 0.80f) {
			Debug.LogWarning ("Solved: " + (GoodCountf * 100.0f) + "%");
		}

		Tex.Apply ();
	}

	void ClearTexture(Texture2D Tex)
	{
		//	clear texture
		for (int y=0; y<Tex.height; y++)
			for (int x=0; x<Tex.width; x++)
				Tex.SetPixel (x, y, new Color (x / (float)Tex.width, y / (float)Tex.height, 0));
		Tex.Apply ();
	}


	void Iteration () {
		
		//int RowLocalTotalCombos = GetRowMasksPerRowTotal (mRowMasksPerRow);
		int RowLocalTotalCombos = mTotalTotal;
		//int RowIteration = mIteration ;
		//int RowIteration = mIteration % RowLocalTotalCombos;
		//int ColIteration = mIteration / RowLocalTotalCombos;

		int RowIteration = mIteration;
		int ColIteration = mIteration;

		ClearTexture (mTextureRows);
		ClearTexture (mTextureCols);
		ClearTexture (mTextureMerged);
	

		GenerateTexture (mTextureRows, mRowMasksPerRow, RowIteration );
	//	GenerateTexture (mTextureCols, mRowMasksPerCol, ColIteration );
		/*

		//	generate the masks for this iteration
		List<int> RowRowMasks = GetRowMaskItertion (mRowMasksPerRow, RowIteration);
		List<int> ColRowMasks = GetRowMaskItertion (mRowMasksPerCol, ColIteration);
		GenerateTexture (mTextureRows, RowRowMasks);
		GenerateTexture (mTextureCols, ColRowMasks);


		GenerateMergedTexture (mTextureMerged, RowRowMasks, ColRowMasks);
		*/
	}



	void Update () {

		int ItCount = mIterationsPerLoop - (Random.Range (0, mIterationsPerLoop / 2));
		for (int x=0; x<mIterationsPerLoop; x++) {
			mIteration++;
			//mIteration = mIteration % mTotalTotal;
			Iteration ();
		}

		if (mIterationsPerLoop == 0)
			Iteration ();
	}



}
