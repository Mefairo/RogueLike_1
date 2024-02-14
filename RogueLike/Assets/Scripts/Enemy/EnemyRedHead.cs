using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyRedHead : EntityStats
{
    public override void NewRoundStats()
    {
        Damage = EnemyManager.Instance.RoundManager.CountRound + _bonusDamage + _initialDamage;

        if (EnemyManager.Instance.RoundManager.CountRound % 2 == 0)
        {
            BulletSpeed += EnemyManager.Instance.RoundManager.CountRound * 2;
        }
        else
            BulletSpeed = BulletSpeed;

    }

   

    public override void ChangeArmor(float armor)
    {
        
    }

    public override void ChangeEvasion(float evasion)
    {
        
    }

    public override void ChangeLifestealMultiply(float lifestealMultiply)
    {
       
    }

    public override void ChangeLifestealChance(float lifestealChance)
    {
        
    }
}
