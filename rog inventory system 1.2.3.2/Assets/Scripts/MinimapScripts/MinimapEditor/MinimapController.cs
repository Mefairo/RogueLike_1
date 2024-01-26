using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    [SerializeField] private GameObject _mapWindow;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !_mapWindow.activeSelf)
            _mapWindow.SetActive(true);
    }
}
