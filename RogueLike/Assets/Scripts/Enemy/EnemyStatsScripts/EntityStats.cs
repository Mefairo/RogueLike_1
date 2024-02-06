using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityStats : UnitStats
{
    public abstract void NewRoundStats();

    public override abstract void ChangeDamage(float damage);
    public override abstract void ChangeCritMultiply(float critMuliply);
    public override abstract void ChangeCritChance(float critChance);
    public override abstract void ChangeAttackSpeed(float attackSpeed);
    public override abstract void ChangeBulletSpeed(float bulletSpeed);
    public override abstract void ChangeLifeTime(float lifetime);
}
