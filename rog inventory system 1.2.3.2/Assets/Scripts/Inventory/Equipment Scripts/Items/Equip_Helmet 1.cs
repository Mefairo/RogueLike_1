using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip_Helmet1 : EquipSlot
{
    [SerializeField] protected float[] _moveSpeed;

    public override void Equip(Player player, EquipSlot_UI equipSlot_UI)
    {
        var itemTier = equipSlot_UI.AssignedInventorySlot.ItemData.ItemTierCount;

        UseEquip(player, itemTier, true);
    }

    public override void UnEquip(Player player, EquipSlot_UI equipSlot_UI)
    {
        var itemTier = equipSlot_UI.AssignedInventorySlot.ItemData.ItemTierCount;

        UseEquip(player, itemTier, false);
    }

    private void UseEquip(Player player, int itemTier, bool equip)
    {
        if(itemTier >= 0 && itemTier < _moveSpeed.Length)
        {
            float speedChange = equip ? _moveSpeed[itemTier] : -_moveSpeed[itemTier];
            player.PlayerController.MoveSpeed += speedChange;
        }
    }
}
