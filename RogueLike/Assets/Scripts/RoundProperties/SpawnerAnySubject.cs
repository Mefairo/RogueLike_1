using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnerAnySubject : MonoBehaviour
{
    [SerializeField] protected RoundTimer _timer;
    [SerializeField] protected GameObject[] _subjectSpawn;
    [SerializeField] protected float _speedSubjectSpawn;
    [SerializeField] protected EnemiesRandomiser _enemiesRandomer;

    public List<Transform> subjectsSpawnPoints;

    public GameObject[] SubjectSpawn => _subjectSpawn;

    protected void Start()
    {
        _timer.OnEnemySpawnerActive += StartSpawnEnemy;
    }

    protected void StartSpawnEnemy()
    {
        StartCoroutine(StartSpawnEnemyCor());
    }

    protected IEnumerator StartSpawnEnemyCor()
    {
        while (_timer.CurrentTime > 0)
        {
            Transform randomEnemyPosition = subjectsSpawnPoints[Random.Range(0, subjectsSpawnPoints.Count)];
            Instantiate(_subjectSpawn[0], randomEnemyPosition.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(_speedSubjectSpawn);
        }
    }
}
