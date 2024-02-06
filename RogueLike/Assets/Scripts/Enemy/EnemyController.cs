using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IMoveable
{
    private Enemy _enemy;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _agent = GetComponent<NavMeshAgent>();

        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        if (_agent != null && _agent.isActiveAndEnabled)
            _agent.SetDestination(EnemyManager.Instance.player.transform.position);
            //_agent.SetDestination(_enemy.Player.transform.position);
    }

    public void ChangeMoveSpeed(float speed)
    {
        _agent.speed += speed;
    }
}
