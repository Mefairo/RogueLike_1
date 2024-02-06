using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] private Equip_Helmet1 _equipHelmet_1;
    [SerializeField] private Equip_Helmet2 _equipHelmet_2;
    [Space]
    [SerializeField] private Player _player;

    public Player Player => _player;

    internal void EquipItem(EquipSlot_UI equipSlot)
    {
        Debug.Log("EQUIP manager");
        var equipID = equipSlot.AssignedInventorySlot.ItemData.ID;

        switch (equipID)
        {
            case 111:
                Debug.Log("manager switch 1");
                _equipHelmet_1.Equip(_player, equipSlot);
                break;
            case 112:
                Debug.Log("manager switch 2");
                _equipHelmet_2.Equip(_player, equipSlot);
                break;
        }
    }

    public void TakeOfEquipItem(EquipSlot_UI equipSlot)
    {
        Debug.Log("DISEQUIP manager");
        var equipID = equipSlot.AssignedInventorySlot.ItemData.ID;

        switch (equipID)
        {
            case 111:
                Debug.Log("DISmanager switch 1");
                _equipHelmet_1.UnEquip(_player, equipSlot);
                break;
            case 112:
                Debug.Log("DISmanager switch 2");
                _equipHelmet_2.UnEquip(_player, equipSlot);
                break;
        }
    }
}
