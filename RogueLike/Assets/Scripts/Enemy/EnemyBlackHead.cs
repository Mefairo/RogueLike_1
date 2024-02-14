using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBlackHead : EntityStats
{

    public override void NewRoundStats()
    {
        Damage = (EnemyManager.Instance.RoundManager.CountRound * 2) + _bonusDamage + _initialDamage;
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
