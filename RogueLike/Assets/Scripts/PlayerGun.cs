using Assets.Scripts.Inventory.Item_Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : GunData
{
    [SerializeField] private GunManager _gunManager;

    private Player _player;
    private DataWeaponMod _weaponMod;

    public Player Player => _player;

    protected void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    protected override void Update()
    {
        DirectionForShoot();
        Shoot();
    }

    public void InitModifierWeapon(DataWeaponMod mod)
    {
        _weaponMod = mod;
    }

    protected override void DirectionForShoot()
    {
        _difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _rotZ = Mathf.Atan2(_difference.y, _difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, _rotZ + _offset);
    }

    public override void Shoot()
    {
        if (_timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                if (_weaponMod == null)
                    StandartShoot();

                else
                    _weaponMod.ModifyShoot(_player);

                _timeBtwShots = _player.PlayerStats.StartTimeBtwShots;
            }
        }

        else
            _timeBtwShots -= Time.deltaTime;
    }

    public void StandartShoot()
    {
        BulletData bullet_1 = Instantiate(_bullet, _shotPoint.position, _shotPoint.rotation, _bulletContainer.transform);
        BulletPlayer bulletPlayer = bullet_1.GetComponent<BulletPlayer>();

        if (bulletPlayer != null)
        {
            bulletPlayer.InitializeBullet(_player.PlayerStats);
            bulletPlayer.InitOwner(_player);
        }
    }
}
