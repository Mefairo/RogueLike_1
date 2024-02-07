using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletTrap : BulletData
{
    public TagValueType _tag;

    protected override void Start()
    {
        StartCoroutine(DestroyBulletByTime());
    }

    protected override void Update()
    {
        MoveBullet();

        base.Update();
    }

    public void InitializeBullet(EntityStats stats)
    {
        _damage = stats.Damage;
        _critMultiply = stats.CritMultiply;
        _critChance = stats.CritChance;
        _bulletSpeed = stats.BulletSpeed;
        _lifeTime = stats.LifeTime;
    }

    protected override void CheckCollision()
    {
        base.CheckCollision();
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
