using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    [SerializeField] private PlayerInventoryHolder _inventoryHolder;
    [SerializeField] private TextMeshProUGUI _goldText;

    private void Start()
    {
        SetAmountGold(_inventoryHolder.PrimaryInventorySystem.Gold);
    }

    private void OnEnable()
    {
        _inventoryHolder.PrimaryInventorySystem.OnChangeGold += SetAmountGold;
    }

    private void OnDisable()
    {
        _inventoryHolder.PrimaryInventorySystem.OnChangeGold -= SetAmountGold;
    }

    private void SetAmountGold(int amount)
    {
        _goldText.text = $"{amount}";
    }

}
