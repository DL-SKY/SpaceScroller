using DllSky.Patterns;
using DllSky.Utility;
using System.Collections;
using UnityEngine;

public class GameSceneController : Singleton<GameSceneController>
{
    #region Variables
    [Header("Main")]
    public bool isInit = false;

    private CameraGame cameraGame;
    private GameplayManager gameplay;
    #endregion

    #region Unity methods
    private void Start()
    {
        //StartCoroutine(InitializeCoroutine());
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
    public GameplayManager GetGameplayManager()
    {
        return gameplay;
    }

    public void Initialize(string _spaceship)
    {
        StartCoroutine(InitializeCoroutine(_spaceship));
    }
    #endregion

    #region Private methods
    private void CameraPreparation()
    {
        cameraGame.Resize(ConstantsGameSettings.SPACETRACK_WIDTH, ConstantsGameSettings.SPACETRACK_COUNT);
        cameraGame.Reposition();
    }   
    #endregion

    #region Coroutines
    private IEnumerator InitializeCoroutine(string _spaceship)
    {
        cameraGame = CameraGame.Instance;
        gameplay = FindObjectOfType<GameplayManager>();

        CameraPreparation();
        gameplay.Initialize(_spaceship);     

        //Кадр для применения настроек создаваемых объектов на сцене
        yield return null;
        //------------------------------
        isInit = true;
    }
    #endregion
}
