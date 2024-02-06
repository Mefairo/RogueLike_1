using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public abstract class EquipSlot : MonoBehaviour
{
    [SerializeField] private List<EquipSlotStatsList> _statsList;

    public void Equip(Player player, EquipSlot_UI equipSlot_UI)
    {
        var itemTier = equipSlot_UI.AssignedInventorySlot.ItemData.ItemTierCount;

        UseEquip(player, itemTier, true);
    }

    public void UnEquip(Player player, EquipSlot_UI equipSlot_UI)
    {
        var itemTier = equipSlot_UI.AssignedInventorySlot.ItemData.ItemTierCount;

        UseEquip(player, itemTier, false);
    }

    protected void UseEquip(Player player, int itemTier, bool equip)
    {
        if (itemTier >= 0)
        {
            foreach (var stat in _statsList)
            {
                switch (stat.Stats)
                {
                    case PlayerStatsEnum.MaxHealth:
                        float maxHealthChange = equip ? stat.ValueStat[itemTier] : -stat.ValueStat[itemTier];

                        if (player.TryGetComponent(out IHealthChangeable maxHPChangeable))
                            maxHPChangeable.ChangeMaxHealth(maxHealthChange);
                        break;

                    case PlayerStatsEnum.CurrentHealth:
                        float currentHealthChange = equip ? stat.ValueStat[itemTier] : -stat.ValueStat[itemTier];

                        if (player.TryGetComponent(out IHealthChangeable currentHPChangeable))
                            currentHPChangeable.ChangeCurrentHealth(currentHealthChange);
                        break;

                    case PlayerStatsEnum.MoveSpeed:
                        float speedChange = equip ? stat.ValueStat[itemTier] : -stat.ValueStat[itemTier];

                        if (player.TryGetComponent(out IMoveable moveSpeedChangeable))
                            moveSpeedChangeable.ChangeMoveSpeed(speedChange);
                        break;

                    case PlayerStatsEnum.Damage:
                        float damageChange = equip ? stat.ValueStat[itemTier] : -stat.ValueStat[itemTier];

                        if (player.TryGetComponent(out IStatsChangeable damageChangeable))
                            damageChangeable.ChangeDamage(damageChange);
                        break;

                    case PlayerStatsEnum.CritMultiply:
                        float critMultiplyChange = equip ? stat.ValueStat[itemTier] : -stat.ValueStat[itemTier];

                        if (player.TryGetComponent(out IStatsChangeable critMultyChangeable))
                            critMultyChangeable.ChangeCritMultiply(critMultiplyChange);
                        break;

                    case PlayerStatsEnum.CritChance:
                        float critChanceChange = equip ? stat.ValueStat[itemTier] : -stat.ValueStat[itemTier];

                        if (player.TryGetComponent(out IStatsChangeable critChanceChangeable))
                            critChanceChangeable.ChangeCritChance(critChanceChange);
                        break;

                    case PlayerStatsEnum.StartTimeBtwShots:
                        float attackSpeedChange = equip ? stat.ValueStat[itemTier] : -stat.ValueStat[itemTier];

                        if (player.TryGetComponent(out IStatsChangeable attackSpeedChangeable))
                            attackSpeedChangeable.ChangeAttackSpeed(attackSpeedChange);
                        break;

                    case PlayerStatsEnum.BulletSpeed:
                        float bulletSpeedChange = equip ? stat.ValueStat[itemTier] : -stat.ValueStat[itemTier];

                        if (player.TryGetComponent(out IStatsChangeable bulletSpeedChangeable))
                            bulletSpeedChangeable.ChangeBulletSpeed(bulletSpeedChange);
                        break;

                    case PlayerStatsEnum.Lifetime:
                        float lifetimeChange = equip ? stat.ValueStat[itemTier] : -stat.ValueStat[itemTier];

                        if (player.TryGetComponent(out IStatsChangeable lifetimeChangeable))
                            lifetimeChangeable.ChangeLifeTime(lifetimeChange);
                        break;

                    case PlayerStatsEnum.Armor:
                        float armorChange = equip ? stat.ValueStat[itemTier] : -stat.ValueStat[itemTier];

                        if (player.TryGetComponent(out IDefensable armorChangeable))
                            armorChangeable.ChangeArmor(armorChange);
                        break;

                    case PlayerStatsEnum.Evasion:
                        float evasionChange = equip ? stat.ValueStat[itemTier] : -stat.ValueStat[itemTier];

                        if (player.TryGetComponent(out IDefensable evasionChangeable))
                            evasionChangeable.ChangeEvasion(evasionChange);
                        break;
                }


            }
        }
    }
}