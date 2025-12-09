using UnityEngine;


public class MazeCell
{
    // -------------------------------------------------------------------------
    // Public Properties:
    // ------------------
    //   IsLeftWall
    //   IsTopWall
    //   IsVisited
    //   X
    //   Y
    //   Position
    // -------------------------------------------------------------------------

    #region .  Public Properties  .

    public bool IsLeftWall;
    public bool IsTopWall;
    public bool IsVisited;

    public int  X;
    public int  Y;

    public Vector2Int Position
    {
        get { return new Vector2Int(X, Y); }
    }

    #endregion



    // -------------------------------------------------------------------------
    // Public Methods:
    // ---------------
    //   MazeCell()
    // -------------------------------------------------------------------------

    #region .  MazeCell()  .
    // -------------------------------------------------------------------------
    //   Method.......:  MazeCell()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public MazeCell(int x, int y)
    {
        this.IsLeftWall = true;
        this.IsTopWall  = true;
        this.IsVisited  = false;

        this.X = x;
        this.Y = y;

    }   // MazeCell()

    #endregion


}   // class MazeCell
