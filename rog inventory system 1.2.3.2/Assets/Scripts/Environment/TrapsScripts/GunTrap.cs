using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTrap : GunData
{
    protected override void Update()
    {
        Shoot();
    }

    protected override void DirectionForShoot() { }

    protected override void Shoot()
    {
        base.Shoot();
    }
}
