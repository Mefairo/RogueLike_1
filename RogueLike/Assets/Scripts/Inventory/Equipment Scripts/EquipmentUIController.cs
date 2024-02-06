using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUIController : MonoBehaviour
{
    [SerializeField] private EquipDisplay _equipPanel;

    private void OnEnable()
    {
        PlayerEquipmentHolder.OnPlayerEquipmentRequested += DisplayEquipment;
    }

    private void OnDisable()
    {
        PlayerEquipmentHolder.OnPlayerEquipmentRequested -= DisplayEquipment;
    }

    private void DisplayEquipment()
    {
        _equipPanel.gameObject.SetActive(true);
    }
}
