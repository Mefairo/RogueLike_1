using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class EnemiesRandomiser
{
    [SerializeField] private EntityStats[] _enemy;
    [SerializeField] private float _randomSpawn;

    public EntityStats[] Enemy => _enemy;
    public float RandomSpawn => _randomSpawn;
}
