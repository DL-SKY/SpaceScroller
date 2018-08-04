using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScreenController : ScreenController
{
    #region Variables
    #endregion

    #region Unity methods
    private void OnEnable()
    {
        if (IsInit)
            StartCoroutine(Show());
    }
    #endregion

    #region Buttons method
    public void OnClickSettings()
    {
        StartCoroutine(Dialog());
    }

    public void OnClickGameMode00()
    {
        StartCoroutine(GameMode00());
    }

    public void OnClickPlay()
    {
        StartCoroutine(StartPlay());
    }
    #endregion

    #region Public methods
    public override void Initialize(object _data)
    {
        base.Initialize(_data);

        StartCoroutine(Show());
    }
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    private IEnumerator Show()
    {
        yield return MainGameManager.Instance.LoadSceneCoroutine(ConstantsScene.MAIN_MENU);

        yield return SplashScreenManager.Instance.HideSplashScreen();
        yield return null;
    }

    private IEnumerator Dialog()
    {
        var dialog = ScreenManager.Instance.ShowDialog(ConstantsDialog.SETTINGS);
        yield return dialog.Wait();
    }

    private IEnumerator GameMode00()
    {
        //Прелоадер
        yield return SplashScreenManager.Instance.ShowBlack();
        //yield return new WaitForSeconds(2.5f);

        //MyGameManager.Instance.LoadScene(ConstantsScene.GAME_MODE_00);
        ScreenManager.Instance.ShowScreen(ConstantsScreen.GAME_MODE_00);
    }

    private IEnumerator StartPlay()
    {
        //Прелоадер
        yield return SplashScreenManager.Instance.ShowBlack();
        //yield return new WaitForSeconds(2.5f);

        ScreenManager.Instance.ShowScreen(ConstantsScreen.GAME_SCREEN, Global.Instance.PROFILE.currentShip);
    }
    #endregion
}
