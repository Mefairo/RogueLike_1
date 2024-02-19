using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [Header("Player")]
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private Player _player;
    [Space]
    [Header("Entity")]
    [SerializeField] private List<EntityStats> _enemyList = new List<EntityStats>();
    [SerializeField] private List<EntityStats> _trapList = new List<EntityStats>();
    [Space]
    [Header("Manager")]
    [SerializeField] private RoundManager _roundManager;

    public RoundManager RoundManager => _roundManager;
    public Player Player => _player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }

        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ChangeStats();
    }

    private void OnEnable()
    {
        _roundManager.OnEnemyStatsChange += ChangeStats;
        _roundManager.OnFirstRoundStart += RefreshStats;
    }

    private void OnDisable()
    {
        _roundManager.OnEnemyStatsChange -= ChangeStats;
        _roundManager.OnFirstRoundStart -= RefreshStats;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //Instantiate(_enemyList[0], transform.position, Quaternion.identity);
            //Instantiate(_enemyList[1], transform.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            //Instantiate(_enemyList[1], transform.position, Quaternion.identity);
        }
    }

    private void ChangeStats()
    {
        foreach (var enemy in _enemyList)
        {
            enemy.NewRoundStats();
        }
    }

    private void RefreshStats()
    {
        foreach (var enemy in _enemyList)
        {
            enemy.RefreshStats();
            enemy.RefreshBonusStats();
        }

        foreach (var trap in _trapList)
        {
            trap.RefreshStats();
            trap.RefreshBonusStats();
        }

        _playerStats.RefreshStats();
    }

}
