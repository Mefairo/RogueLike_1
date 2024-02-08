using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;


public class BulletEnemy : BulletData
{
    private Enemy _enemy;

    protected override void Start()
    {
        StartCoroutine(DestroyBulletByTime());
    }

    protected override void Update()
    {
        MoveBullet();

        base.Update();
    }

    protected override void MoveBullet()
    {
        transform.Translate(Vector2.up * _bulletSpeed * Time.deltaTime);
    }

    public void InitOwner(Enemy enemy)
    {
        _enemy = enemy;
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
            _enemy.Health.LifeSteal(damage);
        }

        Destroy(gameObject);
    }
}
