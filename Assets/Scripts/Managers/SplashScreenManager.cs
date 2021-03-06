﻿using DllSky.Patterns;
using DllSky.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenManager : Singleton<SplashScreenManager>
{
    #region Variables
    public Transform parent;

    private GameObject splashScreen;    
    #endregion

    #region Unity methods
    private void Awake()
    {
        if (!parent)
            parent = transform;
    }

    void OnGUI()
    {
        //float fps = 1.0f / Time.unscaledDeltaTime;
        //GUILayout.Label("FPS = " + fps);
    }
    #endregion

    #region Public methods
    public void HideSplashScreenImmediately()
    {
        DialogController controller = splashScreen.GetComponent<DialogController>();
        controller.CloseSplashScreenImmediately();
    }
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    public IEnumerator ShowStartingGame()
    {
        if (splashScreen)
            Destroy(splashScreen);

        splashScreen = Instantiate(ResourcesManager.LoadPrefab(ConstantsResourcesPath.SPLASHSCREEN, "StartingGame"), parent);
        splashScreen.transform.SetAsLastSibling();

        yield return null;
    }

    public IEnumerator ShowBlack()
    {
        if (splashScreen)
            Destroy(splashScreen);

        splashScreen = Instantiate(ResourcesManager.LoadPrefab(ConstantsResourcesPath.SPLASHSCREEN, "ScreenBlack"), parent);
        splashScreen.transform.SetAsLastSibling();

        yield return splashScreen.GetComponent<DialogController>().WaitShowSplashScreen();
    }

    public IEnumerator HideSplashScreen()
    {
        if (!splashScreen)
            yield break;

        DialogController controller = splashScreen.GetComponent<DialogController>();        

        if (splashScreen.name.Contains("StartingGame"))
        {
            controller.CloseSplashScreenImmediately();
            yield return controller.Wait();
            yield break;
        }

        controller.CloseSplashScreen();
        yield return controller.Wait();
    }

    /*public IEnumerator HideSplashScreenImmediately()
    {
        DialogController controller = splashScreen.GetComponent<DialogController>();

        controller.CloseSplashScreenImmediately();
        yield return controller.Wait();
    }*/
    #endregion
}
