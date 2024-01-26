using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTrap : GunData
{
    protected override void Start()
    {
        
    }

    protected override void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (_timeBtwShots <= 0)
            Shoot();

        else
            _timeBtwShots -= Time.deltaTime;
    }
}
