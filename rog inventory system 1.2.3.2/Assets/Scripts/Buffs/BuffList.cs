using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffList : MonoBehaviour
{
    [SerializeField] private List<(InventoryItemData itemData, Buff_UI buff_UI)> _buffs = new List<(InventoryItemData itemData, Buff_UI buff_UI)>();
    [SerializeField] private List<(StatusEffectsData statusData, Buff_UI buff_UI)> _buffs1 = new List<(StatusEffectsData statusData, Buff_UI buff_UI)>();

    public List<(InventoryItemData itemData, Buff_UI buff_UI)> Buffs => _buffs;
    public List<(StatusEffectsData statusData, Buff_UI buff_UI)> Buffs1 => _buffs1;





    //private void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.C))
    //    {
    //        Debug.Log(_buffs.Count);
    //    }
    //}
}
