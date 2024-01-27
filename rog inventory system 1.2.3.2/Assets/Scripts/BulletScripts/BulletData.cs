using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class BulletData : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private float _damage;
    [SerializeField] private float _critMultiply;
    [SerializeField] private float _chanceCrit;

    [Header("Lifetime")]
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _distance;

    [Header("Other")]
    [SerializeField] private LayerMask _whatIsSolid;

    private void Start()
    {
        StartCoroutine(DestruyBulletByTime());
    }

    private void Update()
    {
        MoveBullet();

        CheckCollision();
    }

    private void MoveBullet()
    {
        transform.Translate(Vector2.up * _bulletSpeed * Time.deltaTime);
    }

    private void CheckCollision()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, _distance, _whatIsSolid);

        if (hitInfo.collider != null)
            HandleCollision(hitInfo.collider);
    }

    private void HandleCollision(Collider2D collider)
    {
        if (collider.TryGetComponent(out IHealthChangeable damageable))
        {
            damageable.TakeUnitDamage(_damage);
        }

        Destroy(gameObject);
    }

    private IEnumerator DestruyBulletByTime()
    {
        yield return new WaitForSeconds(_lifeTime);

        Destroy(gameObject);
    }
}
