using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsShowInfo : MonoBehaviour
{


    private void OnEnable()
    {
        InventorySlot_UI.OnItemShowInfo += ShowInfo;
        InventorySlot_UI.OnItemHideInfo += HideInfo;
    }

    private void OnDisable()
    {
        InventorySlot_UI.OnItemShowInfo -= ShowInfo;
        InventorySlot_UI.OnItemHideInfo -= HideInfo;
    }

    private void ShowInfo()
    {
        Debug.Log("show 1");
    }

    private void HideInfo()
    {
        Debug.Log("hide 1");
    }
}
