using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : GunData
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
    }

    protected override void Shoot()
    {
       base.Shoot();
    }

    protected override void DirectionForShoot()
    {
        _difference = _enemy.Player.transform.position - transform.position;
        _rotZ = Mathf.Atan2(_difference.y, _difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, _rotZ + _offset);
    }
}
