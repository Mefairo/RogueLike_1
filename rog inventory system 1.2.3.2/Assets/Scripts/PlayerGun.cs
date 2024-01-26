using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private float _offset;
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private Transform _bulletContainer;
    [Space]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _startTimeBtwShots;

    private float _timeBtwShots;
    private float _rotZ;
    private Vector3 _difference;

    private void Update()
    {
        DirectionForShoot();
        Shoot();
    }

    private void Shoot()
    {
        if (_timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                GameObject bullet_1 = Instantiate(_bullet, _shotPoint.position, _shotPoint.rotation, _bulletContainer.transform);

                _timeBtwShots = _startTimeBtwShots;
            }                
        }
        else
            _timeBtwShots -= Time.deltaTime;       
    }

    private void DirectionForShoot()
    {
        _difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _rotZ = Mathf.Atan2(_difference.y, _difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, _rotZ + _offset);       
    }
}
