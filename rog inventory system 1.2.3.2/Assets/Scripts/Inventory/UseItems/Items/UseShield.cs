using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseShield : UseItems
{
    public override void UseItem(Player player, StatusEffectsData statusData, InventorySlot_UI invSlot_UI)
    {
        base.UseItem(player, statusData, invSlot_UI);
    }

    public override IEnumerator HandleBuff(Player player, StatusEffectsData statusData)
    {
        return base.HandleBuff(player, statusData);
    }
}
