using Assets.Scripts.Environment.TrapsScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTrap : GunData
{
    private Trap _trap;

    private void Awake()
    {
        _trap = GetComponent<Trap>();
    }

    protected override void Update()
    {
        Shoot();
    }

    protected override void DirectionForShoot() { }

    protected override void Shoot()
    {
        if (_timeBtwShots <= 0)
        {
            BulletData bullet_1 = Instantiate(_bullet, _shotPoint.position, _shotPoint.rotation);
            BulletTrap bulletTrap = bullet_1.GetComponent<BulletTrap>();

            if (bulletTrap != null)
                bulletTrap.InitializeBullet(_trap.TrapStats);

            _timeBtwShots = _startTimeBtwShots;
        }

        else
            _timeBtwShots -= Time.deltaTime;
    }
}
