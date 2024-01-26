using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepulsiveLogic : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _playerRB;
    [SerializeField] private float _repelForce;

    private Player _player;

    private void Awake()
    {
        _player = FindObjectOfType<Player>().GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector2 repelDiraction = (_player.PlayerController.RBPlayer.transform.position - transform.position).normalized;
            _player.PlayerController.RBPlayer.AddForceAtPosition(repelDiraction * _repelForce, transform.position);
        }

    }
}
