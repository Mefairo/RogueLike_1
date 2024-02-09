using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEquipmentHolder : MonoBehaviour
{
    public static UnityAction OnPlayerEquipmentRequested;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("knopka okno");
            OnPlayerEquipmentRequested?.Invoke();
        }
    }
}
