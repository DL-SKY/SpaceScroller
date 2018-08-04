using DllSky.Extensions;
using DllSky.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    #region Variables
    [Header("Gameplay")]
    public bool pause = false;
    public int difficulty = 1;
    public float playerSpeed = 1.0f;    
    public float timerToGeneration = 2.5f;

    [Header("Space")]
    public Transform space;
    public List<Vector3> spawners = new List<Vector3>();
    public List<Vector3> destroyers = new List<Vector3>();

    [Header("Player")]
    public PlayerController player;    

    [Header("Objects")]
    public List<BaseObject> objects = new List<BaseObject>();

    [Header("Background")]
    public Transform background;

    private GameSceneController sceneController;
    #endregion

    #region Unity methods
    private void Awake()
    {
        sceneController = GameSceneController.Instance;
    }

    private void OnEnable()
    {
        pause = false;
    }

    private void OnDisable()
    {
        pause = true;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void FixedUpdate()
    {
        if (pause || spawners.Count < 1 || destroyers.Count < 1)
            return;

        for (int i = objects.Count - 1; i >= 0; i--)
        {
            if (objects[i] == null)
            {
                objects.RemoveAt(i);
                continue;
            }

            if (objects[i].transform.position.y > spawners[0].y || objects[i].transform.position.y < destroyers[0].y)
            {
                Destroy(objects[i].gameObject);
            }
            else
            {
                objects[i].RotateToFixedUpdate();
                objects[i].MoveToFixedUpdate();
            }
        }
    }
    #endregion

    #region Gizmos
    private void OnDrawGizmos()
    {
        DrawSpawners();
        DrawDestroyers();

        DrawObjects();
        DrawPlayer();
    }

    private void DrawSpawners()
    {
        foreach (var point in spawners)
            Gizmos.DrawIcon(point, "plain-arrow");
    }

    private void DrawDestroyers()
    {
        foreach (var point in destroyers)
            Gizmos.DrawIcon(point, "hazard-sign");
    }

    private void DrawObjects()
    {
        foreach (var obj in objects)
        {
            if (obj == null)
                continue;

            /*if (obj.tag == ConstantsTag.TAG_SPACE_OBJECT)
                Gizmos.DrawIcon(obj.transform.position, "3d-meeple");
            else if (obj.tag == ConstantsTag.TAG_SPACESHIP)
                Gizmos.DrawIcon(obj.transform.position, "3d-meeple");*/
            switch (obj.tag)
            {
                case ConstantsTag.TAG_SPACE_OBJECT:
                    Gizmos.DrawIcon(obj.transform.position, "cube");
                    break;
                case ConstantsTag.TAG_SPACESHIP:
                    Gizmos.DrawIcon(obj.transform.position, "spaceship");
                    break;
            }
        }
    }

    private void DrawPlayer()
    {
        if (player)
            Gizmos.DrawIcon(player.transform.position, "3d-meeple");
    }
    #endregion

    #region Public methods
    public void Initialize(string _spaceship)
    {
        CheckSpaceTransform();
        CheckBackgroundTransform();

        CreateSpawnerPoints();
        CreateDestroyerPoints();
        CreateSpace(_spaceship);

        ApplyGameplayParameters();

        StartGame();
    }

    public void CreateMissleAndShot(string _id, Vector3 _position)
    {
        //TEST
        if (_id == "TEST")
            _id = "TestMissile";


        var spaceObject = CreateObject(_id);

        spaceObject.transform.position = _position;
        //spaceObject.speed = objSpeed;
        spaceObject.dontRotate = true;
        spaceObject.Initialize(null);
    }
    #endregion

    #region Private methods   
    private void CheckSpaceTransform()
    {
        if (space == null)
        {
            var obj = new GameObject();
            space = obj.transform;
        }

        space.position = Vector3.zero;
        space.name = "SPACE";
    }

    private void CheckBackgroundTransform()
    {
        if (background == null)
        {
            var obj = new GameObject();
            background = obj.transform;
        }

        background.position = Vector3.zero;
        background.name = "BACKGROUND";
    }

    private void CreateSpawnerPoints()
    {
        spawners.Clear();

        var count = (int)ConstantsGameSettings.SPACETRACK_COUNT;
        var cam = CameraGame.Instance;
        var y = (cam.transform.position.y + cam.GetCamera().orthographicSize) + cam.transform.position.y / 2.0f;

        for (int i = 0; i < count; i++)
        {
            float x = ConstantsGameSettings.SPACETRACK_WIDTH * (i - (count - 1) / 2);
            Vector3 point = new Vector3(x, y, 0.0f);
            spawners.Add(point);
        }
    }

    private void CreateDestroyerPoints()
    {
        destroyers.Clear();

        var count = (int)ConstantsGameSettings.SPACETRACK_COUNT;
        var cam = CameraGame.Instance;
        var y = - cam.GetCamera().orthographicSize + cam.transform.position.y / 2.0f;

        for (int i = 0; i < count; i++)
        {
            float x = ConstantsGameSettings.SPACETRACK_WIDTH * (i - (count - 1) / 2);
            Vector3 point = new Vector3(x, y, 0.0f);
            destroyers.Add(point);
        }
    }

    private void CreateSpace(string _spaceship)
    {
        space.DestroyChildren();
        //...
        objects.Clear();
        CreatePlayer(_spaceship);
    }

    private void CreatePlayer(string _spaceship)
    {
        var inst = Instantiate(ResourcesManager.LoadPrefab(ConstantsResourcesPath.PREF_SPACESHIPS, _spaceship), space);
        player = inst.GetComponent<PlayerController>();
        player.Initialize(null);

        //TEST
        player.GetPlayerSpaceship().AddEquipment("testMissileLauncher");
    }

    private void ApplyGameplayParameters()
    {
        difficulty = ConstantsGameSettings.GAMEPLAY_DIFFICULTY_DEF;
        playerSpeed = ConstantsGameSettings.GAMEPLAY_SPEED_DEF;
        timerToGeneration = 1.0f; //ConstantsGameSettings.GAMEPLAY_GENERATION_TIMER;
    }

    private void StartGame()
    {
        StartCoroutine(SpawnerTimer());
    }

    private int GetCountCreateObjects()
    {
        int result = 1;

        //...

        return result;
    }

    private string GetNameCreateObject()
    {
        string result = "";

        //...
        result = "TestBlock";
        //...

        return result;
    }

    private BaseObject CreateObject(string _id)
    {
        var inst = Instantiate(ResourcesManager.LoadPrefab(ConstantsResourcesPath.PREF_SPACEOBJECTS, _id), space);
        inst.transform.SetParent(space);
        var spaceObject = inst.GetComponent<BaseObject>();
        objects.Add(spaceObject);

        return spaceObject;
    }

    private void OnGenerate()
    {
        List<int> usedSpawner = new List<int>();        //Список использованных точек спауна за текущую генерацию
        int countObjects = GetCountCreateObjects();     //Кол-во генерируемых объектов для текущей генерации

        for (int i = 0; i < countObjects; i++)
        {
            //TODO: разные объекты
            var objectName = GetNameCreateObject();
            var spaceObject = CreateObject(objectName);

            var spawnIndex = Random.Range(0, spawners.Count);
            if ( usedSpawner.Exists(x => x == spawnIndex) )
                for (int j = 0; j < spawners.Count; j++)
                {
                    spawnIndex++;
                    if (spawnIndex >= spawners.Count)
                        spawnIndex = 0;

                    if (!usedSpawner.Exists(x => x == spawnIndex))
                        break;
                }
            usedSpawner.Add(spawnIndex);

            //TODO: добавить зависимость скорости от сложности
            var objSpeed = Random.Range(playerSpeed, playerSpeed * 2.5f);  // = playerSpeed;
            var objSpeedRotate = Random.Range(ConstantsGameSettings.GAMEPLAY_GENERATION_ROTATION.x, ConstantsGameSettings.GAMEPLAY_GENERATION_ROTATION.y);

            spaceObject.transform.position = spawners[spawnIndex];
            spaceObject.speed = objSpeed;
            spaceObject.speedRotate = objSpeedRotate;
            spaceObject.Initialize(null);
        }        
    }
    #endregion

    #region Coroutines
    private IEnumerator SpawnerTimer()
    {
        while (true)
        {
            if (pause)
                while (pause)
                    yield return null;

            //UpdateSpeedAndDifficulty();            
            OnGenerate();

            timerToGeneration = 1.0f / (playerSpeed * 0.4f);
            yield return new WaitForSeconds(timerToGeneration);
        }        
    }
    #endregion
}
