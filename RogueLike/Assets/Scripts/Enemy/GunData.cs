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

    public Transform ShotPoint => _shotPoint;
    public Transform BulletContainer => _bulletContainer;
    public BulletData Bullet => _bullet;

    protected virtual void Update()
    {
        DirectionForShoot();
        Shoot();
    }

    public abstract void Shoot();

    protected abstract void DirectionForShoot();

}
