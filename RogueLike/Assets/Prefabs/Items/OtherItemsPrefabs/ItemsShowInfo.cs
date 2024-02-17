using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class ItemsShowInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _itemTypeText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _statsNameText;
    [SerializeField] private TextMeshProUGUI _statsValueText;

    private void UpdateSlot(InventoryItemData item)
    {
        if (item != null)
        {
            gameObject.SetActive(true);

            _nameText.text = item.DisplayName;
            _descriptionText.text = item.Description;

            if (item is CraftItemData)
            {
                CraftItemData craftItem = (CraftItemData)item;
                _itemTypeText.text = craftItem.EquipType.ToString();
            }
        }
    }

    public void ShowInfo(InventorySlot_UI slot)
    {
        ClearInfo();
        InventoryItemData item = slot.AssignedInventorySlot.ItemData;
        UpdateSlot(item);
        if (item != null)
        {
            //gameObject.SetActive(true);

            //_nameText.text = item.DisplayName;
            //_descriptionText.text = item.Description;

            //if (item is CraftItemData)
            //{
            //    CraftItemData craftItem = (CraftItemData)item;
            //    _itemTypeText.text = craftItem.EquipType.ToString();
            //}

            SetStatsText(slot);
        }

    }

    public void ShowInfo(CraftSlotUI slot)
    {
        ClearInfo();
        InventoryItemData item = slot.AssignedItemSlot.ItemData;
        UpdateSlot(item);

        if (item != null)
        {
            //gameObject.SetActive(true);

            //_nameText.text = item.DisplayName;
            //_descriptionText.text = item.Description;

            //if (item is CraftItemData)
            //{
            //    CraftItemData craftItem = (CraftItemData)item;
            //    _itemTypeText.text = craftItem.EquipType.ToString();
            //}
            SetStatsText(slot);
        }

    }

    public void HideInfo()
    {
        gameObject.SetActive(false);
    }

    private void SetStatsText(InventorySlot_UI slot)
    {
        InventoryItemData item = slot.AssignedInventorySlot.ItemData;

        string statsText = "";
        string statsValue = "";

        foreach (var stat in item.StatsList)
        {
            if (stat != null)
            {
                statsText += $"{stat.Stats}: \n";
                statsValue += $"{stat.ValueStat[item.ItemTierCount]}\n";
            }
        }

        _statsNameText.text = statsText;
        _statsValueText.text = statsValue;
    }

    private void SetStatsText(CraftSlotUI slot)
    {
        InventoryItemData item = slot.AssignedItemSlot.ItemData;

        string statsText = "";
        string statsValue = "";

        foreach (var stat in item.StatsList)
        {
            statsText += $"{stat.Stats}: \n";
            statsValue += $"{stat.ValueStat[item.ItemTierCount]}\n";
        }

        _statsNameText.text = statsText;
        _statsValueText.text = statsValue;
    }

    private void ClearInfo()
    {
        _nameText.text = "";
        _descriptionText.text = "";
        _itemTypeText.text = "";
        _statsNameText.text = "";
        _statsValueText.text = "";
    }
}
