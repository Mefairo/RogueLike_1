using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomVariants : MonoBehaviour
{
    public GameObject[] topRooms;
    public GameObject[] bottomRooms;
    public GameObject[] rightRooms;
    public GameObject[] leftRooms;

    public GameObject key;
    public GameObject gun;

    [HideInInspector] public List<GameObject> rooms;

    //private void Start()
    //{
    //    StartCoroutine(RandomSpawner());
    //}
    //IEnumerator RandomSpawner()
    //{
    //    yield return new WaitForSeconds(4f);
    //    AddRoom lastRoom = rooms[rooms.Count - 1].GetComponent<AddRoom>();
    //    int rand1 = Random.Range(0, rooms.Count - 2);
    //    int rand2 = Random.Range(0, rooms.Count - 2);

    //    Instantiate(key, rooms[rand1].transform.position, Quaternion.identity);
    //    Instantiate(gun, rooms[rand2].transform.position, Quaternion.identity);

    //    lastRoom.door.SetActive(true);
    //    lastRoom.DestroyWalls();
    //}
}
