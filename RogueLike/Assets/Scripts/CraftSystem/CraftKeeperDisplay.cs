using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class CraftKeeperDisplay : MonoBehaviour
{
    [SerializeField] private CraftSlotUI _craftSlotPrefab;
    [SerializeField] private Button _craftButton;

    private CraftSlotUI _selectedCraftSlotUI;
    private ItemCraft _selectedRequiredItem;

    [Header("Item Preview Section")]
    [SerializeField] private Image _itemPreviewSprite;
    //[SerializeField] private Image _itemPreviewBackground;
    [SerializeField] private TextMeshProUGUI _itemPreviewName;
    [SerializeField] private TextMeshProUGUI _itemPreviewDescription;

    [SerializeField] private GameObject _itemListContentPanel;
    [SerializeField] private GameObject _craftingCartContentPanel;

    private CraftSystem _craftSystem;
    private PlayerInventoryHolder _playerInventoryHolder;

    private Dictionary<CraftItemData, ItemCraft> _craftingCartUI = new Dictionary<CraftItemData, ItemCraft>();

    [SerializeField] private Transform pointCraftItem;

    public void DisplayCraftWindow(CraftSystem craftSystem, PlayerInventoryHolder playerInventoryHolder)
    {
        _craftSystem = craftSystem;
        _playerInventoryHolder = playerInventoryHolder;

        RefreshDisplay();
    }

    private void RefreshDisplay()
    {
        if (_craftButton != null)
        {
            _craftButton.onClick.RemoveAllListeners();
            _craftButton.onClick.AddListener(CraftItemWrapper);
        }

        ClearSlot();
        ClearResourcesList();
        ClearItemPreview();

        _craftButton.gameObject.SetActive(false);

        DisplayCraftInventory();
    }

    private void ClearSlot()
    {
        //_craftCart = new Dictionary<InventoryItemData, int>();
        _craftingCartUI = new Dictionary<CraftItemData, ItemCraft>();

        foreach (var item in _itemListContentPanel.transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }

    }

    private void ClearResourcesList()
    {

        foreach (var item in _craftingCartContentPanel.transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }
    }

    private void DisplayCraftInventory()
    {
        foreach (var item in _craftSystem.CraftInventory)
        {
            if (item.ItemData == null)
                continue;

            var craftSlot = Instantiate(_craftSlotPrefab, _itemListContentPanel.transform);
            craftSlot.Init(item);
        }
    }


    public void UpdateItemPreview(CraftSlotUI craftSlotUI)
    {
        Debug.Log("112");
        var data = craftSlotUI.AssignedItemSlot.ItemData;

        _itemPreviewSprite.sprite = data.Icon;
        _itemPreviewSprite.color = Color.white;

        //if (data.IconBackground != null)
        //{
        //    _itemPreviewBackground.sprite = data.IconBackground;
        //    _itemPreviewBackground.color = Color.white;
        //}

        //else
        //    _itemPreviewBackground.color = _itemPreviewBackground.color.WithAlpha(0);

        _itemPreviewName.text = data.DisplayName;
        _itemPreviewDescription.text = data.Description;

        if (_selectedCraftSlotUI != null && _selectedCraftSlotUI != craftSlotUI)
        {
            _selectedCraftSlotUI.ResetSlotColor();
        }

        _selectedCraftSlotUI = craftSlotUI;
    }

    public void UpdateItemPreview(ItemCraft itemCraft)
    {
        var data = itemCraft.ItemData;

        _itemPreviewSprite.sprite = data.Icon;
        _itemPreviewSprite.color = Color.white;

        //if (data.IconBackground != null)
        //{
        //    _itemPreviewBackground.sprite = data.IconBackground;
        //    _itemPreviewBackground.color = Color.white;
        //}

        //else
        //    _itemPreviewBackground.color = _itemPreviewBackground.color.WithAlpha(0);

        _itemPreviewName.text = data.DisplayName;
        _itemPreviewDescription.text = data.Description;

        if (_selectedRequiredItem != null && _selectedRequiredItem != itemCraft)
        {
            _selectedRequiredItem.ResetSlotColor();
        }

        _selectedRequiredItem = itemCraft;
    }

    private void ClearItemPreview()
    {
        _itemPreviewSprite.sprite = null;
        _itemPreviewSprite.color = Color.clear;

        //_itemPreviewBackground.sprite = null;
        //_itemPreviewBackground.color = Color.clear;

        _itemPreviewName.text = "";
        _itemPreviewDescription.text = "";

    }

    public void SelectItemCraft(CraftSlotUI craftSlotUI)
    {
        ClearResourcesList();
        _selectedCraftSlotUI = craftSlotUI;

        var data = craftSlotUI.AssignedItemSlot.craftItemData.Recipes[0];

        if (data.RequiredItems.Count != data.AmountResources.Count)
        {
            Debug.LogError("Количество элементов в списке RequiredItems не соответствует количеству элементов в списке AmountResources.");
            return;
        }

        for (int i = 0; i < data.RequiredItems.Count; i++)
        {
            var requiredItem = data.RequiredItems[i];
            var requiredAmount = data.AmountResources[i];
            var requiredImage = data.RequiredItems[i].Icon;
            //var requiredImageBackground = data.RequiredItems[i].IconBackground;
            var requiredPrefab = data.CraftPrefab[i];



            if (!CanCraftItem(requiredItem, requiredAmount))
            {
                requiredPrefab.NameComponent.color = Color.red;
                requiredPrefab.AmountComponent.color = Color.red;
            }

            else
            {
                requiredPrefab.NameComponent.color = Color.black;
                requiredPrefab.AmountComponent.color = Color.black;
            }

            requiredPrefab.SetItemComponents(requiredItem, requiredItem.DisplayName, requiredAmount.ToString(), requiredImage);
            Instantiate(requiredPrefab, _craftingCartContentPanel.transform);
        }

    }

    public void CraftItem(CraftSlotUI craftSlotUI)
    {
        if (CanCraftItem(craftSlotUI))
        {
            Instantiate(craftSlotUI.AssignedItemSlot.craftItemData.ItemPrefab, pointCraftItem.position, Quaternion.identity);
            RemoveItemOnCraft(craftSlotUI);
            CanCraftItem(craftSlotUI);
            SelectItemCraft(craftSlotUI);
        }
    }

    public void CraftItemWrapper()
    {
        if (CanCraftItem(_selectedCraftSlotUI))
        {
            CraftItem(_selectedCraftSlotUI);
        }
    }


    private void RemoveItemOnCraft(CraftSlotUI craftSlotUI)
    {
        var data = craftSlotUI.AssignedItemSlot.craftItemData;

        foreach (var recipe in data.Recipes)
        {
            for (int i = 0; i < recipe.RequiredItems.Count; i++)
            {
                var requiredItem = recipe.RequiredItems[i];
                var requiredAmount = recipe.AmountResources[i];

                _playerInventoryHolder.PrimaryInventorySystem.RemoveItemsFromInventory(requiredItem, requiredAmount);
            }
        }

    }

    public bool CanCraftItem(CraftSlotUI craftSlotUI)
    {
        Dictionary<InventoryItemData, int> inventoryCounts = new Dictionary<InventoryItemData, int>();

        var data = craftSlotUI.AssignedItemSlot.craftItemData;

        foreach (var slot in _playerInventoryHolder.PrimaryInventorySystem.InventorySlots)
        {
            if (slot.ItemData != null)
            {
                if (inventoryCounts.ContainsKey(slot.ItemData))
                {
                    inventoryCounts[slot.ItemData] += slot.StackSize;
                }
                else
                {
                    inventoryCounts[slot.ItemData] = slot.StackSize;
                }
            }
        }


        foreach (var recipe in data.Recipes)
        {
            for (int i = 0; i < recipe.RequiredItems.Count; i++)
            {
                var requiredItem = recipe.RequiredItems[i];
                var requiredAmount = recipe.AmountResources[i];
                var requiredPrefab = recipe.CraftPrefab[i];

                if (!inventoryCounts.ContainsKey(requiredItem) || inventoryCounts[requiredItem] < requiredAmount)
                {
                    _craftButton.gameObject.SetActive(false);


                    return false;
                }
            }
        }

        _craftButton.gameObject.SetActive(true);
        return true;
    }

    public bool CanCraftItem(InventoryItemData requiredItem, int requiredAmount)
    {
        Dictionary<InventoryItemData, int> inventoryCounts = new Dictionary<InventoryItemData, int>();

        foreach (var slot in _playerInventoryHolder.PrimaryInventorySystem.InventorySlots)
        {
            if (slot.ItemData != null)
            {
                if (inventoryCounts.ContainsKey(slot.ItemData))
                {
                    inventoryCounts[slot.ItemData] += slot.StackSize;
                }
                else
                {
                    inventoryCounts[slot.ItemData] = slot.StackSize;
                }
            }
        }

        if (inventoryCounts.ContainsKey(requiredItem) && inventoryCounts[requiredItem] >= requiredAmount)
        {
            return true;
        }
        return false;
    }

}
