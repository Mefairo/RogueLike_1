using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System;
using static UnityEngine.Rendering.DebugUI;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> inventorySlots;
    [SerializeField] private int _gold;

    public event UnityAction<int> OnChangeGold;

    public int Gold
    {
        get => _gold;
        set
        {
            _gold = value;
            OnChangeGold?.Invoke(value);
        }
    }

    public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => InventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size)
    {
        _gold = 0;

        CreateInventory(size);
    }

    public InventorySystem(int size, int gold)
    {
        _gold = gold;

        CreateInventory(size);
    }

    private void CreateInventory(int size)
    {
        inventorySlots = new List<InventorySlot>(size);

        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }

    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {
        //Debug.Log("16");
        if (ContainsItem(itemToAdd, out List<InventorySlot> invSlot))  //Существует ли предмет в инвентаре
        {
            //Debug.Log("17");
            foreach (var slot in invSlot)
            {
                if (slot.EnoughRoomLeftInStack(amountToAdd))
                {
                    //Debug.Log("18");
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }

        }
        if (HasFreeSlot(out InventorySlot freeSlot))  // Получает первый доступный слот
        {
            //Debug.Log("19");
            if (freeSlot.EnoughRoomLeftInStack(amountToAdd))
            {
                //Debug.Log("20");
                freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
                OnInventorySlotChanged?.Invoke(freeSlot);
                return true;
            }
        }

        return false;

    }

    public bool ContainsItem(InventoryItemData itemToAdd, out List<InventorySlot> invSlot)
    {
        //Debug.Log("21");
        invSlot = InventorySlots.Where(i => i.ItemData == itemToAdd).ToList();

        return invSlot == null ? false : true;
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        //Debug.Log("22");
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null);
        return freeSlot == null ? false : true;
    }

    internal bool CheckInventoryRemaining(Dictionary<InventoryItemData, int> shoppingCart)
    {
        var clonedSystem = new InventorySystem(this.InventorySize);

        for (int i = 0; i < InventorySize; i++)
        {
            clonedSystem.InventorySlots[i].AssignItem(this.InventorySlots[i].ItemData, this.InventorySlots[i].StackSize);
        }

        foreach (var kvp in shoppingCart)
        {
            for (int i = 0; i < kvp.Value; i++)
            {
                if (!clonedSystem.AddToInventory(kvp.Key, 1))
                    return false;
            }
        }

        return true;
    }

    public void SpendGold(int price)
    {
        Gold -= price;
        OnChangeGold?.Invoke(Gold);
    }

    public Dictionary<InventoryItemData, int> GetAllItemHeld()
    {
        var distinctItems = new Dictionary<InventoryItemData, int>();

        foreach (var item in inventorySlots)
        {
            if (item.ItemData == null)
                continue;

            if (!distinctItems.ContainsKey(item.ItemData))
                distinctItems.Add(item.ItemData, item.StackSize);

            else
                distinctItems[item.ItemData] += item.StackSize;
        }

        return distinctItems;
    }

    public void GainGold(int price)
    {
        Gold += price;
        OnChangeGold?.Invoke(Gold);
    }

    public void RemoveItemsFromInventory(InventoryItemData data, int amount)
    {
        if (ContainsItem(data, out List<InventorySlot> invSlot))
        {
            foreach (var slot in invSlot)
            {
                var stackSize = slot.StackSize;

                if (stackSize > amount)
                {
                    slot.RemoveFromStack(amount);
                    OnInventorySlotChanged?.Invoke(slot);
                    return;
                }

                else
                {
                    slot.RemoveFromStack(stackSize);
                    amount -= stackSize;
                    OnInventorySlotChanged?.Invoke(slot);
                }


            }

        }
    }

}
