using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class UnitHealth : MonoBehaviour, IHealthChangeable
{
    [SerializeField] private HealthBar _healthBar;
    [Space]
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;

    public event Action<float> OnMaxHPChange;
    public event Action<float> OnCurrentHPChange;

    public float MaxHealth
    {
        get => _maxHealth;
        private set
        {
            _maxHealth = value;
            OnMaxHPChange?.Invoke(value);
        }
    }

    public float CurrentHealth
    {
        get => _currentHealth;
        private set
        {
            _currentHealth = value;
            OnCurrentHPChange?.Invoke(value);
            if (_currentHealth <= 0)
                CheckHealth(value);
        }
    }

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        CurrentHealth = _maxHealth;

        _healthBar.SetMaxHealth(_maxHealth);
        _healthBar.SetHealth(_currentHealth);
    }

    public void ChangeCurrentHealth(float damageValue)
    {
        CurrentHealth -= damageValue;
    }

    public void TakeTrapDamage(float damageValue)
    {
        CurrentHealth -= damageValue;
    }

    public void TakeUnitDamage(float damageValue)
    {
        CurrentHealth -= damageValue;
    }
    public void HealUnitDamage(float healValue)
    {
        CurrentHealth += healValue;
    }

    public void ChangeMaxHealth(float value)
    {
        MaxHealth += value;
    }

    private void CheckHealth(float health)
    {
        if (health <= 0)
            UnitDeath();
    }

    private void UnitDeath()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Destroy(gameObject);
    }
}
