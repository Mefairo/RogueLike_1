using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWhiteAttack : MonoBehaviour
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out IHealthChangeable damageable))
                damageable.TakeUnitDamage(_enemy.EnemyStats.Damage);
        }
    }
}
