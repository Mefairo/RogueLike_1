using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UseFruit : UseItems
{
    public override void UseItem(Player player, StatusEffectsData statusData, InventorySlot_UI invSlot_UI)
    {
        if (player.PlayerHealth.CurrentHealth == player.PlayerHealth.MaxHealth)
            return;

        else
            base.UseItem(player, statusData, invSlot_UI);
    }

    public override IEnumerator HandleBuff(Player player, StatusEffectsData statusData)
    {

        if (_countBuffs < statusData.MaxStackEffect)
        {
            Debug.Log("+++");
            _countBuffs++;
        }

        for (int i = 0; i < statusData.TickAmount; i++)
        {

            //player.ChangeHealth(statusData.VOTAmount, 0);
            //player.PlayerHealth.HealUnitDamage(statusData.VOTAmount);
            if (player.TryGetComponent(out IHealthChangeable healthChangeable))
                healthChangeable.HealUnitDamage(statusData.VOTAmount);

            if (i == statusData.TickAmount - 1)
            {
                Debug.Log("q1");
                continue;
            }
            else
            {
                Debug.Log("q2");
                yield return new WaitForSecondsRealtime(statusData.TickSpeed);
            }

        }

        _countBuffs--;

        Debug.Log("Corutina null");
        _enumerators.RemoveAt(0);


        //coroutineBuff = null;////////////

        //for (int i = 0; i < _tickAmount; i++)
        //{

        //    yield return new WaitForSecondsRealtime(_timeBtwHeals);
        //    player.ChangeHealth(-_heal, 0);

        //    player.healthDisplay.text = "HP: " + player.currentHealth.ToString("F1");

        //}
        //coroutineBuff = null;
    }
}
