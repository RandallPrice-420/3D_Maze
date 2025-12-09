using UnityEngine;


public class MazeCellObject : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Public Properties:
    // ------------------
    //   BottomWall
    //   LeftWall
    //   RightWall
    //   TopWall
    // -------------------------------------------------------------------------

    #region .  Public Properties  .

    [SerializeField] GameObject BottomWall;
    [SerializeField] GameObject LeftWall;
    [SerializeField] GameObject RightWall;
    [SerializeField] GameObject TopWall;

    #endregion



    // -------------------------------------------------------------------------
    // Public Methods:
    // ---------------
    //   Init()
    // -------------------------------------------------------------------------

    #region .  Init()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Init()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void Init(bool bottom, bool left, bool right, bool top)
    {
        // Set the walls to be active or inactive based on the cell's properties
        this.BottomWall.SetActive(bottom);
        this.LeftWall  .SetActive(left);
        this.RightWall .SetActive(right);
        this.TopWall   .SetActive(top);

    }   // Init()
    #endregion


}   // class MazeCellObject
