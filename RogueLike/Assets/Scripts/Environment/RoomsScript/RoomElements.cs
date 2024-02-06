using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomElements : MonoBehaviour
{
    [SerializeField] public GameObject _room;
    [SerializeField] public GameObject _mainRoomPrefab;
    [SerializeField] public GameObject _walls;
    [SerializeField] private GameObject _hallwayPoints;
    [SerializeField] private GameObject _enemySpawnPoints;
    [SerializeField] private GameObject _traps;

    public GameObject Room => _room;
    public GameObject MainRoomPrefab => _mainRoomPrefab;
    public GameObject Walls => _walls;

}
