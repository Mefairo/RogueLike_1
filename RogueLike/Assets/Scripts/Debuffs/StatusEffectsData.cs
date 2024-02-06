using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Status Effects System/New status effect")]
public class StatusEffectsData : ScriptableObject
{
    public string ID;
    public string Name;
    [TextArea(4, 4)]
    public string Description;
    public Sprite Icon;
    public float Duration;
    public float VOTAmount;    // Value Over Time Amount
    public float TickSpeed;
    public int TickAmount;
    public bool Stackable;
    public int AmountStackEffect;
    public int MaxStackEffect;

}
