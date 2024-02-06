using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private RoundManager _roundManager;
    [SerializeField] private List<EntityStats> _enemyList = new List<EntityStats>();

    public RoundManager RoundManager => _roundManager;
    public Player player;

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
            Instantiate(_enemyList[0], transform.position, Quaternion.identity);
            //Instantiate(_enemyList[1], transform.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Instantiate(_enemyList[1], transform.position, Quaternion.identity);
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
        _playerStats.RefreshStats();
    }

}
