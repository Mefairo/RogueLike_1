using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffManager : MonoBehaviour
{
    [SerializeField] private Player _player;

    [Header("Debuffs")]
    [SerializeField] private Debuff_RedHead _debuffRedHead;

    private InventorySlot_UI _slot = null;


    public void ApplyDebuff(StatusEffectsData statusData)
    {
        switch (statusData.ID)
        {
            case "001":
                //_debuffRedHead.AddDebuff(_player, statusData);
                _debuffRedHead.UseItem(_player, statusData, _slot);
                break;
        }
    }
}
