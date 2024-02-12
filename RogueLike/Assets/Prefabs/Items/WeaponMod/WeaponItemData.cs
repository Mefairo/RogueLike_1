using Assets.Scripts.Inventory.Item_Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon System/Weapon Item Mod")]
public class WeaponItemData : CraftItemData
{
    public DataWeaponMod WeaponMod;
}
