using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityStats : UnitStats
{
    public abstract void NewRoundStats();

    public override void ChangeDamage(float damage)
    {
        _bonusDamage += damage;
    }

    public override void ChangeCritMultiply(float critMuliply)
    {
        _bonusCritMultiply += critMuliply;
    }

    public override void ChangeCritChance(float critChance)
    {
        _bonusCritChance += critChance;
    }

    public override void ChangeAttackSpeed(float attackSpeed)
    {
        _bonusAttackSpeed -= attackSpeed;
    }

    public override void ChangeBulletSpeed(float bulletSpeed)
    {
        _bonusBulletSpeed += bulletSpeed;
    }

    public override void ChangeLifeTime(float lifetime)
    {
        _bonusLifetime += lifetime;
    }
}
