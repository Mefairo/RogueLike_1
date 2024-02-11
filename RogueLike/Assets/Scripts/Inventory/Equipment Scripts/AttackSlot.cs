using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AttackSlot : IWeaponModifier
{
    public abstract void ModifyShoot(Player player);
}
