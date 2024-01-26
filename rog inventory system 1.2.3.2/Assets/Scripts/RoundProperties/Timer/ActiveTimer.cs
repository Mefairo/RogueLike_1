using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NavMeshPlus.Components;

public class ActiveTimer : MonoBehaviour
{
    [SerializeField] private RoundTimer _roundTimer;

    private void Awake()
    {
        _roundTimer = FindObjectOfType<RoundTimer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _roundTimer.StartTimer();
            this.gameObject.SetActive(false);
        }
    }
}
