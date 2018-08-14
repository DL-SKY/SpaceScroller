using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    #region Variables
    public string ID { get { return id; } }
    [SerializeField]
    private string id;

    public int MaxUses { get { return maxUses; } }
    [SerializeField]
    private int maxUses;

    public int Uses { get { return uses; } }
    [SerializeField]
    private int uses;

    public float Coolldown { get { return cooldown; } }
    [SerializeField]
    private float cooldown;

    public EnumEquipmentType type;

    private EquipmentConfig config;
    private GameplayManager gameplay;
    #endregion

    #region Unity methods
    private void Awake()
    {
        gameplay = GameSceneController.Instance.GetGameplayManager();
    }
    #endregion

    #region Public methods
    public void Initialize(EquipmentConfig _config)
    {
        config = _config;
        ApplyType();

        id = config.id;
        maxUses = config.uses[config.level - 1];
        uses = maxUses;
        cooldown = config.cooldown[config.level - 1];
    }

    public void Use()
    {       
        switch (type)
        {
            case EnumEquipmentType.TestMissileLauncher:
                UsingTestMissileLauncher();
                uses++;
                break;

            default:
                UsingTestMissileLauncher();
                break;
        }

        uses--;
    }

    public string GetSlotImage()
    {
        return config.slotImage;
    }
    #endregion

    #region Private methods
    private void ApplyType()
    {
        switch (config.id)
        {
            case "testMissileLauncher":
                type = EnumEquipmentType.TestMissileLauncher;
                break;
        }
    }

    private void UsingTestMissileLauncher()
    {
        gameplay.CreateMissleAndShot(config.ammoPrefab, transform.position);
    }
    #endregion
}
