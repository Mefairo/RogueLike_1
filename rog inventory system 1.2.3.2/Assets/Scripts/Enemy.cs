using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject floatingDamage;

    public float maxHealth;
    public float currentHealth;
    public HealthBar healthBar;
    //public float speed;
    public float damage;
    public GameObject effectDeath;

    private Player player;
    private AddRoom room;

    [SerializeField] private Transform target;
    private NavMeshAgent agent;


    private void Start()
    {
        player = FindObjectOfType<Player>();
        room = GetComponentInParent<AddRoom>();
        maxHealth = currentHealth;
        healthBar.SetMaxHealth(maxHealth);

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Instantiate(effectDeath, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
            //room.enemies.Remove(gameObject);
        }
        if (player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (agent != null && agent.isActiveAndEnabled)
        {
            agent.SetDestination(target.position);
        }
        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        Vector2 damagePos = new Vector2(transform.position.x, transform.position.y + 2.75f);
        Instantiate(floatingDamage, damagePos, Quaternion.identity);
        floatingDamage.GetComponentInChildren<FloatingDamage>().damage = damage;
    }
}
