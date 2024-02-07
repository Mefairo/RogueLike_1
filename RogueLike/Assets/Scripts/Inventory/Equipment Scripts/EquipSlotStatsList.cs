using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EquipSlotStatsList
{
    [SerializeField] private PlayerStatsEnum _stats;
    [SerializeField] private float[] _valueStat;

    public PlayerStatsEnum Stats => _stats;
    public float[] ValueStat => _valueStat;
}
