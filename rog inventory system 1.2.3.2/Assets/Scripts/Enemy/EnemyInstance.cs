using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyInstance
{
    public UnitStats _unitStats;
    public float maxHealth;
    public float currentHealth;

    public UnitStats UnitStats => _unitStats;

    public EnemyInstance(UnitStats unitStats)
    {
        _unitStats = unitStats;
        maxHealth = _unitStats.maxHealth;
    }

    public EnemyInstance()
    {

    }
}
