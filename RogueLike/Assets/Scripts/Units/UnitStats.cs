using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitStats: MonoBehaviour, IStatsChangeable, IDefensable
{
    [Header("Current Stats")]
    [SerializeField] protected float _damage;
    [SerializeField] protected float _critMultiply;
    [SerializeField] protected float _critChance;
    [SerializeField] protected float _startTimeBtwShots;
    [SerializeField] protected float _bulletSpeed;
    [SerializeField] protected float _lifeTime;
    [SerializeField] protected float _armor;
    [SerializeField] protected float _evasion;
    [Space(20)]
    [Header("Initial Stats")]
    [SerializeField] protected float _initialDamage;
    [SerializeField] protected float _initialCritMultiply;
    [SerializeField] protected float _initialCritChance;
    [SerializeField] protected float _initialStartTimeBtwShots;
    [SerializeField] protected float _initialBulletSpeed;
    [SerializeField] protected float _initialLifeTime;
    [SerializeField] protected float _initialArmor;
    [SerializeField] protected float _initialEvasion;
    [Space(20)]
    [Header("Bonus Stats")]
    [SerializeField] protected float _bonusDamage;
    [SerializeField] protected float _bonusCritMultiply;
    [SerializeField] protected float _bonusCritChance;
    [SerializeField] protected float _bonusAttackSpeed;
    [SerializeField] protected float _bonusBulletSpeed;
    [SerializeField] protected float _bonusLifetime;
    [SerializeField] protected float _bonusArmor;
    [SerializeField] protected float _bonusEvasion;

    public float Damage
    {
        get => _damage;
        protected set
        {
            _damage = value;
            //OnMaxHPChange?.Invoke(value);
        }
    }
    public float CritMultiply
    {
        get => _critMultiply;
        protected set
        {
            _critMultiply = value;
            //OnMaxHPChange?.Invoke(value);
        }
    }
    public float CritChance
    {
        get => _critChance;
        protected set
        {
            _critChance = value;
            //OnMaxHPChange?.Invoke(value);
        }
    }
    public float StartTimeBtwShots
    {
        get => _startTimeBtwShots;
        protected set
        {
            _startTimeBtwShots = value;
            //OnMaxHPChange?.Invoke(value);
        }
    }
    public float BulletSpeed
    {
        get => _bulletSpeed;
        protected set
        {
            _bulletSpeed = value;
            //OnMaxHPChange?.Invoke(value);
        }
    }
    public float LifeTime
    {
        get => _lifeTime;
        protected set
        {
            _lifeTime = value;
            //OnMaxHPChange?.Invoke(value);
        }
    }
    public float Armor
    {
        get => _armor;
        protected set
        {
            _armor = value;
            //OnArmorChange?.Invoke(value);
        }
    }
    public float Evasion
    {
        get => _evasion;
        protected set
        {
            _evasion = value;
            //OnEvasionChange?.Invoke(value);
        }
    }



    public abstract void ChangeDamage(float damage);
    public abstract void ChangeCritMultiply(float critMuliply);
    public abstract void ChangeCritChance(float critChance);
    public abstract void ChangeAttackSpeed(float attackSpeed);
    public abstract void ChangeBulletSpeed(float bulletSpeed);
    public abstract void ChangeLifeTime(float lifetime);
    public abstract void ChangeArmor(float armor);
    public abstract void ChangeEvasion(float evasion);


    public  void RefreshStats()
    {
        Damage = _initialDamage;
        CritMultiply = _initialCritMultiply;
        CritChance = _initialCritChance;
        StartTimeBtwShots = _initialStartTimeBtwShots;
        BulletSpeed = _initialBulletSpeed;
        LifeTime = _initialLifeTime;
        Armor = _initialArmor;
        Evasion = _initialEvasion;
    }
    public  void RefreshBonusStats()
    {
        _bonusDamage = 0;
        _bonusCritMultiply = 0;
        _bonusCritChance = 0;
        _bonusAttackSpeed = 0;
        _bonusBulletSpeed = 0;
        _bonusLifetime = 0;
        _bonusArmor = 0;
        _bonusEvasion = 0;
    }
}
