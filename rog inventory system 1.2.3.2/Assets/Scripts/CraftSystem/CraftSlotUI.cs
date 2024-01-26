using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CraftSlotUI : MonoBehaviour
{
    [SerializeField] private Image _itemSprite;
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private CraftSlot _assignedItemSlot;

    public CraftSlot AssignedItemSlot => _assignedItemSlot;

    [SerializeField] private Button _updatePreviewButton;

    public CraftKeeperDisplay ParentDisplay { get; private set; }


    private void Awake()
    {
        _itemSprite.sprite = null;
        _itemSprite.preserveAspect = true;
        _itemSprite.color = Color.clear;
        _itemName.text = "";

        _updatePreviewButton?.onClick.AddListener(UpdateItemPreview);

        ParentDisplay = GetComponentInParent<CraftKeeperDisplay>();
    }

    public void Init(CraftSlot craftSlot)
    {
        _assignedItemSlot = craftSlot;

        UpdateUISlot();
    }

    private void UpdateUISlot()
    {
        if (_assignedItemSlot != null)
        {
            _itemSprite.sprite = _assignedItemSlot.ItemData.Icon;
            _itemSprite.color = Color.white;
            _itemName.text = $"{AssignedItemSlot.ItemData.DisplayName}";
        }
        else
        {
            _itemSprite.sprite = null;
            _itemSprite.color = Color.clear;
            _itemName.text = "";
        }
    }

    private void UpdateItemPreview()
    {
        ParentDisplay.UpdateItemPreview(this);
        ParentDisplay.SelectItemCraft(this);
        ParentDisplay.CanCraftItem(this);

        SelectSlot(this);
    }

    private void SelectSlot(CraftSlotUI craftSlotUI)
    {
        ColorBlock colors = this._updatePreviewButton.colors;
        colors.normalColor = Color.white;
        colors.colorMultiplier = 5f;
        this._updatePreviewButton.colors = colors;
    }

    public void ResetSlotColor()
    {
        ColorBlock colors = this._updatePreviewButton.colors;
        colors.normalColor = Color.white;
        colors.colorMultiplier = 1f;
        this._updatePreviewButton.colors = colors;
    }

}
