using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UseTest : UseItems
{
    [Header("Item Parameters")]
    [SerializeField] private float _heal;

    [SerializeField] private float _heal1;
    [SerializeField] private float _timeBtwHeals;
    [SerializeField] private int _tickAmount; 
    [SerializeField] private float _maxHPChange;

    public override void UseItem(Player player, StatusEffectsData statusData, InventorySlot_UI invSlot_UI)
    {
        //if (player.currentHealth == player.maxHealth)
        //    return;

        //else
            base.UseItem(player, statusData, invSlot_UI);
    }

    public override IEnumerator HandleBuff(Player player, StatusEffectsData statusData)
    {
        for (int i = 0; i < _tickAmount; i++)
        {
            //player.ChangeHealth(_heal1, 0);
            //player.PlayerHealth.ChangeCurrentHealth(_heal1);
            if (player.TryGetComponent(out IHealthChangeable healthChangeable))
            {
                healthChangeable.ChangeCurrentHealth(_heal1);
            }

            yield return new WaitForSecondsRealtime(_timeBtwHeals);

        }
        coroutineBuff = null;


        //for (int i = 0; i < _tickAmount; i++)
        //{
        //    player.ChangeHealth(_heal1, 0);
        //    player.healthDisplay.text = "HP: " + player.currentHealth.ToString("F1");

        //    yield return new WaitForSecondsRealtime(_timeBtwHeals);

        //}
        //coroutineBuff = null;
       
    }
}
