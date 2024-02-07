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
        if (_timeBtwShots <= 0)
        {
            BulletData bullet_1 = Instantiate(_bullet, _shotPoint.position, _shotPoint.rotation);
            BulletEnemy bulletEnemy = bullet_1.GetComponent<BulletEnemy>();

            if (bulletEnemy != null)
                bulletEnemy.InitializeBullet(_enemy.EnemyStats);

            _timeBtwShots = _enemy.EnemyStats.StartTimeBtwShots;
        }

        else
            _timeBtwShots -= Time.deltaTime;
    }

    protected override void DirectionForShoot()
    {
        //_difference = _enemy.Player.transform.position - transform.position;
        _difference = EnemyManager.Instance.Player.transform.position - transform.position;
        _rotZ = Mathf.Atan2(_difference.y, _difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, _rotZ + _offset);
    }
}
