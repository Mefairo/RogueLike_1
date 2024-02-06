using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class UseItems : MonoBehaviour
{
    private RemoveItems _removeItem = new RemoveItems();

    [Header("General Parametres")]
    [SerializeField] protected BuffManagerUI _buffManagerUI;
    [SerializeField] protected BuffList _buffList;
    [SerializeField] protected Buff_UI _buff_UI;
    [SerializeField] protected GameObject _buffOnObject;

    [Header("Action Parametres")]
    [SerializeField] protected InventoryItemData _itemData;
    [SerializeField] protected StatusEffectsData _statusEffect;
    [SerializeField] protected int _countBuffs;

    public Coroutine coroutineBuff = null;

    protected List<IEnumerator> _enumerators = new List<IEnumerator>();
    protected IEnumerator _enumer;

    public virtual void UseItem(Player player, StatusEffectsData statusData, InventorySlot_UI invSlot_UI)
    {
        if (statusData.Stackable == true)
        {
            Debug.Log("Стакается");
            if (_buffList.Buffs1.Any(tuple => tuple.statusData == statusData))
            {
                Debug.Log("Есть в списке");
                if (_countBuffs < statusData.MaxStackEffect)
                {
                    Debug.Log("corutina update");

                    _buffManagerUI.UpdateBuffUI(statusData, statusData.Duration);
                    CoroutineAdd(player, statusData);

                    //_buffManagerUI.UpdateBuffUI(invSlot_UI, statusEffect.Duration);
                    //coroutineBuff = StartCoroutine(HandleBuff(player, invSlot_UI));
                }
                else
                {
                    Debug.Log("corutina return");
                    //return;

                    CoroutineRemove();
                    _buffManagerUI.UpdateBuffUI(statusData, statusData.Duration);
                    CoroutineAdd(player, statusData);

                    //StopCoroutine(coroutineBuff);
                    //coroutineBuff = null;

                    //_buffManagerUI.UpdateBuffUI(invSlot_UI, statusEffect.Duration);
                    //coroutineBuff = StartCoroutine(HandleBuff(player, invSlot_UI));
                }
            }
            else
            {
                Debug.Log("corutina start");

                _buffManagerUI.AddBuff(statusData, statusData.Duration);
                CoroutineAdd(player, statusData);

                //_buffManagerUI.AddBuff(invSlot_UI, statusEffect.Duration);
                //coroutineBuff = StartCoroutine(HandleBuff(player, invSlot_UI));
            }


            if (invSlot_UI != null)
                _removeItem.RemoveItemsFromSlot(invSlot_UI);
        }
        else
        {
            Debug.Log("Не стакается");
            UnStackBuff(player, statusData, invSlot_UI);
        }

    }

    public virtual void UnStackBuff(Player player, StatusEffectsData statusData, InventorySlot_UI invSlot_UI)
    {
        Debug.Log("333");
        if (coroutineBuff == null)
        {
            Debug.Log("334");
            _buffManagerUI.AddBuff(statusData, statusData.Duration);
            coroutineBuff = StartCoroutine(HandleBuff(player, statusData));

        }
        else
        {
            Debug.Log("335");
            StopCoroutine(coroutineBuff);
            coroutineBuff = null;

            _buffManagerUI.UpdateBuff(statusData, statusData.Duration);
            coroutineBuff = StartCoroutine(HandleBuff(player, statusData));
        }

        if (invSlot_UI != null)
            _removeItem.RemoveItemsFromSlot(invSlot_UI);
    }

    public virtual IEnumerator HandleBuff(Player player, StatusEffectsData statusData)
    {
        Debug.Log("Handle All");

        if (_countBuffs < statusData.MaxStackEffect)
        {
            Debug.Log("+++");
            _countBuffs++;
        }

        Debug.Log("rrrr3333");
        float elapsedTime = 0f;

        while (elapsedTime < statusData.Duration)
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
        _enumerators.RemoveAt(0);

        coroutineBuff = null;
        Debug.Log("Corutina null");

    }
    protected void CoroutineAdd(Player player, StatusEffectsData statusData)
    {
        Debug.Log("Corutina Add1");
        _enumer = HandleBuff(player, statusData);
        _enumerators.Add(_enumer);

        StartCoroutine(_enumer);
    }

    protected void CoroutineRemove()
    {
        Debug.Log("Corutina Remove1");
        StopCoroutine(_enumerators[0]);
        _enumerators[0] = null;
        _enumerators.RemoveAt(0);
    }

}
