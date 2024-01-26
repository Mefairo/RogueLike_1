using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUIController : MonoBehaviour
{
     public EquipDisplay _equipPanel;

    private void OnEnable()
    {
        Debug.Log("okno open");
        PlayerEquipmentHolder.OnPlayerEquipmentRequested += DisplayEquipment;
    }

    private void OnDisable()
    {
        Debug.Log("okno close");
        PlayerEquipmentHolder.OnPlayerEquipmentRequested -= DisplayEquipment;
    }

    private void DisplayEquipment()
    {
        Debug.Log("metod okno");
        _equipPanel.gameObject.SetActive(true);
    }
}
