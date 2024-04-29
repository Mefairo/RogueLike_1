using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopSlot : ItemSlot
{
    public ShopSlot()
    {
        ClearSlot();
    }

    //public void UpdateShopSlot(ItemPrefabData data, int amount)
    //{
    //    _equipSlot = data.EquipSlot;
    //    _equipSlot.ItemData = data.EquipSlot.ItemData;
    //    _equipSlot.ItemTier = data.EquipSlot.ItemTier;

    //    itemData = data.EquipSlot.ItemData;
    //    stackSize = amount;
    //    _itemID = itemData.ID;
    //}
}
