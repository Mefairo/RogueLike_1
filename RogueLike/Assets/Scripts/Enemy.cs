using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyController _controller;
    [SerializeField] private EnemyGun _gun;
    [SerializeField] private EnemyRoundController _enemyRoundController;
    [SerializeField] private EntityStats _enemyStats;

    public EnemyGun Gun => _gun;
    public EntityStats EnemyStats => _enemyStats;
}
