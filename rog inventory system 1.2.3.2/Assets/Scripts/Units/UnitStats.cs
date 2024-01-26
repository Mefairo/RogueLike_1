using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies Data/New Enemy")]
public class UnitStats: ScriptableObject
{
    [Header("Health")]
    public float maxHealth;
    public float currentHealth;

    //[Header("Defence")]
    //public float armor;
    //public float evasionRating;

    //[Header("Damage")]
    //public float damageGun;
    //public float critDamage;
    //public float chanceCrit;

    //[Header("Gun Parameters")]
    //public float speedBullet;
    //public float disatanceBullet;
    //public float lifeTimeBullet;
    //public LayerMask solidSurface;

    //[Header("Survival")]
    //public float moveSpeed;
    //public float lifeSteal;
    //public float chanveLifeSteal;

}
