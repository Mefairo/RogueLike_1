using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    public Vector3 cameraChangePos;
    public Vector3 playerChangePos;
    public Camera cam;
    private bool canChange = true;

    private void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        canChange = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canChange && other.CompareTag("Player"))
        {
            ChangedRoom(other.gameObject);
            other.transform.position += playerChangePos;
            cam.transform.position += cameraChangePos;
        }
    }

    private void ChangedRoom(GameObject changeRoom)
    {
        CapsuleCollider2D colliderRoom = changeRoom.GetComponent<CapsuleCollider2D>();
        colliderRoom.isTrigger = false;
        canChange = false;
    }
}
