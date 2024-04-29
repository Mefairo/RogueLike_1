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
using UnityEngine.Scripting;

public class ShopKeeperDisplay : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private ShopSlotUI _shopSlotPrefab;
    [SerializeField] private ShoppingCartItemUI _shoppingCartItemPrefab;
    [Space]
    [Header("Shop Tabs")]
    [SerializeField] private Button _buyTab;
    [SerializeField] private Button _sellTab;
    [Space]
    [Header("Shopping Cart")]
    [SerializeField] private TextMeshProUGUI _basketTotalText;
    [SerializeField] private TextMeshProUGUI _playerGoldText;
    [SerializeField] private TextMeshProUGUI _shopGoldText;
    [SerializeField] private Button _buyButton;
    [SerializeField] private TextMeshProUGUI _buyButtonText;
    [Space]
    [Header("Item Preview Section")]
    [SerializeField] private Image _itemPreviewSprite;
    [SerializeField] private Image _backgroundPreviewSprite;
    [SerializeField] private TextMeshProUGUI _itemPreviewName;
    [SerializeField] private TextMeshProUGUI _itemPreviewDescription;
    [Space]
    [Header("Cart")]
    [SerializeField] private GameObject _itemListContentPanel;
    [SerializeField] private GameObject _shoppingCartContentPanel;

    private int _basketTotal;
    private bool _isSelling;

    private ShopSystem _shopSystem;
    private PlayerInventoryHolder _playerInventoryHolder;

    private Dictionary<InventoryItemData, int> _shoppingCart = new Dictionary<InventoryItemData, int>();

    private Dictionary<InventoryItemData, ShoppingCartItemUI> _shoppingCartUI = new Dictionary<InventoryItemData, ShoppingCartItemUI>();

    public void DisplayShopWindow(ShopSystem shopSystem, PlayerInventoryHolder playerInventoryHolder)
    {
        _shopSystem = shopSystem;
        _playerInventoryHolder = playerInventoryHolder;

        RefreshDisplay();
        DisplayShopInventory();
    }

    private void RefreshDisplay()
    {
        if (_buyButton != null)
        {
            _buyButtonText.text = _isSelling ? "Sell Items" : "Buy Items";
            _buyButton.onClick.RemoveAllListeners();
            if (_isSelling)
                _buyButton.onClick.AddListener(SellItems);
            else
                _buyButton.onClick.AddListener(BuyItems);
        }

        ClearSlots();
        ClearItemPreview();

        _basketTotalText.enabled = false;
        _buyButton.gameObject.SetActive(false);
        _basketTotal = 0;
        _playerGoldText.text = $"Player Gold: {_playerInventoryHolder.PrimaryInventorySystem.Gold}";
        _shopGoldText.text = $"Shop Gold: {_shopSystem.AvailableGold}";
    }

    private void BuyItems()
    {
        if (_playerInventoryHolder.PrimaryInventorySystem.Gold < _basketTotal)
            return;

        if (!_playerInventoryHolder.PrimaryInventorySystem.CheckInventoryRemaining(_shoppingCart))
            return;

        foreach (var kvp in _shoppingCart)
        {
            _shopSystem.PurchaseItem(kvp.Key, kvp.Value);

            for (int i = 0; i < kvp.Value; i++)
            {
                _playerInventoryHolder.PrimaryInventorySystem.AddToInventory(kvp.Key, 1);
            }
        }

        _shopSystem.GainGold(_basketTotal);
        _playerInventoryHolder.PrimaryInventorySystem.SpendGold(_basketTotal);

        RefreshDisplay();
        DisplayShopInventory();
    }

    private void SellItems()
    {
        if (_shopSystem.AvailableGold < _basketTotal)
            return;

        foreach (var kvp in _shoppingCart)
        {
            var price = GetModifiedPrice(kvp.Key, kvp.Value, _shopSystem.SellMarkUp);

            _shopSystem.SellItem(kvp.Key, kvp.Value, price);

            _playerInventoryHolder.PrimaryInventorySystem.GainGold(price);
            _playerInventoryHolder.PrimaryInventorySystem.RemoveItemsFromInventory(kvp.Key, kvp.Value);
        }

        RefreshDisplay();
        DisplayShopInventory();
    }

    private void ClearSlots()
    {
        _shoppingCart = new Dictionary<InventoryItemData, int>();
        _shoppingCartUI = new Dictionary<InventoryItemData, ShoppingCartItemUI>();

        foreach (var item in _itemListContentPanel.transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }

        foreach (var item in _shoppingCartContentPanel.transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }
    }

    private void CreatePlayerItemSlot(KeyValuePair<InventoryItemData, int> item)
    {
        var tempSlot = new ShopSlot();
        tempSlot.AssignItem(item.Key, item.Value);

        //if (tempSlot.ItemData.ItemType == ItemType.Equipment)
        //{
        //    tempSlot.AssignEquipSlot();
        //    tempSlot.AssignEquipItem(tempSlot.ItemData, tempSlot, 1);
        //}

        //else
        //    tempSlot.AssignItem(item.Key, item.Value);

        ShopSlotUI shopSlot = Instantiate(_shopSlotPrefab, _itemListContentPanel.transform);
        shopSlot.Init(tempSlot, _shopSystem.SellMarkUp);
    }

    private void CreateShopItemSlot(ShopSlot item)
    {
        ShopSlotUI shopSlot = Instantiate(_shopSlotPrefab, _itemListContentPanel.transform);
        shopSlot.Init(item, _shopSystem.BuyMarkUp);
    }

    public void TabClicked(CheckTypeForTabs tab)
    {
        RefreshDisplay();

        if (_isSelling)
        {
            foreach (var item in _playerInventoryHolder.PrimaryInventorySystem.GetAllItemHeld())
            {
                if (tab.ItemType == ItemType.All)
                    CreatePlayerItemSlot(item);

                else
                {
                    if (item.Key.ItemType == tab.ItemType)
                        CreatePlayerItemSlot(item);

                    else
                        continue;
                }
            }
        }

        else
        {
            foreach (var item in _shopSystem.ShopInventory)
            {
                if (item.ItemData == null)
                    continue;

                if (tab.ItemType == ItemType.All)
                    CreateShopItemSlot(item);

                else
                {
                    if (item.ItemData.ItemType == tab.ItemType)
                        CreateShopItemSlot(item);

                    else
                        continue;
                }
            }
        }
    }

    private void DisplayShopInventory()
    {
        RefreshDisplay();

        if (_isSelling)
        {
            foreach (var item in _playerInventoryHolder.PrimaryInventorySystem.GetAllItemHeld())
                CreatePlayerItemSlot(item);
        }

        else
        {
            foreach (var item in _shopSystem.ShopInventory)
            {
                if (item.ItemData == null)
                    continue;

                CreateShopItemSlot(item);
            }
        }

    }

    public void RemoveItemFromCart(ShopSlotUI shopSlotUI)
    {
        var data = shopSlotUI.AssignedItemSlot.ItemData;
        var price = GetModifiedPrice(data, 1, shopSlotUI.MarkUp);

        if (_shoppingCart.ContainsKey(data))
        {
            _shoppingCart[data]--;

            string newAmount = $"x{_shoppingCart[data]}";
            ShoppingCartItemUI shoppongCartSlot = _shoppingCartUI[data];
            var backgroundImage = data.IconBackground;/////////////////////////////

            shoppongCartSlot.SetItemText(data, newAmount);

            if (_shoppingCart[data] <= 0)
            {
                _shoppingCart.Remove(data);
                var tempObj = _shoppingCartUI[data].gameObject;
                _shoppingCartUI.Remove(data);
                Destroy(tempObj);
            }
        }

        _basketTotal -= price;
        _basketTotalText.text = $"Total: {_basketTotal}G";

        if (_basketTotal <= 0 && _basketTotalText.IsActive())
        {
            _basketTotalText.enabled = false;
            _buyButton.gameObject.SetActive(false);
            ClearItemPreview();
            return;
        }

        CheckCartVsAvailableGold();
    }

    private void ClearItemPreview()
    {
        _itemPreviewSprite.sprite = null;
        _itemPreviewSprite.color = Color.clear;

        _backgroundPreviewSprite.sprite = null;
        _backgroundPreviewSprite.color = Color.clear;

        _itemPreviewName.text = "";
        _itemPreviewDescription.text = "";
    }

    public void AddItemToCart(ShopSlotUI shopSlotUI)
    {
        var data = shopSlotUI.AssignedItemSlot.ItemData;
        var shoppongCartItem = _shoppingCartItemPrefab;

        UpdateItemPreview(shopSlotUI);

        var price = GetModifiedPrice(data, 1, shopSlotUI.MarkUp);

        if (_shoppingCart.ContainsKey(data))
        {
            _shoppingCart[data]++;

            string newAmount = $"x{_shoppingCart[data]}";
            ShoppingCartItemUI shoppongCartSlot = _shoppingCartUI[data];
            var backgroundImage = data.IconBackground;////////////////////////////

            shoppongCartSlot.SetItemText(data, newAmount);
        }

        else
        {
            _shoppingCart.Add(data, 1);

            ShoppingCartItemUI shoppingCartTextObj = Instantiate(shoppongCartItem, _shoppingCartContentPanel.transform);

            string newAmount = $"x1";
            var backgroundImage = data.IconBackground;//////////////////////////////

            shoppingCartTextObj.SetItemText(data, newAmount);

            _shoppingCartUI.Add(data, shoppingCartTextObj);
        }

        _basketTotal += price;
        _basketTotalText.text = $"Total: {_basketTotal}G";

        if (_basketTotal > 0 && !_basketTotalText.IsActive())
        {
            _basketTotalText.enabled = true;
            _buyButton.gameObject.SetActive(true);
        }

        CheckCartVsAvailableGold();
    }

    private void CheckCartVsAvailableGold()
    {
        var goldToCheck = _isSelling ? _shopSystem.AvailableGold : _playerInventoryHolder.PrimaryInventorySystem.Gold;

        _basketTotalText.color = _basketTotal > goldToCheck ? Color.red : Color.white;

        if (_isSelling || _playerInventoryHolder.PrimaryInventorySystem.CheckInventoryRemaining(_shoppingCart))
            return;

        _basketTotalText.text = "Not enough room in inventory";
        _basketTotalText.color = Color.red;
    }

    public static int GetModifiedPrice(InventoryItemData data, int amount, float markUp)
    {
        var baseValue = data.GoldValue * amount;

        return Mathf.FloorToInt(baseValue + baseValue * markUp);
    }

    public void UpdateItemPreview(ShopSlotUI shopSlotUI)
    {
        var data = shopSlotUI.AssignedItemSlot.ItemData;

        _itemPreviewSprite.sprite = data.Icon;
        _itemPreviewSprite.color = Color.white;

        if (data.IconBackground != null)
        {
            _backgroundPreviewSprite.sprite = data.IconBackground;
            _backgroundPreviewSprite.color = Color.white;
        }

        else
            _backgroundPreviewSprite.color = _backgroundPreviewSprite.color.WithAlpha(0);

        _itemPreviewName.text = data.DisplayName;
        _itemPreviewDescription.text = data.Description;
    }

    public void OnBuyTabPressed()
    {
        _isSelling = false;
        RefreshDisplay();
        DisplayShopInventory();
    }

    public void OnSellTabPressed()
    {
        _isSelling = true;
        RefreshDisplay();
        //DisplayPlayerInventory();
        DisplayShopInventory();
    }

}
