using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemsShowInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _itemTypeText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _statsNameText;
    [SerializeField] private TextMeshProUGUI _statsValueText;

    public void ShowInfo(InventorySlot_UI slot)
    {
        CraftItemData item = (CraftItemData)slot.AssignedInventorySlot.ItemData;

        if (item != null)
        {
            gameObject.SetActive(true);

            _nameText.text = item.DisplayName;
            _itemTypeText.text = item.EquipType.ToString();
            _descriptionText.text = item.Description;

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
            statsText += $"{stat.Stats}: \n";
            statsValue += $"{stat.ValueStat[item.ItemTierCount]}\n";
        }

        _statsNameText.text = statsText;
        _statsValueText.text = statsValue;
    }
}
