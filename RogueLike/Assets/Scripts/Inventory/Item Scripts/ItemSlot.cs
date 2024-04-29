using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSlot : ISerializationCallbackReceiver
{

    [SerializeField] protected InventoryItemData itemData;
    [SerializeField] protected int stackSize;
    [SerializeField] protected int _itemID = -1;
    [Header("Equipment Parameters")]
    [SerializeField] protected EquipSlot _equipSlot;

    public InventoryItemData ItemData => itemData;
    public EquipSlot EquipSlot => _equipSlot;
    public int StackSize => stackSize;

    public void ClearSlot()
    {
        itemData = null;
        _itemID = -1;
        stackSize = -1;
        _equipSlot = null;

        if (_equipSlot != null)
            _equipSlot.ItemTier = -1;
    }

    public void AssignEquipSlot()
    {
        _equipSlot = new EquipSlot();
    }

    public void AssignEquipItem(InventorySlot invSlot)
    {
        if (itemData == invSlot.ItemData)
        {
            //Debug.Log("4");
            if (_equipSlot.ItemTier != invSlot.EquipSlot.ItemTier)
            {
                itemData = invSlot.itemData;
                _itemID = itemData.ID;

                _equipSlot = invSlot.EquipSlot;
                if (_equipSlot != null)
                {
                    _equipSlot.ItemData = invSlot.EquipSlot.ItemData;
                    _equipSlot.ItemTier = invSlot.EquipSlot.ItemTier;
                }
            }

            else
            {
                AddToStack(invSlot.stackSize);
            }

        }

        else
        {
            //Debug.Log("5");
            itemData = invSlot.itemData;
            _itemID = itemData.ID;
            stackSize = 0;

            _equipSlot = invSlot.EquipSlot;
            if (_equipSlot != null)
            {
                _equipSlot.ItemData = invSlot.EquipSlot.ItemData;
                _equipSlot.ItemTier = invSlot.EquipSlot.ItemTier;
            }

            AddToStack(invSlot.stackSize);
        }
    }

    public void AssignEquipItem(InventoryItemData data, ShopSlot slot, int amount)
    {
        if (itemData == data && _equipSlot == slot.EquipSlot)
            AddToStack(amount);

        else
        {
            itemData = data;
            _itemID = data.ID;
            stackSize = 0;

            if (_equipSlot != null)
            {
                _equipSlot.ItemData = data;
                _equipSlot.ItemTier = slot.EquipSlot.ItemTier;
            }

            else
                Debug.Log("eq null");

            AddToStack(amount);
        }


    }

    public void AssignItem(InventorySlot invSlot)
    {
        if (itemData == invSlot.ItemData)
            AddToStack(invSlot.stackSize);

        else
        {
            //Debug.Log("5");
            itemData = invSlot.itemData;
            _itemID = itemData.ID;
            stackSize = 0;

            _equipSlot = invSlot.EquipSlot;
            if (_equipSlot != null)
            {
                _equipSlot.ItemData = invSlot.EquipSlot.ItemData;
                _equipSlot.ItemTier = invSlot.EquipSlot.ItemTier;
            }

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
        if (stackSize <= 0)
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
