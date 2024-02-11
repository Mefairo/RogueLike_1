using Assets.Scripts.Inventory.Item_Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataWeapon_1 : DataWeaponMod
{
    [SerializeField] private BulletData _bullet;

    public override void ModifyShoot(Player player)
    {
        Debug.Log("test 1");

        Vector3 shotPointPosition = player.PlayerGun.ShotPoint.position;

        // Создаем первую пулю с позицией shotPoint
        BulletData bullet1 = Instantiate(_bullet, shotPointPosition,
            player.PlayerGun.ShotPoint.rotation, player.PlayerGun.BulletContainer.transform);

        // Создаем вторую пулю с небольшим смещением относительно shotPoint
        Vector3 bullet2Position = shotPointPosition + player.PlayerGun.ShotPoint.right * 0.1f; // Смещение вправо от shotPoint
        BulletData bullet2 = Instantiate(_bullet, bullet2Position,
            player.PlayerGun.ShotPoint.rotation, player.PlayerGun.BulletContainer.transform);

        // Инициализируем пули
        BulletPlayer bulletPlayer1 = bullet1.GetComponent<BulletPlayer>();
        BulletPlayer bulletPlayer2 = bullet2.GetComponent<BulletPlayer>();

        if (bulletPlayer1 != null && bulletPlayer2 != null)
        {
            bulletPlayer1.InitializeBullet(player.PlayerStats);
            bulletPlayer2.InitializeBullet(player.PlayerStats);
            bulletPlayer1.InitOwner(player);
            bulletPlayer2.InitOwner(player);
        }

    }

    private void StandartShoot(Player player)
    {
        BulletData bullet_1 = Instantiate(_bullet, player.PlayerGun.ShotPoint.position,
    player.PlayerGun.ShotPoint.rotation, player.PlayerGun.BulletContainer.transform);

        BulletPlayer bulletPlayer = bullet_1.GetComponent<BulletPlayer>();

        if (bulletPlayer != null)
        {
            bulletPlayer.InitializeBullet(player.PlayerStats);
            bulletPlayer.InitOwner(player);
        }
    }
}
