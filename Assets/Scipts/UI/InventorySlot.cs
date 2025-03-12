using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public WeaponInfo weaponInfo;

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}
