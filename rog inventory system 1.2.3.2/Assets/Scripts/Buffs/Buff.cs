using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Buff: MonoBehaviour
{
    [SerializeField] private GameObject _buffOnObject;
    [SerializeField] protected int _countBuffs;

    public Coroutine coroutineBuff = null;

    public List<IEnumerator> enumerators = new List<IEnumerator>();
    public IEnumerator enumer;

    public void CoroutineManager(Player player, InventorySlot_UI invSlot_UI)
    {
        enumer = HandleBuff(player, invSlot_UI);

        for (int i = 0; i < enumerators.Count; i++)
        {
            enumerators.Add(enumer);
        }
    }

    //private void Update()
    //{
    //    if(Input.GetKeyUp(KeyCode.C))
    //    {
    //        enumerators.RemoveAt(0);
    //    }
    //}


    public virtual IEnumerator HandleBuff(Player player, InventorySlot_UI invSlot_UI)
    {
        var statusEffect = invSlot_UI.AssignedInventorySlot.ItemData.StatusEffects;

        if (_countBuffs < statusEffect.MaxStackEffect)
        {
            Debug.Log("+++");
            _countBuffs++;
        }

        Debug.Log("rrrr3333");
        float elapsedTime = 0f;

        while (elapsedTime < statusEffect.Duration)
        {

            elapsedTime += Time.deltaTime;
            yield return null;

            if (_buffOnObject != null)
            {
                _buffOnObject.SetActive(true);
            }
        }

        if (_buffOnObject != null)
        {
            _buffOnObject.SetActive(false);
        }

        _countBuffs--;

        coroutineBuff = null;
        Debug.Log("Corutina null");

    }
}
