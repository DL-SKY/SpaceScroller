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
    public float timer;

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

    public void Initialize(string _image, float _cooldown, SpaceShip _playerSpaceship)
    {        
        slotImage.sprite = ResourcesManager.Load<Sprite>(ConstantsResourcesPath.SPRITES, _image);
        cooldown = _cooldown;
        timer = 0.0f;

        playerSpaceship = _playerSpaceship;
    }

    public void OnClick()
    {
        if (timer > 0)
            return;

        playerSpaceship.UseEquipment(id);
        StartCoroutine(CooldownTimer());
    }
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    private IEnumerator CooldownTimer()
    {
        timer = cooldown;

        while (timer > 0)
        {
            yield return null;
            timer -= Time.deltaTime;
        }

        timer = 0.0f;
    }
    #endregion
}
