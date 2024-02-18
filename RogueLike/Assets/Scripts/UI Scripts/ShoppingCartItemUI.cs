using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShoppingCartItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private InventoryItemData _itemData;
    [SerializeField] private TextMeshProUGUI _itemText;
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private Image _itemSprite;
    //[SerializeField] private Image _backgroundSprite;
    [Space]
    [SerializeField] private ItemsShowInfo _panelInfo;

    public InventoryItemData ItemData => _itemData;

    private void Start()
    {
        _panelInfo = UIManager.Instance.PanelInfo;

        if (_panelInfo != null)
            _panelInfo.gameObject.SetActive(false);
    }

    public void SetItemText(string newString, string newAmount ,Sprite newImage)
    {
        _itemText.text = newString;
        _amountText.text = newAmount;
        _itemSprite.sprite = newImage;
        //_backgroundSprite.sprite = backgroundImage;
    }

    public void InitializeItem(InventoryItemData itemData)
    {
        _itemData = itemData;
    }

    public void SetItemText(InventoryItemData itemData, string newAmount)
    {
        _itemData = itemData;
        _itemText.text = $"- {itemData.DisplayName}";
        _amountText.text = newAmount;
        _itemSprite.sprite = itemData.Icon;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _panelInfo.ShowInfo(this.ItemData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _panelInfo.HideInfo();
    }
}

