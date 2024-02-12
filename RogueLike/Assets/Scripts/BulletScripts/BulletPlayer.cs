using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletPlayer : BulletData
{
    protected Player _player;

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
            _player.PlayerHealth.LifeSteal(damage);
        }

        Destroy(gameObject);
    }

    public void InitOwner(Player player)
    {
        _player = player;
    }
}
