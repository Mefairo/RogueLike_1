using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapStandart : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float damageInterval;

    private bool _playerInTrap = false;
    private Player _player;
    private Coroutine _inflictDamageTrap;

    private void Awake()
    {
        _player = FindObjectOfType<Player>().GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInTrap = true;
            _inflictDamageTrap = StartCoroutine(InflictDamage(other));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInTrap = false;
            StopCoroutine(_inflictDamageTrap);
            _inflictDamageTrap = null;
        }
    }

    private IEnumerator InflictDamage(Collider2D collider)
    {
        while (_playerInTrap)
        {
            if (collider.TryGetComponent(out IHealthChangeable damageable))
            {
                damageable.TakeTrapDamage(damage);
            }

            yield return new WaitForSecondsRealtime(damageInterval);
        }
    }
}
