using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemsRandomiser
{
    [SerializeField] private ItemPickUp[] _item;
    [SerializeField] private float _randomSpawn;

    public ItemPickUp[] Item => _item;
    public float RandomSpawn => _randomSpawn;
}
