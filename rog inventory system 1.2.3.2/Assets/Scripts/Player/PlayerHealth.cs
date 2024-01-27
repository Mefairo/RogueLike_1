using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : UnitHealth
{
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

    protected override void CheckHealth(float health)
    {
        if (health <= 0)
            UnitDeath();
    }

    protected override void UnitDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
