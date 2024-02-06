using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff_RedHead : UseItems
{
    public override void UseItem(Player player, StatusEffectsData statusData, InventorySlot_UI invSlot_UI)
    {
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
            if(player.TryGetComponent(out IHealthChangeable healthChangeable))
            {
                healthChangeable.ChangeCurrentHealth(statusData.VOTAmount);
            }
            //player.PlayerHealth.ChangeCurrentHealth(statusData.VOTAmount);

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
    }
}
