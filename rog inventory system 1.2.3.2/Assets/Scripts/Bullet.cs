using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Damage")]
    public float damage;
    public float critDamage;
    public float chanceCrit;

    [Header("Lifetime")]
    public float bulletSpeed;
    public float lifeTime;
    public float distance;

    [Header("Other")]
    [SerializeField] private LayerMask whatIsSolid;
    [SerializeField] private GameObject effect;

    [SerializeField] bool enemyBullet;

    private Enemy _enemy;

    private void Start()
    {
        Invoke("DestroyBullet", lifeTime);
    }

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                var enemyData = hitInfo.collider.GetComponent<EnemyData>();
                var enemy = hitInfo.collider.GetComponent<Enemy>();
                {
                    if (enemyData != null)
                    {
                        enemyData.TakeDamage(damage);
                    }
                    if (enemy != null)
                    {
                        enemy.TakeDamage(damage);
                    }
                }
                //hitInfo.collider.GetComponent<EnemyData>().TakeDamage(damage);
            }
            if (hitInfo.collider.CompareTag("Player") && enemyBullet)
            {
                //hitInfo.collider.GetComponent<Player>().TakeDamage(damage, 0);

                if (hitInfo.collider.TryGetComponent(out IHealthChangeable healthChangeable))
                {
                    healthChangeable.TakeUnitDamage(damage);
                }
                //hitInfo.collider.GetComponent<Player>().TakeDamage(_enemy.damage);
            }
            DestroyBullet();
        }
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);

    }

    public void DestroyBullet()
    {
        //Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
