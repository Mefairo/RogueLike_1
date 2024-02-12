using Assets.Scripts.Inventory.Item_Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataWeapon_1 : DataWeaponMod
{
    public override void ModifyShoot(Player player)
    {
        Debug.Log("test 1");

        StandartShoot(player);
    }

    private void StandartShoot(Player player)
    {
        BulletData bullet_1 = Instantiate(_bullet, player.PlayerGun.ShotPoint.position,
    player.PlayerGun.ShotPoint.rotation, player.PlayerGun.BulletContainer.transform);

        WeaponBullet_1 bulletPlayer = bullet_1.GetComponent<WeaponBullet_1>();

        if (bulletPlayer != null)
        {
            bulletPlayer.InitializeBullet(player.PlayerStats);
            bulletPlayer.InitOwner(player);
        }
    }
}
