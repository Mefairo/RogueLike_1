using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip_Helmet2 : EquipSlot
{
    [SerializeField] protected float[] _maxHP;

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
        if (itemTier >= 0 && itemTier < _maxHP.Length)
        {
            float maxHPChange = equip ? _maxHP[itemTier] : -_maxHP[itemTier];

            if (player.TryGetComponent(out IHealthChangeable healthChangeable))
            {
                healthChangeable.ChangeMaxHealth(maxHPChange);
                //healthChangeable.HealUnitDamage(maxHPChange);
            }
        }
    }
}
