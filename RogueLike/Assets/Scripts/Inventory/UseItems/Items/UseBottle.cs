using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UseBottle : UseItems
{

    public override void UseItem(Player player, StatusEffectsData statusData, InventorySlot_UI invSlot_UI)
    {
        if (player.PlayerHealth.CurrentHealth == player.PlayerHealth.MaxHealth)
        {
            Debug.Log("bbbb");
            return;
        }

        else
            base.UseItem(player, statusData, invSlot_UI);
    }


    //public override IEnumerator HandleBuff(Player player, InventorySlot_UI invSlot_UI)
    //{
    //    var statusEffect = invSlot_UI.AssignedInventorySlot.ItemData.StatusEffects;

    //    _countBuffs++;

    //    float elapsedTime = 0f;

    //    while (elapsedTime < statusEffect.Duration && _buffList.Buffs.Any(item => item.itemData == invSlot_UI.AssignedInventorySlot.ItemData))
    //    {
    //        float healthToAdd = statusEffect.DOTAmount * Time.deltaTime / statusEffect.Duration;
    //        player.ChangeHealth(healthToAdd, 0);

    //        elapsedTime += Time.deltaTime;

    //        player.healthDisplay.text = "HP: " + player.currentHealth.ToString("F1");

    //        yield return null;
    //    }

    //    _countBuffs--;

    //    Debug.Log("Corutina null");

    //    coroutineBuff = null;
    //}

    public override IEnumerator HandleBuff(Player player, StatusEffectsData statusData)
    {
        Debug.Log("Handle bottle");

        if (_countBuffs < statusData.MaxStackEffect)
        {
            Debug.Log("+++");
            _countBuffs++;
        }

        float elapsedTime = 0.01f;

        while (elapsedTime < statusData.Duration && _buffList.Buffs1.Any(item => item.statusData == statusData))
        {
            //Debug.Log("Corutina effect");
            float healthToAdd = statusData.VOTAmount * Time.deltaTime / statusData.Duration;
            //player.ChangeHealth(healthToAdd, 0);
            //player.PlayerHealth.HealUnitDamage(healthToAdd);
            if (player.TryGetComponent(out IHealthChangeable healthChangeable))
                 healthChangeable.HealUnitDamage(healthToAdd);

            elapsedTime += Time.deltaTime;

            //player.healthDisplay.text = "HP: " + player.playerStats.currentHealth.ToString("F1");

            yield return null;
        }

        _countBuffs--;

        Debug.Log("Corutina null");
        _enumerators.RemoveAt(0);

        //_enumerators.Remove(this._enumer);
        //StopCoroutine(this._enumer);
        //this._enumer = null;
        //_enumer = null;

        //coroutineBuff = null;
    }
}