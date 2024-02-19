using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBullet_1 : BulletPlayer
{
    [SerializeField] protected LayerMask _targetDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ((_targetDamage.value & (1 << collision.gameObject.layer)) > 0)
        {
            float damage = SetDamage();

            collision.gameObject.TryGetComponent(out IHealthChangeable health);
            health.TakeUnitDamage(damage);
            _player.PlayerHealth.LifeSteal(damage);
        }
    }

    protected override void HandleCollision(Collider2D collider)
    {
        Destroy(gameObject);
    }
}
