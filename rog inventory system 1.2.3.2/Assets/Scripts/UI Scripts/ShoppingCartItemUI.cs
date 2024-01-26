using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingCartItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemText;
    [SerializeField] private Image _itemSprite;

    public void SetItemText(string newString, Sprite newImage)
    {
        _itemText.text = newString;
        _itemSprite.sprite = newImage;
    }
}

