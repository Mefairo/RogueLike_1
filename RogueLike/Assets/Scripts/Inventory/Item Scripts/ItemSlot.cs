using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSlot : ISerializationCallbackReceiver
{

    [SerializeField] protected InventoryItemData itemData;
    [SerializeField] protected int stackSize;
    [SerializeField] protected int _itemID = -1;

    public InventoryItemData ItemData => itemData;
    public int StackSize => stackSize;

    public void ClearSlot()
    {
        //Debug.Log("3");
        itemData = null;
        _itemID = -1;
        stackSize = -1;
    }


    public void AssignItem(InventorySlot invSlot)
    {
        if (itemData == invSlot.ItemData)
        {
            //Debug.Log("4");
            AddToStack(invSlot.stackSize);
        }

        else
        {
            //Debug.Log("5");
            itemData = invSlot.itemData;
            _itemID = itemData.ID;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
    }

    public void AssignItem(InventoryItemData data, int amount)
    {
        if (itemData == data)
            AddToStack(amount);

        else
        {
            itemData = data;
            _itemID = data.ID;
            stackSize = 0;
            AddToStack(amount);
        }
    }

    public void AddToStack(int amount)
    {
        //Debug.Log("10");
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        //Debug.Log("11");
        stackSize -= amount;
        if(stackSize <= 0)
            ClearSlot();
    }

    public void SwapStack(int amount)
    {
        //Debug.Log("swap");
        stackSize = amount;
    }


    public void OnBeforeSerialize()
    {

    }

    public void OnAfterDeserialize()
    {
        if (_itemID == -1)
            return;

        var db = Resources.Load<Database>("Database");
        itemData = db.GetItem(_itemID);
    }


}
