using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[System.Serializable]
public class ShopSystem
{
    [SerializeField] private List<ShopSlot> _shopInventory;
    [SerializeField] private int _availableGold;
    [SerializeField] private float _buyMarkUp;
    [SerializeField] private float _sellMarkUp;

    public List<ShopSlot> ShopInventory => _shopInventory;

    public int AvailableGold => _availableGold;

    public float BuyMarkUp => _buyMarkUp;
    public float SellMarkUp => _sellMarkUp;

    public ShopSystem(int size, int gold, float buyMarkUp, float sellMarkUp)
    {
        _availableGold = gold;
        _buyMarkUp = buyMarkUp;
        _sellMarkUp = sellMarkUp;

        SetShopSize(size);
    }

    private void SetShopSize(int size)
    {
        _shopInventory = new List<ShopSlot>();

        for (int i = 0; i < size; i++)
        {
            ShopSlot newSlot = new ShopSlot();
            newSlot.AssignEquipSlot(); // Инициализация EquipSlot
            _shopInventory.Add(newSlot);

            //_shopInventory.Add(new ShopSlot());
        }
    }

    public void AddToShop(InventoryItemData data, int amount)
    {
        if (data.ItemType != ItemType.Equipment)
        {
            if (ContainsItem(data, out ShopSlot shopSlot))
            {
                shopSlot.AddToStack(amount);
                return;
            }

            var freeSlot = GetFreeSlot();
            freeSlot.AssignItem(data, amount);
        }
    }

    public void AddToShop(InventoryItemData data, ShopSlot slot, int amount)
    {
        if (data.ItemType != ItemType.Equipment)
        {
            if (ContainsItem(data, out ShopSlot shopSlot))
            {
                shopSlot.AddToStack(amount);
                return;
            }

            var freeSlot = GetFreeSlot();
            freeSlot.AssignItem(data, amount);
        }

        else
        {
            if (ContainsEquipItem(data, slot.EquipSlot.ItemTier, out ShopSlot shopSlot))
            {
                shopSlot.AddToStack(amount);
                return;
            }

            var freeSlot = GetFreeEquipSlot();

            if (freeSlot != null)
            {
                freeSlot.AssignEquipItem(data, slot, amount);

            }
        }
    }

    private ShopSlot GetFreeSlot()
    {
        var freeSlot = _shopInventory.FirstOrDefault(i => i.ItemData == null);

        if (freeSlot == null)
        {
            freeSlot = new ShopSlot();
            _shopInventory.Add(freeSlot);
        }

        return freeSlot;
    }

    private ShopSlot GetFreeEquipSlot()
    {
        var freeSlot = _shopInventory.FirstOrDefault(i => i.ItemData == null && i.EquipSlot != null && i.EquipSlot.ItemTier <= 0);

        if (freeSlot == null)
        {
            freeSlot = new ShopSlot();
            _shopInventory.Add(freeSlot);
        }

        return freeSlot;
    }

    public bool ContainsItem(InventoryItemData itemToAdd, out ShopSlot shopSlot)
    {
        //Debug.Log("21");
        shopSlot = _shopInventory.Find(i => i.ItemData == itemToAdd);
        return shopSlot != null;
    }

    public bool ContainsEquipItem(InventoryItemData itemToAdd, int itemTier, out ShopSlot shopSlot)
    {
        //Debug.Log("21");
        shopSlot = _shopInventory.Find(i => i.ItemData == itemToAdd && i.EquipSlot.ItemTier == itemTier);
        return shopSlot != null;
    }

    public void PurchaseItem(InventoryItemData data, int amount)
    {
        if (!ContainsItem(data, out ShopSlot slot))
            return;

        slot.RemoveFromStack(amount);
    }

    public void GainGold(int basketTotal)
    {
        _availableGold += basketTotal;
    }

    public void SellItem(InventoryItemData kvpKey, int kvpValue, int price)
    {
        AddToShop(kvpKey, kvpValue);
        ReduceGold(price);
    }

    private void ReduceGold(int price)
    {
        _availableGold -= price;
    }
}
