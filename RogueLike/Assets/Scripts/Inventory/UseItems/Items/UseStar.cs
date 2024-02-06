using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseStar : MonoBehaviour
{
    private RemoveItems removeItem = new RemoveItems();
    public float speedAmount;

    public void UseItemStar(Player player, InventorySlot_UI invSlot_UI)
    {
        if (player.TryGetComponent(out IMoveable moveable))
            moveable.ChangeMoveSpeed(speedAmount);

        removeItem.RemoveItemsFromSlot(invSlot_UI);
    }
}
