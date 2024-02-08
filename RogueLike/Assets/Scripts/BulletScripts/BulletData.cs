using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Events;

public abstract class BulletData : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] protected float _damage;
    [SerializeField] protected float _critMultiply;
    [SerializeField] protected float _critChance;

    [Header("Lifetime")]
    [SerializeField] protected float _bulletSpeed;
    [SerializeField] protected float _lifeTime;
    [SerializeField] protected float _distance;
    [SerializeField] protected LayerMask _whatIsSolid;

    protected abstract void Start();

    protected virtual void Update()
    {
        CheckCollision();
    }

    public void InitializeBullet(UnitStats stats)
    {
        _damage = stats.Damage;
        _critMultiply = stats.CritMultiply;
        _critChance = stats.CritChance;
        _bulletSpeed = stats.BulletSpeed;
        _lifeTime = stats.LifeTime;      
    }

    protected abstract void MoveBullet();

    protected virtual void CheckCollision()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, _distance, _whatIsSolid);

        if (hitInfo.collider != null)
            HandleCollision(hitInfo.collider);
    }

    protected float SetDamage()
    {
        int critChance = Random.Range(0, 100);

        if (critChance > _critChance)
            return _damage;

        else
        {
            float damage = _damage * _critMultiply;
            return damage;
        }
    }

    protected abstract void HandleCollision(Collider2D collider);

    protected abstract IEnumerator DestroyBulletByTime();
}
