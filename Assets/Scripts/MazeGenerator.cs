using System;
using System.Collections.Generic;
using UnityEngine;


// -------------------------------------------------------------------------
// Public Enums:
// -------------
//   Direction
// -------------------------------------------------------------------------

#region .  Public Properties  .

public enum Direction
{
    Down,
    Left,
    Right,
    Up
}

#endregion



public class MazeGenerator : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Public Properties:
    // ------------------
    //   MazeHeight
    //   MazeWidth
    //   StartX
    //   StartY
    // -------------------------------------------------------------------------

    #region .  Public Properties  .

    [Range(1, 100)] public int MazeHeight = 30;
    [Range(1, 100)] public int MazeWidth  = 30;

    public int StartX;
    public int StartY;

    #endregion



    // -------------------------------------------------------------------------
    // Private Properties:
    // -------------------
    //   _currentCell
    //   _mazeCells
    //   _directions
    // -------------------------------------------------------------------------

    #region .  Private Properties  .

    private Vector2Int  _currentCell;
    private MazeCell[,] _mazeCells;

    readonly List<Direction> _directions = new List<Direction>
    {
        Direction.Down,
        Direction.Left,
        Direction.Right,
        Direction.Up
    };

    #endregion



    // -------------------------------------------------------------------------
    // Public Methods:
    // ---------------
    //   CarvePath()
    //   ClearMaze()
    //   GetMaze()
    // -------------------------------------------------------------------------

    #region .  CarvePath()  .
    // -------------------------------------------------------------------------
    //   Method.......:  CarvePath()
    //   Description..:  Starting with the x, y passed in, carves a path through
    //                   the maze until it encounters a "dead end" (a cell with
    //                   no unvisited neighbours).
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void CarvePath(int x, int y)
    {
        bool             deadEnd = false;
        List<Vector2Int> path    = new List<Vector2Int>();

        // Check to make sure the start position is within the maze bounds.  If
        // not set them to a deault value and display a console warning message.
        if (x < 0 || x >= this.MazeWidth || y < 0 || y >= this.MazeHeight)
        {
            Debug.LogWarning($"Start position ({x}, {y}) is out of bounds.  Setting to default value (0, 0).");
            x = 0;
            y = 0;
        }

        // Set the current cell to the start position.
        this._currentCell = new Vector2Int(x, y);

        // Loop until there are no unvisited neighbours.
        while (!deadEnd)
        {
            // Get the next cell.
            Vector2Int nextCell = this.CheckNeighbours();

            if (nextCell == this._currentCell)
            {
                // Loop backwards through the path.
                for (int i = path.Count - 1; i >= 0; i--)
                {
                    this._currentCell = path[i];
                    path.RemoveAt(i);
                    nextCell = CheckNeighbours();

                    // If found a valid neighbor, break out of the loop.
                    if (nextCell != this._currentCell)
                    {
                        break;
                    }
                }

                if (nextCell == this._currentCell)
                {
                    deadEnd = true;
                }
            }
            else
            {
                this.BreakWalls(this._currentCell, nextCell);                                 // Set wall flage on these two cells
                this._mazeCells[this._currentCell.x, this._currentCell.y].IsVisited = true;   // Mark the current cell as visited.
                this._currentCell = nextCell;                                                 // Set currentCell to the valid neighbor cell.
                path.Add(_currentCell);                                                       // Add the current cell to the path.
            }
        }

    }   // CarvePath()
    #endregion


    #region .  ClearMaze()  .
    // -------------------------------------------------------------------------
    //   Method.......:  ClearMaze()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void ClearMaze()
    {
        this._mazeCells = new MazeCell[this.MazeWidth, this.MazeHeight];

    }   // ClearMaze()
    #endregion


    #region .  GetMaze()  .
    // -------------------------------------------------------------------------
    //   Method.......:  GetMaze()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public MazeCell[,] GetMaze()
    {
        this._mazeCells = new MazeCell[this.MazeWidth, this.MazeHeight];

        for (int x = 0; x < this.MazeWidth; x++)
        {
            for (int y = 0; y < this.MazeHeight; y++)
            {
                this._mazeCells[x, y] = new MazeCell(x, y);
            }
        }

        this.CarvePath(this.StartX, this.StartY);

        return this._mazeCells;

    }   // GetMaze()
    #endregion



    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   BreakWalls()
    //   CheckNeighbours()
    //   GenerateMaze()  --  COMMENTED OUT
    //   GetNeighbours()
    //   GetRandomDirections()
    //   IsCellValid()
    // -------------------------------------------------------------------------

    #region .  BreakWalls()  .
    // -------------------------------------------------------------------------
    //   Method.......:  BreakWalls()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void BreakWalls(Vector2Int primaryCell, Vector2Int secondaryCell)
    {
        if      (primaryCell.x > secondaryCell.x)                                // Neighbor is to the left
        {
            this._mazeCells[primaryCell.x,   primaryCell.y  ].IsLeftWall = false;
        }
        else if (primaryCell.x < secondaryCell.x)                                // Neighbor is to the right
        {
            this._mazeCells[secondaryCell.x, secondaryCell.y].IsLeftWall = false;
        }
        else if (primaryCell.y < secondaryCell.y)                                // Neighbor is above
        {
            this._mazeCells[primaryCell.x,   primaryCell.y  ].IsTopWall  = false;
        }
        else if (primaryCell.y > secondaryCell.y)                                // Neighbor is below
        {
            this._mazeCells[secondaryCell.x, secondaryCell.y].IsTopWall  = false;
        }

    }   // BreakWalls()
    #endregion


    #region .  CheckNeighbours()  .
    // -------------------------------------------------------------------------
    //   Method.......:  CheckNeighbours()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private Vector2Int CheckNeighbours()
    {
        // Get a random direction from the list of directions.
        List<Direction> randomDirections = this.GetRandomDirections();

        // Check each direction in the random directions list.
        foreach (var direction in randomDirections)
        {
            Vector2Int neighbour = this._currentCell;

            // Move the neighbour cell in the direction of the current direction.
            switch (direction)
            {
                case Direction.Down:
                    neighbour.y--;
                    break;

                case Direction.Left:
                    neighbour.x--;
                    break;

                case Direction.Right:
                    neighbour.x++;
                    break;

                case Direction.Up:
                    neighbour.y++;
                    break;
            }

            // Check if the neighbour cell is valid (within bounds and not visited).
            if (this.IsCellValid(neighbour.x, neighbour.y))
            {
                return neighbour;
            }
        }

        // If no valid neighbour is found, return the current cell.
        return this._currentCell;

    }   // CheckNeighbours()
    #endregion


    #region .  GenerateMaze()  --  COMMENTED OUT  .
    //// -------------------------------------------------------------------------
    ////   Method.......:  GenerateMaze()
    ////   Description..:  
    ////   Parameters...:  None
    ////   Returns......:  Nothing
    //// -------------------------------------------------------------------------
    //private void GenerateMaze(Vector2Int currentCell)
    //{
    //    mazeCells[currentCell.x, currentCell.y].IsVisited = true;

    //    // Get the neighbors of the current cell
    //    Vector2Int[] neighbors = GetNeighbours(currentCell);

    //    foreach (var neighbor in neighbors)
    //    {
    //        if (!mazeCells[neighbor.x, neighbor.y].IsVisited)
    //        {
    //            // Remove walls between current cell and neighbor
    //            BreakWalls(currentCell, neighbor);

    //            // Recursively generate maze from the neighbor
    //            GenerateMaze(neighbor);
    //        }
    //    }

    //}   // GenerateMaze()
    #endregion


    #region .  GetNeighbours()  .
    // -------------------------------------------------------------------------
    //   Method.......:  GetNeighbours()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private Vector2Int[] GetNeighbours(Vector2Int currentCell)
    {
        Vector2Int[] neighbours = new Vector2Int[4];

        int index = 0;

        if (currentCell.x > 0)
            neighbours[index++] = new Vector2Int(currentCell.x - 1, currentCell.y);         // Left

        if (currentCell.x < this.MazeWidth - 1)
            neighbours[index++] = new Vector2Int(currentCell.x + 1, currentCell.y);         // Right

        if (currentCell.y > 0)
            neighbours[index++] = new Vector2Int(currentCell.x,     currentCell.y - 1);     // Down

        if (currentCell.y < this.MazeHeight - 1)
            neighbours[index++] = new Vector2Int(currentCell.x,     currentCell.y + 1);     // Up

        Array.Resize(ref neighbours, index);

        return neighbours;

    }   // GetNeighbours()
    #endregion


    #region .  GetRandomDirections()  .
    // -------------------------------------------------------------------------
    //   Method.......:  GetRandomDirections()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private List<Direction> GetRandomDirections()
    {
        // Make a copy of the directions list to mess around with.
        List<Direction> direction = new List<Direction>(this._directions);

        // Make a directions list to put thr random directions into.
        List<Direction> randomDirections = new List<Direction>();

        // Loop until the random list is empty
        while (direction.Count > 0)
        {
            int randomDirection = UnityEngine.Random.Range(0, direction.Count);    // Get a random index
            randomDirections.Add(direction[randomDirection]);                      // Add the random direction to the rndDir list
            direction.RemoveAt(randomDirection);                                   // Remove that direction so it doesn't get chosen again
        }

        // All four directions are in a random order, return the queue.
        return randomDirections;

    }   // GetRandomDirections()
    #endregion


    #region .  IsCellValid()  .
    // -------------------------------------------------------------------------
    //   Method.......:  IsCellValid()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private bool IsCellValid(int x, int y)
    {
        // If the cell is outside of the map or it has already been visited, it's not valid.
        bool isValid = (x >= 0)
                    && (y >= 0)
                    && (x <= this.MazeWidth  - 1)
                    && (y <= this.MazeHeight - 1)
                    && (!this._mazeCells[x, y].IsVisited);

        return isValid;

    }   // IsCellValid()
    #endregion


}   // class MazeGenerator
