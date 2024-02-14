using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemsKeeperActive : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerInventoryHolder _inventoryHolder;
    [SerializeField] private int _price;
    [SerializeField] private GameObject _keeper;
    [SerializeField] private SpriteRenderer _shopSprite;
    [Space]
    [Header("UI")]
    [SerializeField] private Image _panel;
    [SerializeField] private Button _buttonYes;
    [SerializeField] private Button _buttonNo;
    [SerializeField] private TextMeshProUGUI _textPurchase;

    public UnityAction<IInteractable> OnInteractionComplete { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void Awake()
    {
        _keeper.gameObject.SetActive(false);
        _panel.gameObject.SetActive(false);

        _buttonYes.onClick.AddListener(ConfirmPurchase);
        _buttonNo.onClick.AddListener(ClosePanel);

        SetAlpha(0.5f);
    }

    private void SetAlpha(float alpha)
    {
        Color spriteColor = _shopSprite.color;
        spriteColor.a = alpha;
        _shopSprite.color = spriteColor;
    }

    private void ActivePanelConfirm()
    {
        _panel.gameObject.SetActive(true);

        _textPurchase.text = $"Do you really want to buy \n{_keeper.name} for {_price} gold?";
    }

    private void ConfirmPurchase()
    {
        SetAlpha(1);
        ClosePanel();
        _inventoryHolder.PrimaryInventorySystem.SpendGold(_price);

        _keeper.gameObject.SetActive(true);
    }

    private void ClosePanel()
    {
        _panel.gameObject.SetActive(false);
    }

    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        if (!_keeper.gameObject.activeSelf)
        {
            ActivePanelConfirm();

            interactSuccessful = true;
        }

        else
        {
            interactSuccessful = false;
        }
    }

    public void EndInteraction()
    {
        throw new System.NotImplementedException();
    }
}
