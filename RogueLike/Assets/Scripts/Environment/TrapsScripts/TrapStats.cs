using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapStats : EntityStats
{
    private void Awake()
    {
        RefreshStats();
    }

    public override void NewRoundStats()
    {
        Damage = EnemyManager.Instance.RoundManager.CountRound + _bonusDamage;

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
