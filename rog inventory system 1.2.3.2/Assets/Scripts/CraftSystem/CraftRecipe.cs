using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CraftRecipe
{
    [SerializeField] private List<InventoryItemData> _requiredItems;
    [SerializeField] private List<int> _amountResources;
    [SerializeField] private List<ItemCraft> _craftPrefab;

    [SerializeField] private InventoryItemData _craftedItem;


    public List<InventoryItemData> RequiredItems => _requiredItems;
    public List<int> AmountResources => _amountResources;
    public List<ItemCraft> CraftPrefab => _craftPrefab;
    public InventoryItemData CraftedItem => _craftedItem;

}
