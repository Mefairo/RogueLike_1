using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnerAnySubject : MonoBehaviour
{
    [SerializeField] protected RoundTimer _timer;
    [SerializeField] protected float _speedSubjectSpawn;

    public List<Transform> subjectsSpawnPoints;

    protected void Start()
    {
        _timer.OnEnemySpawnerActive += StartSpawnEnemy;
    }

    protected void StartSpawnEnemy()
    {
        StartCoroutine(StartSpawnEnemyCor());
    }

    protected abstract IEnumerator StartSpawnEnemyCor();
}
