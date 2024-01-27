using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public abstract class EnemyData : MonoBehaviour
{
    [Header("General Stats")]
    [SerializeField] protected Player _target;
    [SerializeField] protected StatusEffectsData _statusData;
    [SerializeField] protected UnitStats _unitStats;
    [SerializeField] protected PlayerStats _playerStats;
    [SerializeField] protected RoundManager _roundManager;

    [Header("Damage Stats")]
    public float startDamage;
    public float damage;

    public EnemyInstance enemyInstance;

    protected Player _player;

    protected NavMeshAgent agent;

    public float Damage => damage;
    public StatusEffectsData StatusData => _statusData;

    public UnityAction OnEnemyDead;

    public Player Target => _target;

    private void Awake()
    {
        enemyInstance = new EnemyInstance(_unitStats);
    }

    protected virtual void Start()
    {
        _target = FindObjectOfType<Player>().GetComponent<Player>();
        _playerStats = FindObjectOfType<PlayerStats>().GetComponent<PlayerStats>();
        _player = FindObjectOfType<Player>().GetComponent<Player>();
        //_statusData = GetComponent<StatusEffectsData>();
        _roundManager = FindObjectOfType<RoundManager>().GetComponent<RoundManager>();
        //_roundManager.EnemiesOnScene.Add(this);

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    protected void Update()
    {
        if (_playerStats.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (agent != null && agent.isActiveAndEnabled)
        {
            agent.SetDestination(_target.transform.position);
        }
    }
    protected void OnDestroy()
    {
        //_roundManager.EnemiesOnScene.Remove(this);

        //OnEnemyDead?.Invoke();
        _roundManager.ConditionsForNewRound();

    }

    public void TakeDamage(float damage)
    {
        float critChance = Random.Range(0, 100);
        float lifeStealChance = Random.Range(0, 100);

        if (critChance < _playerStats.chanceCrit)
            damage *= _playerStats.critDamage;

        if (lifeStealChance < _playerStats.chanceLifeSteal)
            _player.LifeStealDamage(damage);

    }

    public void ChangeStats(float damage111)
    {
        damage -= damage111;
    }
}
