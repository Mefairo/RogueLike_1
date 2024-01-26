using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEquipmentHolder : MonoBehaviour
{
    [SerializeField] private EquipDisplay _equipDisplay;
    [SerializeField] private EquipmentManager _equipManager;

    public static UnityAction OnPlayerEquipmentRequested;

    private void OnEnable()
    {
        Debug.Log("helm enable");
        _equipDisplay.OnPlayerEquip += _equipManager.EquipItem;
        _equipDisplay.OnPlayerTakeOfEquip += _equipManager.TakeOfEquipItem;
    }

    private void OnDisable()
    {
        Debug.Log("helm disable");
        _equipDisplay.OnPlayerEquip -= _equipManager.EquipItem;
        _equipDisplay.OnPlayerTakeOfEquip -= _equipManager.TakeOfEquipItem;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("knopka okno");
            OnPlayerEquipmentRequested?.Invoke();
        }
    }
}
