using DllSky.Extensions;
using DllSky.Managers;
using DllSky.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode00ScreenController : ScreenController
{
    #region Variables
    
    private GameMode00SceneController sceneController;
    #endregion

    #region Unity methods
    private void OnEnable()
    {
        EventManager.eventOnClickEsc += OnClickEsc;

        if (IsInit)
            StartCoroutine(Show());
    }

    private void OnDisable()
    {
        EventManager.eventOnClickEsc -= OnClickEsc;
    }
    #endregion

    #region Public methods
    public override void Initialize(object _data)
    {
        base.Initialize(_data);        

        StartCoroutine(Show());
    }

    public void OnClickEsc()
    {
        StartCoroutine(CloseCoroutine());
    }    
    #endregion

    #region Private methods    
    #endregion

    #region Coroutines
    private IEnumerator Show()
    {
        yield return MainGameManager.Instance.LoadSceneCoroutine(ConstantsScene.GAME_MODE_00);
        while (!GameMode00SceneController.Instance.isInit)
        {
            yield return null;
        }
        sceneController = GameMode00SceneController.Instance;

        yield return SplashScreenManager.Instance.HideSplashScreen();
    }

    private IEnumerator CloseCoroutine()
    {
        yield return SplashScreenManager.Instance.ShowBlack();

        Close();
    }
    #endregion
}
