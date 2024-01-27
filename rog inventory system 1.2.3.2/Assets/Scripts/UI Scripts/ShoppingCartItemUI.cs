using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingCartItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemText;
    [SerializeField] private Image _itemSprite;
    [SerializeField] private Image _backgroundSprite;

    public void SetItemText(string newString, Sprite newImage, Sprite backgroundImage)
    {
        _itemText.text = newString;
        _itemSprite.sprite = newImage;
        _backgroundSprite.sprite = backgroundImage;
    }
}

