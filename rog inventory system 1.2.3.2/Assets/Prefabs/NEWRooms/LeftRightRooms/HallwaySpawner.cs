using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwaySpawner : MonoBehaviour
{
    [SerializeField] private SpawnerWallsBtwRooms[] _pointSpawnWalls;

    private void Start()
    {
        EntranceHallwaySpawn();
    }

    private void EntranceHallwaySpawn()
    {
        SpawnerWallsBtwRooms.Direction[] directions;

        if (Mathf.Approximately(this.gameObject.transform.eulerAngles.z, 0f))
            directions = new SpawnerWallsBtwRooms.Direction[]
            {
                SpawnerWallsBtwRooms.Direction.Top, SpawnerWallsBtwRooms.Direction.Bottom, SpawnerWallsBtwRooms.Direction.Left, SpawnerWallsBtwRooms.Direction.Right
            };

        else if (Mathf.Approximately(this.gameObject.transform.eulerAngles.z, 90f))
            directions = new SpawnerWallsBtwRooms.Direction[]
            {
                SpawnerWallsBtwRooms.Direction.Left, SpawnerWallsBtwRooms.Direction.Right, SpawnerWallsBtwRooms.Direction.Bottom, SpawnerWallsBtwRooms.Direction.Top
            };

        else if (Mathf.Approximately(this.gameObject.transform.eulerAngles.z, 180f))
            directions = new SpawnerWallsBtwRooms.Direction[]
            {
                SpawnerWallsBtwRooms.Direction.Bottom, SpawnerWallsBtwRooms.Direction.Top, SpawnerWallsBtwRooms.Direction.Right, SpawnerWallsBtwRooms.Direction.Left
            };

        else if (Mathf.Approximately(this.gameObject.transform.eulerAngles.z, 270f))
            directions = new SpawnerWallsBtwRooms.Direction[]
            {
                SpawnerWallsBtwRooms.Direction.Right, SpawnerWallsBtwRooms.Direction.Left, SpawnerWallsBtwRooms.Direction.Top, SpawnerWallsBtwRooms.Direction.Bottom
            };
        else return;

        for (int i = 0; i < _pointSpawnWalls.Length; i++)
        {
            if (_pointSpawnWalls[i] != null && i < directions.Length)
            {
                _pointSpawnWalls[i]._direction = directions[i];
                _pointSpawnWalls[i].CheckPassages();
            }
        }
    }
}
