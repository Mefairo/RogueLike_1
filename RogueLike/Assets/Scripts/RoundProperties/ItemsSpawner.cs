using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemsSpawner : SpawnerAnySubject
{
    [SerializeField] protected List<ItemsRandomiser> _itemsRandomer;

    protected override IEnumerator StartSpawnEnemyCor()
    {
        while (_timer.CurrentTime > 0)
        {
            ItemPickUp newItem = GetRandomItem();

            Transform randomEnemyPosition = subjectsSpawnPoints[Random.Range(0, subjectsSpawnPoints.Count)];
            Instantiate(newItem, randomEnemyPosition.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(_speedSubjectSpawn);
        }
    }

    private ItemPickUp GetRandomItem()
    {
        float totalGroupChance = Random.Range(0, 101);

        for (int i = 0; i < _itemsRandomer.Count; i++)
        {
            if (totalGroupChance < _itemsRandomer[i].RandomSpawn)
            {
                var randomItemIndex = Random.Range(0, _itemsRandomer[i].Item.Length);
                Debug.Log(randomItemIndex);
                return _itemsRandomer[i].Item[randomItemIndex];
            }
        }

        return null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            //GetRandomItem();
        }
    }
}
