using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsSpawnPoints : MonoBehaviour
{
    [SerializeField] private ItemsSpawner _subjectsSpawner;
    [SerializeField] private Transform[] _itemsSpawnPoints;

    private void Awake()
    {
        _subjectsSpawner = FindObjectOfType<ItemsSpawner>();
    }

    private void Start()
    {
        foreach (var point in _itemsSpawnPoints)
        {
            _subjectsSpawner.subjectsSpawnPoints.Add(point);
        }
    }
}
