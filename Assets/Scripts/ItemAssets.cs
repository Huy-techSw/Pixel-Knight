//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ItemAssets : MonoBehaviour
//{
//    [SerializeField] WeaponData whip;
//    [SerializeField] WeaponData axe;
//    [SerializeField] WeaponData bible;
//    [SerializeField] WeaponData magicWand;
//    [SerializeField] WeaponData lightning;
//    [SerializeField] WeaponData fireWand;

//    [SerializeField] AccessoryData armor;
//    [SerializeField] AccessoryData clover;
//    [SerializeField] AccessoryData crown;
//    [SerializeField] AccessoryData emptyTome;
//    [SerializeField] AccessoryData spinach;
//    [SerializeField] AccessoryData wings;

//    static ItemAssets instance;

//    void Awake()
//    {
//        instance = this;
//    }

//    public static ItemAssets GetInstance()
//    {
//        return instance;
//    }

//    public WeaponData GetWeaponData(WeaponData.WeaponType weaponType)
//    {
//        switch (weaponType)
//        {
//            default:
//            case WeaponData.WeaponType.Axe:
//                return axe;
//            case WeaponData.WeaponType.Bible:
//                return bible;
//            case WeaponData.WeaponType.Lightning:
//                return lightning;
//            case WeaponData.WeaponType.MagicWand:
//                return magicWand;
//            case WeaponData.WeaponType.FireWand:
//                return fireWand;
//            case WeaponData.WeaponType.Whip:
//                return whip;
//        }
//    }

//    public AccessoryData GetAccessoryData(AccessoryData.AccessoryType type)
//    {
//        switch (type)
//        {
//            default:
//            case AccessoryData.AccessoryType.Armor:
//                return armor;
//            case AccessoryData.AccessoryType.Clover:
//                return clover;
//            case AccessoryData.AccessoryType.Crown:
//                return crown;
//            case AccessoryData.AccessoryType.EmptyTome:
//                return emptyTome;
//            case AccessoryData.AccessoryType.Spinach:
//                return spinach;
//            case AccessoryData.AccessoryType.Wings:
//                return wings;
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ItemAssets : MonoBehaviour
{
    [SerializeField] WeaponData whip;
    [SerializeField] WeaponData axe;
    [SerializeField] WeaponData bible;
    [SerializeField] WeaponData magicWand;
    [SerializeField] WeaponData lightning;
    [SerializeField] WeaponData fireWand;

    [SerializeField] AccessoryData armor;
    [SerializeField] AccessoryData clover;
    [SerializeField] AccessoryData crown;
    [SerializeField] AccessoryData emptyTome;
    [SerializeField] AccessoryData spinach;
    [SerializeField] AccessoryData wings;

    static ItemAssets instance;

    private int wordId;
    private string changeStatusBaseUrl = "http://localhost:8080/api/pru/word/";

    void Awake()
    {
        instance = this;
    }

    public static ItemAssets GetInstance()
    {
        return instance;
    }

    public void SetWordId(int id)
    {
        wordId = id;
    }

    public WeaponData GetWeaponData(WeaponData.WeaponType weaponType)
    {
        WeaponData selectedWeapon = null;

        switch (weaponType)
        {
            default:
            case WeaponData.WeaponType.Axe:
                selectedWeapon = axe;
                break;
            case WeaponData.WeaponType.Bible:
                selectedWeapon = bible;
                break;
            case WeaponData.WeaponType.Lightning:
                selectedWeapon = lightning;
                break;
            case WeaponData.WeaponType.MagicWand:
                selectedWeapon = magicWand;
                break;
            case WeaponData.WeaponType.FireWand:
                selectedWeapon = fireWand;
                break;
            case WeaponData.WeaponType.Whip:
                selectedWeapon = whip;
                break;
        }

        if (selectedWeapon != null)
        {
            StartCoroutine(ChangeStatus(wordId));
        }

        return selectedWeapon;
    }

    public AccessoryData GetAccessoryData(AccessoryData.AccessoryType type)
    {
        AccessoryData selectedAccessory = null;

        switch (type)
        {
            default:
            case AccessoryData.AccessoryType.Armor:
                selectedAccessory = armor;
                break;
            case AccessoryData.AccessoryType.Clover:
                selectedAccessory = clover;
                break;
            case AccessoryData.AccessoryType.Crown:
                selectedAccessory = crown;
                break;
            case AccessoryData.AccessoryType.EmptyTome:
                selectedAccessory = emptyTome;
                break;
            case AccessoryData.AccessoryType.Spinach:
                selectedAccessory = spinach;
                break;
            case AccessoryData.AccessoryType.Wings:
                selectedAccessory = wings;
                break;
        }

        if (selectedAccessory != null)
        {
            StartCoroutine(ChangeStatus(wordId));
        }

        return selectedAccessory;
    }

    IEnumerator ChangeStatus(int id)
    {
        string changeStatusUrl = changeStatusBaseUrl + id.ToString();

        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(changeStatusUrl, ""))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Debug.Log("Status changed successfully for item with ID: " + id);
            }
        }
    }
}
