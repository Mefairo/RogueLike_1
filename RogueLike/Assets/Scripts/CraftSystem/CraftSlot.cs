using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CraftSlot : ItemSlot
{
    public CraftItemData craftItemData;

    public CraftSlot(CraftItemData itemData)
    {
        craftItemData = itemData;
    }

    public CraftSlot()
    {
        ClearSlot();
    }

    public void AssignItem(InventoryItemData data)
    {
        if (itemData == data)
            return;
            //AddToStack(amount);

        else
        {
            itemData = data;
            craftItemData = (CraftItemData)data;
            _itemID = data.ID;
            //stackSize = 0;
            //AddToStack(amount);
        }
    }

}
