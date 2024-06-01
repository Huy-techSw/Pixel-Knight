using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessory : MonoBehaviour
{
    [SerializeField] AccessoryData accessoryData;
    Sprite accessorySprite;
    int level = 1;

    void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        accessorySprite = accessoryData.GetSprite();
    }

    public AccessoryData.AccessoryType GetAccessoryType()
    {
        return accessoryData.GetAccessoryType();
    }

    public Sprite GetSprite()
    {
        return accessorySprite;
    }

    public int GetLevel()
    {
        return level;
    }

    public void IncreaseLevel()
    {
        level++;
        ApplyEffect();
    }

    public void ApplyEffect()
    {
        switch (accessoryData.GetAccessoryType())
        {
            case AccessoryData.AccessoryType.Spinach:
                Player.GetInstance().IncreaseAttackPower(10);
                break;

            case AccessoryData.AccessoryType.Crown:
                Player.GetInstance().IncreaseExpAdditional(10);
                break;

            case AccessoryData.AccessoryType.Clover:
                Player.GetInstance().IncreaseLuck(20);
                break;

            case AccessoryData.AccessoryType.Wings:
                Player.GetInstance().IncreaseSpeed(10);
                break;

            case AccessoryData.AccessoryType.Armor:
                Player.GetInstance().IncreaseDefencePower(10);
                break;

            case AccessoryData.AccessoryType.EmptyTome:
                Player.GetInstance().DecreaseAttackSpeed(8);
                break;
        }
    }
}
