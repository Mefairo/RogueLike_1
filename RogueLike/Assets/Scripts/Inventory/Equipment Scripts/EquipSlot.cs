using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot
{
    public void Subscribe()
    {
        UIManager.Instance.EquipDisplay.OnPlayerEquip += Equip;
        UIManager.Instance.EquipDisplay.OnPlayerTakeOfEquip += UnEquip;
    }

    public void Unsubscribe()
    {
        UIManager.Instance.EquipDisplay.OnPlayerEquip -= Equip;
        UIManager.Instance.EquipDisplay.OnPlayerTakeOfEquip -= UnEquip;
    }

    private void Equip(Player player, EquipSlot_UI equipSlot_UI)
    {
        InventoryItemData itemData = equipSlot_UI.AssignedInventorySlot.ItemData;

        UseEquip(player, true, itemData);

        if (itemData.ItemType == ItemType.Weapon_Mod)
            Mod(player, itemData);
    }

    private void UnEquip(Player player, EquipSlot_UI equipSlot_UI)
    {
        InventoryItemData itemData = equipSlot_UI.AssignedInventorySlot.ItemData;

        UseEquip(player, false, itemData);

        if (itemData.ItemType == ItemType.Weapon_Mod)
            UnMod(player, itemData);
    }

    private void UseEquip(Player player, bool equip, InventoryItemData itemData)
    {
        int itemTier = itemData.ItemTierCount;

        if (itemTier >= 0)
        {
            foreach (var stat in itemData.StatsList)
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

    private void Mod(Player player, InventoryItemData itemData)
    {
        WeaponItemData weaponItemData = itemData as WeaponItemData;
        if (weaponItemData != null)
        {
            player.PlayerGun.InitModifierWeapon(weaponItemData.WeaponMod);
        }
    }

    private void UnMod(Player player, InventoryItemData itemData)
    {
        WeaponItemData weaponItemData = itemData as WeaponItemData;
        if (weaponItemData != null)
        {
            player.PlayerGun.InitModifierWeapon(null);
        }
    }

    //private void Mod(Player player, InventoryItemData itemData)
    //{
    //    player.PlayerGun.InitModifierWeapon(itemData.ItemDataPrefab);
    //}

    //private void UnMod(Player player, InventoryItemData itemData)
    //{
    //    player.PlayerGun.InitModifierWeapon(itemData.ItemDataPrefab = null);
    //}
}

