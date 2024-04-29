using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipSlot
{
    public InventoryItemData ItemData;
    public int ItemTier;

    public void ClearInfo()
    {
        //ItemData = null;
        ItemTier = -1;
    }
}
