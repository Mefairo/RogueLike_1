using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCraft: MonoBehaviour
{
    [SerializeField] private InventoryItemData _itemData;
    [SerializeField] private TextMeshProUGUI _nameComponent;
    [SerializeField] private TextMeshProUGUI _amountComponent;
    [SerializeField] private Image _imageComponent;
    [SerializeField] private Button _updatePreviewButton;

    public InventoryItemData ItemData => _itemData;
    public TextMeshProUGUI NameComponent => _nameComponent;
    public TextMeshProUGUI AmountComponent => _amountComponent;
    public Image ImageComponent => _imageComponent;
    public CraftKeeperDisplay ParentDisplay { get; private set; }

    private void Awake()
    {
        //_imageComponent.sprite = null;
        //_imageComponent.preserveAspect = true;
        //_imageComponent.color = Color.clear;
        //_nameComponent.text = "";
        //_amountComponent.text = "";

        _updatePreviewButton?.onClick.AddListener(UpdateItemPreview);

        ParentDisplay = GetComponentInParent<CraftKeeperDisplay>();
    }

    public void SetItemComponents(InventoryItemData itemData, string newName, string newAmount, Sprite newImage)
    {
        _itemData = itemData;
        _nameComponent.text = newName;
        _amountComponent.text = newAmount;
        _imageComponent.sprite = newImage;
    }

    private void UpdateItemPreview()
    {
        ParentDisplay.UpdateItemPreview(this);

        SelectSlot(this);
    }

    private void SelectSlot(ItemCraft itemCraft)
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
