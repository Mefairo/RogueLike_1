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

    private void UpdateInfo(InventoryItemData item)
    {
        ClearInfo();

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

    public void ShowInfo(InventoryItemData item)
    {
        UpdateInfo(item);

        if (item != null)
            UpdateText(item);
    }

    public void HideInfo()
    {
        gameObject.SetActive(false);
    }

    private void UpdateText(InventoryItemData item)
    {
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

    private void ClearInfo()
    {
        _nameText.text = "";
        _descriptionText.text = "";
        _itemTypeText.text = "";
        _statsNameText.text = "";
        _statsValueText.text = "";
    }
}
