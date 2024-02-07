using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyRoundController : MonoBehaviour
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        //_enemy.Player.RoundManager.EnemiesOnScene.Add(this);
        EnemyManager.Instance.Player.RoundManager.EnemiesOnScene.Add(this);
    }

    private void OnDestroy()
    {
        //_enemy.Player.RoundManager.EnemiesOnScene.Remove(this);
        //_enemy.Player.RoundManager.ConditionsForNewRound();
        EnemyManager.Instance.Player.RoundManager.EnemiesOnScene.Remove(this);
        EnemyManager.Instance.Player.RoundManager.ConditionsForNewRound();

    }
}
