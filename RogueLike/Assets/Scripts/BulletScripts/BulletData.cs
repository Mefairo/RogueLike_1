using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public abstract class BulletData : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] protected float _damage;
    [SerializeField] protected float _critMultiply;
    [SerializeField] protected float _chanceCrit;

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

    protected abstract void MoveBullet();

    protected void CheckCollision()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, _distance, _whatIsSolid);

        if (hitInfo.collider != null)
            HandleCollision(hitInfo.collider);
    }

    protected abstract void HandleCollision(Collider2D collider);

    protected abstract IEnumerator DestruyBulletByTime();
}
