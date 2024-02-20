using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class EnemySpawner : SpawnerAnySubject
{
    [SerializeField] protected List<EnemiesRandomiser> _enemiesRandomer;

    protected override IEnumerator StartSpawnEnemyCor()
    {
        while (_timer.CurrentTime > 0)
        {
            EntityStats newEnemy = GetRandomEnemy();

            Transform randomEnemyPosition = subjectsSpawnPoints[Random.Range(0, subjectsSpawnPoints.Count)];
            Instantiate(newEnemy, randomEnemyPosition.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(_speedSubjectSpawn);
        }
    }

    private EntityStats GetRandomEnemy()
    {
        float totalGroupChance = Random.Range(0, 101);

        for (int i = 0; i < _enemiesRandomer.Count; i++)
        {
            if (totalGroupChance < _enemiesRandomer[i].RandomSpawn)
            {
                var randomItemIndex = Random.Range(0, _enemiesRandomer[i].Enemy.Length);
                return _enemiesRandomer[i].Enemy[randomItemIndex];
            }
        }

        return null;
    }
}
