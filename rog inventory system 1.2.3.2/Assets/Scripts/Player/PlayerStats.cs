using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;




    [Header("Health")]
    //public float maxHealth;
    //public float currentHealth;

    [Header("Defence")]
    public float armor;
    public float evasionRating;

    [Header("Damage")]
    [SerializeField] private Bullet _playerBullet;
    public float critDamage;
    public float chanceCrit;

    [Header("Gun Parameters")]
    public float speedBullet;
    public float disatanceBullet;
    public float lifeTimeBullet;
    public LayerMask solidSurface;

    [Header("Survival")]
    public float lifeSteal;
    public float chanceLifeSteal;
    //public float moveSpeed;
}
