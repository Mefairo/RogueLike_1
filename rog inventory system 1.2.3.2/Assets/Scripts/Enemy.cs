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
    [Space]
    [SerializeField] private Player _player;

    public Player Player => _player;

    private void Awake()
    {
        _player = FindAnyObjectByType<Player>();
    }
}
