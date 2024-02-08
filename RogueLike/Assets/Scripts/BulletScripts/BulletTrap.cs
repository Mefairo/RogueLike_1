using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletTrap : BulletData
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

    protected override void HandleCollision(Collider2D collider)
    {
        float damage = SetDamage();

        if (collider.TryGetComponent(out IHealthChangeable damageable))
        {
            damageable.TakeUnitDamage(damage);
            //_enemy..LifeSteal(damage);
        }

        Destroy(gameObject);
    }
}
