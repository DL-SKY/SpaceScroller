using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    private SpaceShip spaceShip;
    #endregion

    #region Unity methods
    /*private void Awake()    //Start()?
    {
        
    }*/

    /*private void Update()
    {
        //Влево
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            spaceShip.MoveToLeft();     //(true)
        }
        //Вправо
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            spaceShip.MoveToRight();    //(true)
        }
    }*/
    #endregion

    #region Public methods
    public void Initialize(object _data)
    {
        spaceShip = GetComponent<SpaceShip>();

        spaceShip.Initialize(null);

        spaceShip.dontMove = true;
        spaceShip.type = EnumSpaceObject.Player;

        tag = ConstantsTag.TAG_PLAYER;
    }

    public SpaceShip GetPlayerSpaceship()
    {
        return spaceShip;
    }
    #endregion

    #region Protected methods
    #endregion

    #region Private methods
    #endregion
}
