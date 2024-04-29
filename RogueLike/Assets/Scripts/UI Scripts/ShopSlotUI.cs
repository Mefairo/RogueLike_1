using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Preview Item")]
    [SerializeField] private Image _itemSprite;
    [SerializeField] private Image _backgroundSprite;
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemCount;
    [SerializeField] private TextMeshProUGUI _itemPrice;
    [SerializeField] private ShopSlot _assignedItemSlot;
    [Space]
    [Header("Other")]
    [SerializeField] private Button _addItemToCartButton;
    [SerializeField] private Button _removeItemFromCartButton;
    [SerializeField] private Button _updatePreviewButton;
    [Space]
    [SerializeField] private ItemsShowInfo _panelInfo;

    public ShopSlot AssignedItemSlot => _assignedItemSlot;
    public ShopKeeperDisplay ParentDisplay { get; private set; }

    public float MarkUp { get; private set; }

    private int _tempAmount;

    private void Awake()
    {
        _itemSprite.sprite = null;
        _itemSprite.preserveAspect = true;
        _itemSprite.color = Color.clear;

        _backgroundSprite.sprite = null;
        _backgroundSprite.preserveAspect = true;
        _backgroundSprite.color = Color.clear;

        _itemName.text = "";
        _itemCount.text = "";
        _itemPrice.text = "";

        _addItemToCartButton?.onClick.AddListener(AddItemToCart);
        _removeItemFromCartButton?.onClick.AddListener(RemoveItemFromCart);
        _updatePreviewButton?.onClick.AddListener(UpdateItemPreview);

        ParentDisplay = GetComponentInParent<ShopKeeperDisplay>();
    }

    private void Start()
    {
        _panelInfo = UIManager.Instance.PanelInfo;

        if (_panelInfo != null)
            _panelInfo.gameObject.SetActive(false);
    }

    public void Init(ShopSlot slot, float markUp)
    {
        _assignedItemSlot = slot;
        MarkUp = markUp;
        _tempAmount = slot.StackSize;

        if (slot.EquipSlot != null)
        {
            _assignedItemSlot.EquipSlot.ItemData = slot.EquipSlot.ItemData;
            _assignedItemSlot.EquipSlot.ItemTier = slot.EquipSlot.ItemTier;
        }

        UpdateUISlot();
    }

    private void UpdateUISlot()
    {
        if (_assignedItemSlot != null)
        {
            _itemSprite.sprite = _assignedItemSlot.ItemData.Icon;
            _itemSprite.color = Color.white;

            if (_assignedItemSlot.ItemData.IconBackground != null)
            {
                ChangeBackgroundColor(_assignedItemSlot);

                //_backgroundSprite.sprite = _assignedItemSlot.ItemData.IconBackground;
                //_backgroundSprite.color = Color.white;
            }

            else
                _backgroundSprite.color = _backgroundSprite.color.WithAlpha(0);


            var modifiedPrice = ShopKeeperDisplay.GetModifiedPrice(_assignedItemSlot.ItemData, 1, MarkUp);

            _itemName.text = $"{_assignedItemSlot.ItemData.DisplayName}";
            _itemCount.text = $"Amount: {_assignedItemSlot.StackSize}";
            _itemPrice.text = modifiedPrice.ToString();
        }
        else
        {
            _itemSprite.sprite = null;
            _itemSprite.color = Color.clear;

            _backgroundSprite.sprite = null;
            _backgroundSprite.color = Color.clear;

            _itemName.text = "";
            _itemCount.text = "";
            _itemPrice.text = "";
        }
    }

    private void AddItemToCart()
    {
        if (_tempAmount <= 0)
            return;

        _tempAmount--;
        ParentDisplay.AddItemToCart(this);

        _itemCount.text = $"Amount: {_tempAmount}";
        //_itemCount.text = _tempAmount.ToString();
    }

    private void RemoveItemFromCart()
    {
        if (_tempAmount == _assignedItemSlot.StackSize)
            return;

        _tempAmount++;
        ParentDisplay.RemoveItemFromCart(this);

        _itemCount.text = $"Amount: {_tempAmount}";
        //_itemCount.text = _tempAmount.ToString();
    }

    private void ChangeBackgroundColor(ShopSlot slot)
    {
        if (slot.ItemData.IconBackground != null)
        {
            _backgroundSprite.sprite = slot.ItemData.IconBackground;

            if (slot.EquipSlot.ItemTier == 2)
            {
                _backgroundSprite.color = Color.blue;
            }

            else if (slot.EquipSlot.ItemTier == 1)
            {
                _backgroundSprite.color = Color.green;
            }

            else if (slot.EquipSlot.ItemTier == 0)
            {
                _backgroundSprite.color = Color.white;
            }
        }

        else
            _backgroundSprite.color = _backgroundSprite.color.WithAlpha(0);
    }

    public void UpdateItemPreview()
    {
        ParentDisplay.UpdateItemPreview(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _panelInfo.ShowInfo(this.AssignedItemSlot.ItemData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _panelInfo.HideInfo();
    }
}
