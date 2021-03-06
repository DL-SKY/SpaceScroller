﻿using UnityEngine;

public static class ConstantsGameSettings
{
    //public static float CELL_SIZE = 1.0f;           //Сторона вокселя игрового пространства (одна игровая клетка)
    //public static float BEYOND_BORDERS = 10.0f;     //Воксели за пределами Игрового поля, по которым можно перемещаться
    //public static float RAY_COEF = 1.1f;            //Длина луча для проверки столкновения
    //public static float TIME_ANIMATION = 1.0f;      //Скорость анимаций перемещения
    //public static int BASE_EQUIPMENT_SLOTS = 3;     //Базовое кол-во слотов оснащения в корабле

    public const float SPACETRACK_COUNT = 5.1f;            //[!]Нечетное!
    public const float SPACETRACK_WIDTH = 2.5f;

    public const int GAMEPLAY_DIFFICULTY_DEF = 1;          //1
    public const float GAMEPLAY_SPEED_DEF = 1.0f;          //2.5    
    //public const float GAMEPLAY_GENERATION_TIMER = 2.5f;   //1.0
    public static Vector2 GAMEPLAY_GENERATION_ROTATION = new Vector2(-25.0f, 25.0f);

}

public static class ConstantsTag
{
    public const string TAG_VOID = "Void";

    public const string TAG_PLAYER = "Player";
    public const string TAG_MISSILE_PLAYER = "MissilePlayer";
    public const string TAG_SHOT_PLAYER = "ShotPlayer";

    public const string TAG_SPACE_OBJECT = "SpaceObject";
    public const string TAG_SPACESHIP = "Spaceship";
    public const string TAG_MISSILE = "Missile";
    public const string TAG_SHOT = "Shot";

    public const string TAG_EQUIPMENT = "Equipment";
}

public static class ConstantsResourcesPath
{
    //Path
    public const string RESOURCES = "Assets/Resources/";
    public const string CONFIGS = "Configs/";
    public const string PREFABS = "Prefabs/";
    
    public const string PREF_SPACEOBJECTS = "Prefabs/Objects/";
    public const string PREF_SPACESHIPS = "Prefabs/Spaceships/";

    public const string SPLASHSCREEN = "Prefabs/UI/SplashScreens/";
    public const string SCREENS = "Prefabs/UI/Screens/";
    public const string DIALOGS = "Prefabs/UI/Dialogs/";
    public const string ELEMENTS_UI = "Prefabs/UI/Elements/";

    public const string SPRITES = "Sprites/";

    //File name
    public const string FILE_CONFIG = "Config";
    public const string FILE_SETTINGS = "Settings";
    public const string FILE_PROFILE = "Profile";
}

public static class ConstantsScene
{
    public const string MAIN_SCENE = "Main";
    public const string MAIN_MENU = "MainMenu";
    public const string GAME_MODE_00 = "GameMode00";

    public const string GAME_SCENE = "Game";
}

public static class ConstantsScreen
{
    public const string MAIN_MENU = "MainMenuScreen";
    public const string GAME_MODE_00 = "GameMode00Screen";

    public const string GAME_SCREEN = "GameScreen";
}

public static class ConstantsDialog
{
    public const string SETTINGS = "SettingsDialog";
}

public static class ConstantsColor
{
    public const string DARK_AMBER = "dark_amber";
    public const string TIZIANO = "tiziano";
    public const string DEAD_INDIGO = "dead_indigo";
    public const string AZURE_GRAY = "azure_gray";
    public const string AZURE_KRAOLA = "azure_kraola";
    public const string LIGHT_KRAOLA = "light_kraola";
}

public static class ConstantsResourcesType
{

}

public static class ConstantsResourcesSubtype
{

}

public static class ConstantsSkill
{

}

public static class ConstantsLanguage
{
    public const string RUSSIAN = "rus";
    public const string ENGLISH = "eng";
}

#region ENUM
//Тип объекта
public enum EnumSpaceObject
{
    Void,
    SpaceObject,

    Player,
    MissilePlayer,
    ShotPlayer,

    Spaceship,
    Missile,
    Shot,

    Equipment,
    Item,
}

//Тип оснащения
public enum EnumEquipmentType
{
    TestMissileLauncher,
}
#endregion



