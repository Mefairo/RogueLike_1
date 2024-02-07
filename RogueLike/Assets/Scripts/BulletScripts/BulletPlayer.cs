using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletPlayer : BulletData
{
    protected override void Start()
    {
        StartCoroutine(DestroyBulletByTime());
    }

    protected override void Update()
    {
        MoveBullet();

        base.Update();
    }

    public void InitializeBullet(PlayerStats stats)
    {
        _damage = stats.Damage;
        _critMultiply = stats.CritMultiply;
        _critChance = stats.CritChance;
        _bulletSpeed = stats.BulletSpeed;
        _lifeTime = stats.LifeTime;
    }

    protected override void MoveBullet()
    {
        transform.Translate(Vector2.up * _bulletSpeed * Time.deltaTime);
    }

    protected override IEnumerator DestroyBulletByTime()
    {
        yield return new WaitForSeconds(_lifeTime);

        Destroy(gameObject);
    }
}
