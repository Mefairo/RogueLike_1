using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Camera cam;
    private void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        Vector3 camPosition = cam.transform.position;
        camPosition = new Vector3 (target.position.x, target.position.y, -1);
        cam.transform.position = camPosition;
    }
}

