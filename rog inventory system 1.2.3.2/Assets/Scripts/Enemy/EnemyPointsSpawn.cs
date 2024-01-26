using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPointsSpawn : MonoBehaviour
{
    [SerializeField] private EnemySpawner _subjectsSpawner;
    [SerializeField] private Transform[] _enemySpawnPoints;

    private void Awake()
    {
        _subjectsSpawner = FindObjectOfType<EnemySpawner>();
    }

    private void Start()
    {
        foreach (var point in _enemySpawnPoints)
        {
            _subjectsSpawner.subjectsSpawnPoints.Add(point);
        }
    }
}
