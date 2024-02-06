using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletEnemy : BulletData
{

    protected override void Start()
    {
        StartCoroutine(DestruyBulletByTime());
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
        _chanceCrit = stats.CritChance;
        _bulletSpeed = stats.BulletSpeed;
        _lifeTime = stats.LifeTime;
    }

    protected override void MoveBullet()
    {
        transform.Translate(Vector2.up * _bulletSpeed * Time.deltaTime);
    }

    protected override void HandleCollision(Collider2D collider)
    {
        if (collider.TryGetComponent(out IHealthChangeable damageable))
        {
            damageable.TakeUnitDamage(_damage);
        }

        Destroy(gameObject);
    }

    protected override IEnumerator DestruyBulletByTime()
    {
        yield return new WaitForSeconds(_lifeTime);

        Destroy(gameObject);
    }
}
