using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletEnemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float distance;
    [SerializeField] private float damage;
    [SerializeField] private LayerMask whatIsSolid;
    [SerializeField] private GameObject effect;

    [SerializeField] bool enemyBullet;

    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private DebuffManager _debuffManager;
    [SerializeField] private EnemyManager _enemyManager;

    private void Start()
    {
        //_debuffManager = FindObjectOfType<DebuffManager>().GetComponent<DebuffManager>();
        //_enemyManager = FindObjectOfType<EnemyManager>().GetComponent<EnemyManager>();
        Invoke("DestroyBullet", lifeTime);
    }

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Player") && enemyBullet)
            {
                //hitInfo.collider.GetComponent<Player>().TakeDamage(_enemyData.Damage, 0);
                //hitInfo.collider.GetComponent<Player>().TakeDamage(_enemyManager._damageEnemy[0], 0);

                if (hitInfo.collider.TryGetComponent(out IHealthChangeable healthChangeable))
                {
                    healthChangeable.TakeUnitDamage(damage);
                }
                //_debuffManager.ApplyDebuff(_enemyData.StatusData);
            }
            DestroyBullet();
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    public void DestroyBullet()
    {
        //Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
