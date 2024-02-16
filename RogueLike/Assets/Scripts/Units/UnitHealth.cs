using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class UnitHealth : MonoBehaviour, IHealthChangeable
{
    [SerializeField] protected HealthBar _healthBar;
    [Space]
    [SerializeField] protected float _maxHealth;
    [SerializeField] protected float _currentHealth;

    public event Action<float> OnMaxHPChange;
    public event Action<float> OnCurrentHPChange;

    public float MaxHealth
    {
        get => _maxHealth;
        protected set
        {
            _maxHealth = value;
            OnMaxHPChange?.Invoke(value);
            OnCurrentHPChange?.Invoke(_currentHealth);
        }
    }

    public float CurrentHealth
    {
        get => _currentHealth;
        protected set
        {
            _currentHealth = Mathf.Clamp(value, 0f, _maxHealth);
            OnCurrentHPChange?.Invoke(_currentHealth);

            if (_currentHealth <= 0)
                CheckHealth(value);
        }
    }

    protected virtual void Awake()
    {
        Initialize();
    }

    protected void Initialize()
    {
        CurrentHealth = _maxHealth;

        _healthBar.SetMaxHealth(_maxHealth);
        _healthBar.SetHealth(_currentHealth);
    }

    public abstract void ChangeCurrentHealth(float damageValue);
    public abstract void TakeTrapDamage(float damageValue);
    public abstract void TakeUnitDamage(float damageValue);
    public abstract void HealUnitDamage(float healValue);
    public abstract void ChangeMaxHealth(float value);
    public abstract void LifeSteal(float value);

    protected abstract void CheckHealth(float health);
    protected abstract void UnitDeath();
}
