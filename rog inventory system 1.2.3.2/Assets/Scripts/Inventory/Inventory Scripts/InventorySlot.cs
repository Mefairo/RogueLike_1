using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class InventorySlot : ItemSlot
{
    public InventorySlot(InventoryItemData source, int amount)
    {
        //Debug.Log("1");
        itemData = source;
        _itemID = itemData.ID;
        stackSize = amount;
    }

    public InventorySlot()
    {
        //Debug.Log("2");
        ClearSlot();
    }





    public void UpdateInventorySlot(InventoryItemData data, int amount)
    {
        //Debug.Log("6");
        itemData = data;
        stackSize = amount;
        _itemID = itemData.ID;
    }

    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        //Debug.Log("7");
        amountRemaining = ItemData.MaxStackSize - stackSize;
        return EnoughRoomLeftInStack(amountToAdd);

    }

    public bool EnoughRoomLeftInStack(int amountToAdd)
    {
        if (itemData == null || itemData != null && stackSize + amountToAdd <= itemData.MaxStackSize)
        {
            //Debug.Log("8");
            return true;
        }
        else
        {
            //Debug.Log("9");
            return false;
        }
    }



    public bool SplitStack(out InventorySlot splitStack)
    {
        if (stackSize <= 1)
        {
            //Debug.Log("12");
            splitStack = null;
            return false;
        }

        //Debug.Log("13");
        int halfStack = Mathf.RoundToInt(stackSize / 2);
        RemoveFromStack(halfStack);

        //Debug.Log("14");
        splitStack = new InventorySlot(itemData, halfStack);
        return true;
    }
}
