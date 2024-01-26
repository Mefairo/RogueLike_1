using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipSlot : MonoBehaviour
{

    public abstract void Equip(Player player, EquipSlot_UI equipSlot_UI);

    public abstract void UnEquip(Player player, EquipSlot_UI equipSlot_UI);
}
