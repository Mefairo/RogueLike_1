using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingCartItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemText;
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private Image _itemSprite;
    //[SerializeField] private Image _backgroundSprite;

    public void SetItemText(string newString, string newAmount ,Sprite newImage)
    {
        _itemText.text = newString;
        _amountText.text = newAmount;
        _itemSprite.sprite = newImage;
        //_backgroundSprite.sprite = backgroundImage;
    }
}

