using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : UnitHealth
{
    [SerializeField] private UnitStats _enemyStats;

    public override void ChangeCurrentHealth(float damageValue)
    {
        CurrentHealth -= damageValue;
    }

    public override void ChangeMaxHealth(float value)
    {
        MaxHealth += value;
    }

    public override void HealUnitDamage(float healValue)
    {
        CurrentHealth += healValue;
    }

    public override void TakeTrapDamage(float damageValue)
    {
        CurrentHealth -= damageValue;
    }

    public override void TakeUnitDamage(float damageValue)
    {
        CurrentHealth -= damageValue;
    }

    public override void LifeSteal(float damageValue)
    {
        Debug.Log(damageValue);

        int chanceLifesteal = Random.Range(0, 100);

        if (chanceLifesteal > _enemyStats.LifestealChance)
        {
            Debug.Log("return");
            return;
        }

        else
        {
            Debug.Log("return 1");
            CurrentHealth += damageValue * _enemyStats.LifestealMultiply;
        }
    }

    protected override void CheckHealth(float health)
    {
        if (health <= 0)
            UnitDeath();
    }

    protected override void UnitDeath()
    {
        Destroy(gameObject);
    }
}
