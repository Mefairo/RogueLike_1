using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerHealth : UnitHealth
{
    [SerializeField] private PlayerStats _playerStats;

    public override void ChangeCurrentHealth(float damageValue)
    {
        CurrentHealth += damageValue;
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
        int chanceEvasion = Random.Range(0, 100);

        if (chanceEvasion > _playerStats.Evasion)
        {
            if (_playerStats.Armor > 0)
                CurrentHealth -= damageValue / _playerStats.Armor;

            else
                CurrentHealth -= damageValue;
        }
        else
        {
            Debug.Log("evasion");
            return;
        }
    }

    public override void LifeSteal(float damageValue)
    {
        int chanceLifesteal = Random.Range(0, 100);

        if (chanceLifesteal > _playerStats.LifestealChance)
            return;

        else
        {
            if (CurrentHealth == MaxHealth)
                return;

            else
                CurrentHealth += damageValue * _playerStats.LifestealMultiply;
        }
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
