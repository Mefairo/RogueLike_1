using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : UnitStats
{

    private void SetStats()
    {
        Damage = _initialDamage + _bonusDamage;
        CritMultiply = _initialCritMultiply + _bonusCritMultiply;
        CritChance = _initialCritChance + _bonusCritChance;
        StartTimeBtwShots = _initialStartTimeBtwShots + _bonusAttackSpeed;
        BulletSpeed = _initialBulletSpeed + _bonusBulletSpeed;
        LifeTime = _initialLifeTime + _bonusLifetime;
    }

    public override void ChangeDamage(float damage)
    {
        Damage += damage;
    }

    public override void ChangeCritMultiply(float critMuliply)
    {
        CritMultiply += critMuliply;
    }

    public override void ChangeCritChance(float critChance)
    {
        CritChance += critChance;
    }

    public override void ChangeAttackSpeed(float attackSpeed)
    {
        StartTimeBtwShots += attackSpeed;
    }

    public override void ChangeBulletSpeed(float bulletSpeed)
    {
        BulletSpeed += bulletSpeed;
    }

    public override void ChangeLifeTime(float lifetime)
    {
        LifeTime += lifetime;
    }

    public override void ChangeArmor(float armor)
    {
        Armor += armor;
    }

    public override void ChangeEvasion(float evasion)
    {
        Evasion += evasion;
    }
}
