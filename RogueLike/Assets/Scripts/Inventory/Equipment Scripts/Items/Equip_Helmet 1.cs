using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip_Helmet1 : EquipSlot
{
    //[SerializeField] private float[] _moveSpeed;
    //[SerializeField] private float[] _maxHealth;
    //[SerializeField] private float[] _currentHealth;
 
    //protected override void UseEquip(Player player, int itemTier, bool equip)
    //{
    //    if (itemTier >= 0)
    //    {
    //        float speedChange = equip ? _moveSpeed[itemTier] : -_moveSpeed[itemTier];
    //        float maxHealthChange = equip ? _maxHealth[itemTier] : -_maxHealth[itemTier];
    //        float currentHealthChange = equip ? _currentHealth[itemTier] : -_currentHealth[itemTier];

    //        if (player.TryGetComponent(out IMoveable moveable))
    //            moveable.ChangeMoveSpeed(speedChange);

    //        if(player.TryGetComponent(out IHealthChangeable healthChangeable))
    //        {
    //            healthChangeable.ChangeMaxHealth(maxHealthChange);
    //            healthChangeable.ChangeCurrentHealth(currentHealthChange);
    //        }
    //    }
    //}
}
