using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatsChangeable
{
    public void ChangeDamage(float damage);
    public void ChangeCritMultiply(float critMuliply);
    public void ChangeCritChance(float critChance);
    public void ChangeAttackSpeed(float attackSpeed);
    public void ChangeBulletSpeed(float bulletSpeed);
    public void ChangeLifeTime(float lifetime);
    public void RefreshStats();
}
