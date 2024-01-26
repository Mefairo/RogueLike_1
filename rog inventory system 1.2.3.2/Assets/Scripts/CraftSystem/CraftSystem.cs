using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[System.Serializable]
public class CraftSystem
{
    [SerializeField] private List<CraftSlot> _craftInventory;
    
    public List<CraftSlot> CraftInventory => _craftInventory;

    public CraftSystem(int size)
    {
        SetCraftSize(size);
    }

    private void SetCraftSize(int size)
    {
        _craftInventory = new List<CraftSlot>();

        for (int i = 0; i < size; i++)
        {
            _craftInventory.Add(new CraftSlot());
        }
    }

    public void AddToCraft(CraftItemData data)
    {
        if (ContainsItem(data, out CraftSlot craftSlot))
        {
            return;
        }

        var freeSlot = GetFreeSlot();
        freeSlot.AssignItem(data);
    }

    private CraftSlot GetFreeSlot()
    {
        var freeslot = _craftInventory.FirstOrDefault(i => i.ItemData == null);
        var freeslot1 = _craftInventory.FirstOrDefault(i => i.craftItemData == null);

        if (freeslot == null)
        {
            freeslot = new CraftSlot();
            freeslot1 = new CraftSlot();
            _craftInventory.Add(freeslot);
        }

        return freeslot;
    }

    public bool ContainsItem(CraftItemData itemToAdd, out CraftSlot craftSlot)
    {
        craftSlot = _craftInventory.Find(i => i.ItemData == itemToAdd);
        craftSlot = _craftInventory.Find(i => i.craftItemData == itemToAdd);

        return craftSlot != null;
    }
}
