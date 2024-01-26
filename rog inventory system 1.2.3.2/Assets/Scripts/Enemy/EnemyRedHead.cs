using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyRedHead : EnemyData
{
    public EnemyRedHead(EnemyData enemyData)
    {
        damage = enemyData.damage;
    }

    protected override void Start()
    {
        base.Start();
    }
}
