using DllSky.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotButton : MonoBehaviour
{
    #region Variables
    public string id;
    public int uses;
    public float cooldown;

    [Header("Links")]
    public Text usesText;
    public Image slotImage;

    private SpaceShip playerSpaceship;
    #endregion

    #region Unity methods
    #endregion

    #region Public methods
    public void UpdateProperties(int _uses)
    {
        uses = _uses;
        usesText.text = uses.ToString();
    }

    public void Initialize(string _image, SpaceShip _playerSpaceship)
    {        
        slotImage.sprite = ResourcesManager.Load<Sprite>(ConstantsResourcesPath.SPRITES, _image);


        playerSpaceship = _playerSpaceship;
    }

    public void OnClick()
    {
        playerSpaceship.UseEquipment(id);
    }
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    #endregion
}
