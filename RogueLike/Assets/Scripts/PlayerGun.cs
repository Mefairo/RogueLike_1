using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : GunData
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    protected override void DirectionForShoot()
    {
        _difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _rotZ = Mathf.Atan2(_difference.y, _difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, _rotZ + _offset);
    }

    protected override void Shoot()
    {
        if (_timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                BulletData bullet_1 = Instantiate(_bullet, _shotPoint.position, _shotPoint.rotation, _bulletContainer.transform);
                BulletPlayer bulletPlayer = bullet_1.GetComponent<BulletPlayer>();

                if (bulletPlayer != null)
                {
                    bulletPlayer.InitializeBullet(_player.PlayerStats);
                    bulletPlayer.InitOwner(_player);
                }

                _timeBtwShots = _player.PlayerStats.StartTimeBtwShots;
            }
        }
        else
            _timeBtwShots -= Time.deltaTime;
    }
}
