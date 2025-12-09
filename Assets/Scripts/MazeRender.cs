using UnityEngine;


public class MazeRender : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Public Properties:
    // ------------------
    //   CellSize
    // -------------------------------------------------------------------------

    #region .  Public Properties  .

    public float CellSize = 1.0f;       // Physical size of the maze cell

    #endregion



    // -------------------------------------------------------------------------
    // Private Properties:
    // -------------------
    //   _mazeGenerator
    //   _mazeCellPrefab
    // -------------------------------------------------------------------------

    #region .  Private Properties  .

    [SerializeField] MazeGenerator _mazeGenerator;
    [SerializeField] GameObject    _mazeCellPrefab;

    #endregion



    // -------------------------------------------------------------------------
    // Public Methods:
    // ---------------
    //   Button_NewMaze()
    // -------------------------------------------------------------------------

    #region .  Button_NewMaze()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Button_NewMaze()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void Button_NewMaze()
    {
        this._mazeGenerator.ClearMaze();
        this.GenerateMaze();

    }   // Button_NewMaze()
    #endregion



    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   GenerateMaze()
    //   Start()
    // -------------------------------------------------------------------------

    #region .  GenerateMaze()  .
    // -------------------------------------------------------------------------
    //   Method.......:  GenerateMaze()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void GenerateMaze()
    {
        MazeCell[,] maze = this._mazeGenerator.GetMaze();

        // Loop through the maze and instantiate the maze cell objects.
        for (int x = 0; x < this._mazeGenerator.MazeWidth; x++)
        {
            for (int y = 0; y < this._mazeGenerator.MazeHeight; y++)
            {
                // Instantiate the maze cell object at the correct position.
                GameObject newCell = Instantiate(this._mazeCellPrefab, new Vector3((float)x * this.CellSize, 0f, (float)y * this.CellSize), Quaternion.identity, transform);

                // Set the name of the cell object for easier debugging.
                MazeCellObject mazeCellObject = newCell.GetComponent<MazeCellObject>();

                bool left = maze[x, y].IsLeftWall;
                bool top  = maze[x, y].IsTopWall;

                // Bottom and right cells are deactivated by default unless we are at the bottom or right.
                bool bottom = (y == 0);
                bool right  = (x == this._mazeGenerator.MazeWidth - 1);

                mazeCellObject.Init(bottom, left, right, top);
            }
        }

    }   // GenerateMaze()
    #endregion


    #region .  Start()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Start()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void Start()
    {
        this.GenerateMaze();

    }   // Start()
    #endregion


}   // class MazeRender
