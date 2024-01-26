using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerRoom : MonoBehaviour
{
    [Header("Rooms")]
    [SerializeField] private HeadOfRoom[] _roomsLeftRight;
    [SerializeField] private HeadOfRoom[] _roomsSide;
    [SerializeField] private HeadOfRoom[] _roomsCenter;
    [SerializeField] private HeadOfRoom[] _roomsEntrance;
    [SerializeField] private Transform _roomsContainer;

    [Header("RoomPoints")]
    [SerializeField] private Transform[] _pointSpawnLeftRight;
    [SerializeField] private Transform[] _pointSide;
    [SerializeField] private Transform[] _pointCenter;
    [SerializeField] private Transform[] _pointEntrance;

    [Header("WallPoints")]
    [SerializeField] private SpawnerWallsBtwRooms[] _pointSpawnWalls;

    [Header("Other")]
    [SerializeField] private RoundManager _roundManager;


    private void Awake()
    {
        _pointSpawnWalls = GetComponentsInChildren<SpawnerWallsBtwRooms>();

        RoomSpawn();
    }

    private void OnEnable()
    {
        _roundManager.OnNewRoundStart += RoomSpawn;
    }

    private void OnDisable()
    {
        _roundManager.OnNewRoundStart -= RoomSpawn;
    }

    public void RoomSpawn()
    {
        SpawnRoomEntrance();
        SpawnRoomCenter();
        SpawnRoomLeftRight();
        SpawnRoomSide();
    }

    private void SpawnRoomLeftRight()
    {
        for (int i = 0; i < _pointSpawnLeftRight.Length; i++)
        {
            HeadOfRoom randomRoom = _roomsLeftRight[Random.Range(0, _roomsLeftRight.Length)];

            switch (i)
            {
                case 0:
                    GameObject room_1 = Instantiate(randomRoom.gameObject, _pointSpawnLeftRight[i].position, Quaternion.Euler(0f, 0f, 0f));
                    room_1.transform.parent = _roomsContainer.transform;

                    break;

                case 1:
                    GameObject room_2 = Instantiate(randomRoom.gameObject, _pointSpawnLeftRight[i].position, Quaternion.Euler(0f, 0f, 90f));
                    room_2.transform.parent = _roomsContainer.transform;

                    break;

                case 2:
                    GameObject room_3 = Instantiate(randomRoom.gameObject, _pointSpawnLeftRight[i].position, Quaternion.Euler(0f, 0f, 180f));
                    room_3.transform.parent = _roomsContainer.transform;

                    break;
            }


        }

        foreach (var pointSpawn in _pointSpawnWalls)
        {
            pointSpawn.CheckPassages();
        }
    }

    private void SpawnRoomSide()
    {
        for (int i = 0; i < _pointSide.Length; i++)
        {
            HeadOfRoom randomRoom = _roomsSide[Random.Range(0, _roomsSide.Length)];

            switch (i)
            {
                case 0:
                    GameObject room_1 = Instantiate(randomRoom.gameObject, _pointSide[i].position, Quaternion.Euler(0f, 0f, 0f));
                    room_1.transform.parent = _roomsContainer.transform;

                    break;

                case 1:
                    GameObject room_2 = Instantiate(randomRoom.gameObject, _pointSide[i].position, Quaternion.Euler(0f, 0f, 90f));
                    room_2.transform.parent = _roomsContainer.transform;

                    break;

                case 2:
                    GameObject room_3 = Instantiate(randomRoom.gameObject, _pointSide[i].position, Quaternion.Euler(0f, 0f, 180f));
                    room_3.transform.parent = _roomsContainer.transform;

                    break;

                case 3:
                    GameObject room_4 = Instantiate(randomRoom.gameObject, _pointSide[i].position, Quaternion.Euler(0f, 0f, 270f));
                    room_4.transform.parent = _roomsContainer.transform;

                    break;
            }
        }
    }

    private void SpawnRoomCenter()
    {
        HeadOfRoom randomRoom = _roomsCenter[Random.Range(0, _roomsCenter.Length)];

        GameObject room_1 = Instantiate(randomRoom.gameObject, _pointCenter[0].position, Quaternion.identity);
        room_1.transform.parent = _roomsContainer.transform;


    }

    private void SpawnRoomEntrance()
    {
        HeadOfRoom randomRoom = _roomsEntrance[Random.Range(0, _roomsEntrance.Length)];

        GameObject room_1 = Instantiate(randomRoom.gameObject, _pointEntrance[0].position, Quaternion.identity);
        room_1.transform.parent = _roomsContainer.transform;


    }
}
