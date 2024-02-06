using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipSlot : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float[] _maxHealth;
    [SerializeField] private float[] _currentHealth;
    [Space]
    [Header("Movement")]
    [SerializeField] private float[] _moveSpeed;
    [Space]
    [Header("Damage")]
    [SerializeField] private float[] _damage;
    [SerializeField] protected float[] _critMultiply;
    [SerializeField] protected float[] _critChance;
    [SerializeField] protected float[] _startTimeBtwShots;
    [Space]
    [Header("Bullet Parametres")]
    [SerializeField] protected float[] _bulletSpeed;
    [SerializeField] protected float[] _lifeTime;
    [Space]
    [Header("Defence")]
    [SerializeField] protected int[] _armor;
    [SerializeField] protected int[] _evasion;

    [SerializeField] private Dictionary<ItemType, float> _equipValue;


    public void Equip(Player player, EquipSlot_UI equipSlot_UI)
    {
        Debug.Log("eq 1");
        var itemTier = equipSlot_UI.AssignedInventorySlot.ItemData.ItemTierCount;

        UseEquip(player, itemTier, true);
    }

    public void UnEquip(Player player, EquipSlot_UI equipSlot_UI)
    {
        Debug.Log("uneq 1");
        var itemTier = equipSlot_UI.AssignedInventorySlot.ItemData.ItemTierCount;

        UseEquip(player, itemTier, false);
    }

    //protected abstract void UseEquip(Player player, int itemTier, bool equip);

    protected void UseEquip(Player player, int itemTier, bool equip)
    {
        if (itemTier >= 0)
        {
            float maxHealthChange = equip ? _maxHealth[itemTier] : -_maxHealth[itemTier];
            float currentHealthChange = equip ? _currentHealth[itemTier] : -_currentHealth[itemTier];

            float speedChange = equip ? _moveSpeed[itemTier] : -_moveSpeed[itemTier];

            float damageChange = equip ? _damage[itemTier] : -_damage[itemTier];
            float critMultiplyChange = equip ? _critMultiply[itemTier] : -_critMultiply[itemTier];
            float critChanceChange = equip ? _critChance[itemTier] : -_critChance[itemTier];
            float attackSpeedChange = equip ? _startTimeBtwShots[itemTier] : -_startTimeBtwShots[itemTier];
            float bulletSpeedChange = equip ? _bulletSpeed[itemTier] : -_bulletSpeed[itemTier];
            float lifetimeChange = equip ? _lifeTime[itemTier] : -_lifeTime[itemTier];

            int armorChange = equip ? _armor[itemTier] : -_armor[itemTier];
            int evasionChange = equip ? _evasion[itemTier] : -_evasion[itemTier];


            if (player.TryGetComponent(out IHealthChangeable healthChangeable))
            {
                healthChangeable.ChangeMaxHealth(maxHealthChange);
                healthChangeable.ChangeCurrentHealth(currentHealthChange);
            }


            if (player.TryGetComponent(out IMoveable moveable))
                moveable.ChangeMoveSpeed(speedChange);

            if (player.TryGetComponent(out IStatsChangeable statsChangeable))
            {
                statsChangeable.ChangeDamage(damageChange);
                statsChangeable.ChangeCritMultiply(critMultiplyChange);
                statsChangeable.ChangeCritChance(critChanceChange);
                statsChangeable.ChangeAttackSpeed(attackSpeedChange);

                statsChangeable.ChangeBulletSpeed(bulletSpeedChange);
                statsChangeable.ChangeLifeTime(lifetimeChange);
            }

            if (player.TryGetComponent(out IDefensable defensable))
            {
                defensable.ChangeArmor(armorChange);
                defensable.ChangeEvasion(evasionChange);
            }
        }
    }

    //private void ApplyEquipmentEffect(Player player, float[] values, ItemType type, int itemTier, bool equip)
    //{
    //    if (values != null && itemTier >= 0 && itemTier < values.Length)
    //    {
    //        float valueChange = equip ? values[itemTier] : -values[itemTier];
    //        ApplyEquipmentEffect(player, type, valueChange);
    //    }
    //}

    //private void ApplyEquipmentEffect(Player player, ItemType type, float valueChange)
    //{
    //    switch (type)
    //    {
    //        case EquipmentType.MaxHealth:
    //            if (player.TryGetComponent(out IHealthChangeable healthChangeable))
    //                healthChangeable.ChangeMaxHealth(valueChange);
    //            break;

    //        case EquipmentType.CurrentHealth:
    //            if (player.TryGetComponent(out IHealthChangeable healthChangeable))
    //                healthChangeable.ChangeCurrentHealth(valueChange);
    //            break;

    //        case EquipmentType.MoveSpeed:
    //            if (player.TryGetComponent(out IMoveable moveable))
    //                moveable.ChangeMoveSpeed(valueChange);
    //            break;
    //    }
    //}
}