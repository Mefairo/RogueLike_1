using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunData : MonoBehaviour
{
    [SerializeField] protected float _offset;
    [SerializeField] protected Transform _shotPoint;
    [SerializeField] protected Transform _bulletContainer;
    [Space]
    [SerializeField] protected BulletData _bullet;
    [SerializeField] protected float _startTimeBtwShots;

    protected float _timeBtwShots;
    protected float _rotZ;
    protected Vector3 _difference;

    protected virtual void Update()
    {
        DirectionForShoot();
        Shoot();
    }

    protected virtual void Shoot()
    {
        if (_timeBtwShots <= 0)
        {
            BulletData bullet_1 = Instantiate(_bullet, _shotPoint.position, _shotPoint.rotation);

            _timeBtwShots = _startTimeBtwShots;
        }

        else
            _timeBtwShots -= Time.deltaTime;
    }

    protected abstract void DirectionForShoot();

}
