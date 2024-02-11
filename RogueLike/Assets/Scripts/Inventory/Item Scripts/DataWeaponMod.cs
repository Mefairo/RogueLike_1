using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Inventory.Item_Scripts
{
    public abstract class DataWeaponMod : MonoBehaviour
    {
        public abstract void ModifyShoot(Player player);
    }

}
