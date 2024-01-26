using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthChangeable 
{
    public void ChangeCurrentHealth(float value);
    public void TakeTrapDamage(float value);
    public void TakeUnitDamage(float value);
    public void HealUnitDamage(float value);
    public void ChangeMaxHealth(float value);
}
