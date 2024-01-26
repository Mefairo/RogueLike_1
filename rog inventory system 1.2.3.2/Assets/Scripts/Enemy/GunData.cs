using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunData : MonoBehaviour
{
    [SerializeField] protected float _offset;
    [SerializeField] protected Transform _shotPoint;
    //[SerializeField] protected Transform _bulletContainer;
    [Space]
    [SerializeField] protected BulletEnemy _bullet;
    [SerializeField] protected float _startTimeBtwShots;

    protected float _timeBtwShots;
    protected float _rotZ;
    protected Vector3 _difference;
    protected Player _player;

    protected virtual void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    protected virtual void Update()
    {
        DirectionForShoot();
        Shoot();
    }

    protected void Shoot()
    {
        if (_timeBtwShots <= 0)
        {
            BulletEnemy bullet_1 = Instantiate(_bullet, _shotPoint.position, _shotPoint.rotation);

            _timeBtwShots = _startTimeBtwShots;
        }

        else
            _timeBtwShots -= Time.deltaTime;
    }

    protected void DirectionForShoot()
    {
        _difference = _player.transform.position - transform.position;
        _rotZ = Mathf.Atan2(_difference.y, _difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, _rotZ + _offset);
    }
}
