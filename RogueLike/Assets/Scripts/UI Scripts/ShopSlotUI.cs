using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlotUI : MonoBehaviour
{
    [SerializeField] private Image _itemSprite;
    //[SerializeField] private Image _backgroundSprite;
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemCount;
    [SerializeField] private ShopSlot _assignedItemSlot;

    public ShopSlot AssignedItemSlot => _assignedItemSlot;

    [SerializeField] private Button _addItemToCartButton;
    [SerializeField] private Button _removeItemFromCartButton;
    [SerializeField] private Button _updatePreviewButton;

    public ShopKeeperDisplay ParentDisplay { get; private set; }

    public float MarkUp { get; private set; }

    private int _tempAmount;

    private void Awake()
    {
        _itemSprite.sprite = null;
        _itemSprite.preserveAspect = true;
        _itemSprite.color = Color.clear;

        //_backgroundSprite.sprite = null;
        //_backgroundSprite.preserveAspect = true;
        //_backgroundSprite.color = Color.clear;

        _itemName.text = "";
        _itemCount.text = "";

        _addItemToCartButton?.onClick.AddListener(AddItemToCart);
        _removeItemFromCartButton?.onClick.AddListener(RemoveItemFromCart);
        _updatePreviewButton?.onClick.AddListener(UpdateItemPreview);

        ParentDisplay = GetComponentInParent<ShopKeeperDisplay>();
    }

    public void Init(ShopSlot slot, float markUp)
    {
        _assignedItemSlot = slot;
        MarkUp = markUp;
        _tempAmount = slot.StackSize;
        UpdateUISlot();
    }

    private void UpdateUISlot()
    {
        if (_assignedItemSlot != null)
        {
            _itemSprite.sprite = _assignedItemSlot.ItemData.Icon;
            _itemSprite.color = Color.white;

            //if(_assignedItemSlot.ItemData.IconBackground != null)
            //{
            //    _backgroundSprite.sprite = _assignedItemSlot.ItemData.IconBackground;
            //    _backgroundSprite.color = Color.white;
            //}

            //else
            //    _backgroundSprite.color = _backgroundSprite.color.WithAlpha(0);

            _itemCount.text = _assignedItemSlot.StackSize.ToString();

            var modifiedPrice = ShopKeeperDisplay.GetModifiedPrice(_assignedItemSlot.ItemData, 1, MarkUp);
            _itemName.text = $"{_assignedItemSlot.ItemData.DisplayName} - {modifiedPrice} gold";
        }
        else
        {
            _itemSprite.sprite = null;
            _itemSprite.color = Color.clear;

            //_backgroundSprite.sprite = null;
            //_backgroundSprite.color = Color.clear;

            _itemName.text = "";
            _itemCount.text = "";
        }
    }

    private void AddItemToCart()
    {
        if (_tempAmount <= 0)
            return;

        _tempAmount--;
        ParentDisplay.AddItemToCart(this);
        _itemCount.text = _tempAmount.ToString();
    }

    private void RemoveItemFromCart()
    {
        if (_tempAmount == _assignedItemSlot.StackSize)
            return;

        _tempAmount++;
        ParentDisplay.RemoveItemFromCart(this);
        _itemCount.text = _tempAmount.ToString();
    }

    public void UpdateItemPreview()
    {
        ParentDisplay.UpdateItemPreview(this);
    }
}
