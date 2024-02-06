using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MillTrap : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    //private void Update()
    //{
    //    transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
    //}

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
    }
}
