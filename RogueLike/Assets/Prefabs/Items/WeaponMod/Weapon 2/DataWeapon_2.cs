using Assets.Scripts.Inventory.Item_Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataWeapon_2 : DataWeaponMod
{
    public override void ModifyShoot(Player player)
    {
        Debug.Log("test 2");

        Vector3 shotPointPosition = player.PlayerGun.ShotPoint.position;

        BulletData bullet1 = CreateAndInitializeBullet(player, shotPointPosition);

        Vector3 bullet2Position = shotPointPosition + player.PlayerGun.ShotPoint.up * -0.3f; 
        BulletData bullet2 = CreateAndInitializeBullet(player, bullet2Position);
}

    private BulletData CreateAndInitializeBullet(Player player, Vector3 position)
    {
        BulletData bullet = Instantiate(_bullet, position, player.PlayerGun.ShotPoint.rotation, player.PlayerGun.BulletContainer.transform);

        BulletPlayer bulletPlayer = bullet.GetComponent<BulletPlayer>();
        if (bulletPlayer != null)
        {
            bulletPlayer.InitializeBullet(player.PlayerStats);
            bulletPlayer.InitOwner(player);
        }

        return bullet;
    }
}
