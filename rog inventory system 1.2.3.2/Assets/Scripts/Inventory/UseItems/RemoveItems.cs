using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveItems
{
    public void RemoveItemsFromSlot(InventorySlot_UI invSlot_UI)
    {
        invSlot_UI.AssignedInventorySlot.RemoveFromStack(1);
        invSlot_UI.UpdateUISlot();
    }

}
