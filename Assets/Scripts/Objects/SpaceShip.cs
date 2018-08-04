using DllSky.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : BaseObject
{
    #region Variables
    [Header("Main Parameters (SpaceShip)")]
    public float shieldPoints = 1.0f;

    [Header("Equipments")]
    public List<Equipment> equipments = new List<Equipment>();    //TODO
    #endregion

    #region Unity methods
    protected override void Awake()
    {
        base.Awake();

        dontRotate = true;
        //tag = ConstantsTag.TAG_SPACESHIP;
    }

    /*protected override void OnTriggerEnter2D(Collider2D _other)
    {
        string _tag = _other.tag;
        BaseObject _otherObject = _other.GetComponent<BaseObject>();

        //Игрок и его снаряды
        if (tag == ConstantsTag.TAG_PLAYER && (_tag == ConstantsTag.TAG_MISSILE_PLAYER || _tag == ConstantsTag.TAG_SHOT_PLAYER))
        {

        }
        //Противники и их снаряды
        else if (tag == ConstantsTag.TAG_SPACESHIP && (_tag == ConstantsTag.TAG_MISSILE || _tag == ConstantsTag.TAG_SHOT))
        {

        }

        //Иначе - получаем повреждения
        else
        {
            GetSelfDamage(_otherObject.damage);
        }
    }*/
    #endregion

    #region Public methods
    public override void Initialize(object _data)
    {
        //TODO

        ApplyParameters();
    }

    public override void GetSelfDamage(float _damage)
    {
        if (isImmortal)
            return;

        if (shieldPoints > 0)   //Прочность щитов
        {
            shieldPoints -= _damage;

            if (shieldPoints <= 0)
            {
                shieldPoints = 0;
                DestroyShields();
            }
        }
        else                    //Прочность корпуса
        {
            hitPoints -= _damage;

            if (hitPoints <= 0)
                SelfDestroy();
        }   
    }

    public void AddEquipment(string _id)
    {
        var config = Global.Instance.CONFIGS.equipments.Find(x => x.id == _id);
        if (config == null)
        {
            Debug.LogWarning("<color=#FF0000>[WARNING]</color> method AddEquipment(" + _id + ") - The equipment was not found in the configuration file!");
            return;
        }

        var newEquipment = gameObject.AddComponent<Equipment>();
        newEquipment.Initialize(config);

        equipments.Add(newEquipment);

        //Вызываем событие изменения слотов
        if (tag == ConstantsTag.TAG_PLAYER)
            EventManager.CallOnChangePlayerSlots();
    }

    public void UseEquipment(int _index)
    {

    }

    public void UseEquipment(string _id)
    {
        var equipment = equipments.Find(x => x.ID == _id);
        if (equipment == null)
        {
            Debug.LogWarning("<color=#FF0000>[WARNING]</color> method UseEquipment(" + _id + ") - The equipment was not found in the list!");
            return;
        }

        equipment.Use();
        //Вызываем событие изменения слотов
        if (tag == ConstantsTag.TAG_PLAYER)
            EventManager.CallOnChangePlayerSlots();

        //Если кончились заряды
        if (equipment.Uses <= 0)
            DestroyEquipment(_id);
    }
    #endregion

    #region Protected methods
    protected override void SelfDestroy()
    {
        if (tag == ConstantsTag.TAG_SPACESHIP)
            Destroy(gameObject);
    }
    #endregion

    #region Private methods
    private void ApplyParameters()
    {
        //TODO
    }

    private void CreateShields()
    {
        //Эффект появления щитов
    }
    
    private void DestroyShields()
    {
        //Эффект уничтожения щита
    }

    private void DestroyEquipment(string _id)
    {
        var equipment = equipments.Find(x => x.ID == _id);
        if (equipment == null)
        {
            Debug.LogWarning("<color=#FF0000>[WARNING]</color> method DestroyEquipment(" + _id + ") - The equipment was not found in the list!");
            return;
        }

        equipments.Remove(equipment);
        Destroy(equipment);

        //Вызываем событие изменения слотов
        if (tag == ConstantsTag.TAG_PLAYER)
            EventManager.CallOnChangePlayerSlots();
    }
    #endregion
}
