﻿using DllSky.Managers;
using DllSky.Utility;
using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreenController : ScreenController
{
    #region Variables
    [Header("Slots")]
    public Transform slotsParent;
    public List<SlotButton> slotButtons = new List<SlotButton>();

    private string spaceshipID;
    private GameSceneController sceneController;
    private GameplayManager gameplay;
    private SpaceShip playerSpaceship;
    #endregion

    #region Unity methods
    private void OnEnable()
    {
        EventManager.eventOnClickEsc += OnClickEsc;
        EventManager.eventOnChangePlayerSlots += OnChangePlayerSlots;

        LeanTouch.OnFingerTap += OnTap;
        LeanTouch.OnFingerSwipe += OnSwipe;

        if (IsInit)
            StartCoroutine(Show(spaceshipID));
    }

    private void OnDisable()
    {
        EventManager.eventOnClickEsc -= OnClickEsc;
        EventManager.eventOnChangePlayerSlots -= OnChangePlayerSlots;

        LeanTouch.OnFingerTap -= OnTap;
        LeanTouch.OnFingerSwipe -= OnSwipe;
    }

    private void Update()
    {
        if (!IsInit)
            return;

        //Влево (на ПК)
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerSpaceship.MoveToLeft();               //(true)
        }
        //Вправо (на ПК)
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerSpaceship.MoveToRight();              //(true)
        }

        //TEST: Missile
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerSpaceship.UseEquipment("testMissileLauncher");
        }
    }
    #endregion

    #region Public methods
    public override void Initialize(object _data)
    {
        var CONFIGS = Global.Instance.CONFIGS;
        spaceshipID = _data as string;

        if (string.IsNullOrEmpty(spaceshipID) || CONFIGS.spaceships.Find(x => x.id == spaceshipID) == null)
            //spaceshipID = CONFIGS.spaceships.Find(x => x.id.Contains("TestShip")).id;
            spaceshipID = CONFIGS.spaceships[0].id;

        StartCoroutine(Show(spaceshipID));
    }

    public void OnClickEsc()
    {
        StartCoroutine(CloseCoroutine());
    }
    #endregion

    #region Private methods   
    private void OnTap(LeanFinger _finger)
    {
        var tap = _finger.ScreenPosition;
        Debug.Log("<color=#FFD800>[INFO]</color> TAP: " + tap + " /OverGUI: " +  _finger.StartedOverGui.ToString());

        if (_finger.StartedOverGui)
            return;

        var center = Screen.width / 2;

        //Тап влево
        if (tap.x < center)
            playerSpaceship.MoveToLeft();           //(true)
        //Тап вправо
        else if (tap.x > center)
            playerSpaceship.MoveToRight();          //(true)
    }

    private void OnSwipe(LeanFinger _finger)
    {
        var swipe = _finger.SwipeScreenDelta;
        Debug.Log("<color=#FFD800>[INFO]</color> Swipe: " + swipe + " /OverGUI: " + _finger.StartedOverGui.ToString());

        if (_finger.StartedOverGui)
            return;
        
        var absX = Mathf.Abs(swipe.x);
        var absY = Mathf.Abs(swipe.y);

        //Вертикальный свайп игнорируем
        if ( absY > absX)        
            return;

        //Свайп влево
        if (swipe.x < 0)
            playerSpaceship.MoveToLeft();           //(true)
        //Свайп вправо
        else if (swipe.x > 0)
            playerSpaceship.MoveToRight();          //(true)
    }

    private void OnChangePlayerSlots()
    {
        if (!IsInit)
            return;
        
        var playerSlots = playerSpaceship.equipments.Count;
        var guiSlots = slotButtons.Count;

        //Проверяем кол-во слотов
        if (playerSlots > guiSlots)
        {
            foreach (var equipment in playerSpaceship.equipments)
            {
                var button = slotButtons.Find(x => x.id == equipment.ID);

                if (button == null)
                {
                    var newButton = Instantiate(ResourcesManager.LoadPrefab(ConstantsResourcesPath.ELEMENTS_UI, "SlotButton"), slotsParent);
                    var newSlot = newButton.GetComponent<SlotButton>();

                    newSlot.id = equipment.ID;
                    newSlot.Initialize(equipment.GetSlotImage(), equipment.Coolldown, playerSpaceship);
                    newSlot.UpdateProperties(equipment.Uses);

                    slotButtons.Add(newSlot);
                }
                else
                {
                    button.UpdateProperties(equipment.Uses);
                }
            }
        }
        else if (playerSlots < guiSlots)
        {
            for (int i = guiSlots-1; i >= 0; i--)
            {
                var equipment = playerSpaceship.equipments.Find(x => x.ID == slotButtons[i].id);

                if (equipment == null)
                {
                    Destroy(slotButtons[i].gameObject);
                    slotButtons.RemoveAt(i);                    
                }
                else
                {
                    slotButtons[i].UpdateProperties(equipment.Uses);
                }
            }
        }
        else
        {
            foreach (var equipment in playerSpaceship.equipments)
            {
                Debug.Log("!!! OnChangePlayerSlots() playerSlots == guiSlots");
                var button = slotButtons.Find(x => x.id == equipment.ID);
                button.UpdateProperties(equipment.Uses);
            }
        }
    }
    #endregion

    #region Coroutines
    private IEnumerator Show(string _spaceship)
    {
        yield return MainGameManager.Instance.LoadSceneCoroutine(ConstantsScene.GAME_SCENE);

        sceneController = GameSceneController.Instance;
        sceneController.Initialize(_spaceship);
        gameplay = sceneController.GetGameplayManager();
        playerSpaceship = gameplay.player.GetPlayerSpaceship();

        while (!sceneController.isInit)
        {
            yield return null;
        }

        IsInit = true;

        OnChangePlayerSlots();

        yield return SplashScreenManager.Instance.HideSplashScreen();        
    }

    private IEnumerator CloseCoroutine()
    {
        yield return SplashScreenManager.Instance.ShowBlack();

        Close();
    }
    #endregion
}
