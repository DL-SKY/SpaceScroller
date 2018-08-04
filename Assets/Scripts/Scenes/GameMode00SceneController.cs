using DllSky.Extensions;
using DllSky.Managers;
using DllSky.Patterns;
using DllSky.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode00SceneController : Singleton<GameMode00SceneController>
{
    #region Variables
    public bool isInit = false;
    
    [Header("Matrix")]
    public int lengthX;
    public int lengthY;
    public int lengthZ;
    public float percentBox;

    [Header("Counters")]
    public int countVoxels;
    public int countVoid;
    public int countBoxs;

    [Header("Game")]
    public bool isPlayerTurn = false;


    [SerializeField]
    private Transform space;
    #endregion

    #region Unity methods
    private void Start()
    {
        space = new GameObject("SPACE").transform;

        StartCoroutine(Initialize());
    }

    private void OnEnable()
    {
        //Подписываемся на события
        //EventManager.eventOnStartPlayerTurn += HandlerOnStartPlayerTurn;
        //EventManager.eventOnEndPlayerTurn += HandlerOnEndPlayerTurn;
    }

    private void OnDisable()
    {
        //Отписываемся от событий
        //EventManager.eventOnStartPlayerTurn -= HandlerOnStartPlayerTurn;
        //EventManager.eventOnEndPlayerTurn -= HandlerOnEndPlayerTurn;
    }
    #endregion

    #region Public methods
    #endregion

    #region Private methods    
    #endregion

    #region Coroutines
    private IEnumerator Initialize()
    {
        //TODO: testing
        //GenerateGameBoard();
        //Кадр для применения настроек создаваемых объектов на сцене
        yield return null;
        //------------------------------
        //isInit = true;
        //ScreenManager.Instance.ShowScreen(ConstantsScreen.GAME_MODE_00);
    }    
    #endregion

    #region Menu
    [ContextMenu("Generate Game Board")]
    private void ContextMenuGenerate()
    {
        
    }
    #endregion
}
