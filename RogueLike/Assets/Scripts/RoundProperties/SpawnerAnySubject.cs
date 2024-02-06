using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnerAnySubject : MonoBehaviour
{
    [SerializeField] private RoundTimer _timer;
    [SerializeField] private GameObject _subjectSpawn;
    [SerializeField] private float _speedSubjectSpawn;

    public List<Transform> subjectsSpawnPoints;

    public GameObject SubjectSpawn => _subjectSpawn;

    private void Start()
    {
        _timer.OnEnemySpawnerActive += StartSpawnEnemy;
    }

    private void StartSpawnEnemy()
    {
        StartCoroutine(StartSpawnEnemy1());
    }

    private IEnumerator StartSpawnEnemy1()
    {
        while (_timer.CurrentTime > 0)
        {
            Debug.Log("spawnEnemy");

            Transform randomEnemyPosition = subjectsSpawnPoints[Random.Range(0, subjectsSpawnPoints.Count)];

            Instantiate(_subjectSpawn, randomEnemyPosition.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(_speedSubjectSpawn);
        }
    }
}
