using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuffManagerUI : MonoBehaviour
{
    [SerializeField] private Buff_UI _prefabBuff;
    [SerializeField] private GameObject _buffContentList;

    private BuffList _buffList;

    public BuffList BuffList => _buffList;

    private void Start()
    {
        _buffList = GetComponentInChildren<BuffList>();
    }


    public void AddBuff(StatusEffectsData statusData, float buffDuration)
    {
        UnityEngine.Debug.Log("Добавить бафф в манагере");
        var buffSlot = Instantiate(_prefabBuff, _buffContentList.transform);

        var addListNewBuff = (statusData, buffSlot);
        _buffList.Buffs1.Add(addListNewBuff);//////////////////////////////////

        buffSlot.Init(statusData, buffDuration);
        buffSlot.CoroutineController(statusData, buffDuration, buffSlot);
    }


    public void UpdateBuff(StatusEffectsData statusData, float buffDuration)
    {
        UnityEngine.Debug.Log("Апдейт бафф в манагере");
        //List<(InventoryItemData itemData, Buff_UI buff_UI)> buffsToRemove = new List<(InventoryItemData itemData, Buff_UI buff_UI)>();
        List<(StatusEffectsData statusData, Buff_UI buff_UI)> buffsToRemove = new List<(StatusEffectsData statusData, Buff_UI buff_UI)>();

        foreach (var buff in BuffList.Buffs1)///////////////////////////////////////
        {
            if (buff.Item1 != statusData)
            {
                UnityEngine.Debug.Log("446");
                continue;
            }
            else
            {
                UnityEngine.Debug.Log("447");
                buffsToRemove.Add(buff);
            }
        }

        foreach (var buffToRemove in buffsToRemove)
        {
            UnityEngine.Debug.Log("448");
            Destroy(buffToRemove.Item2.gameObject);
            _buffList.Buffs1.Remove(buffToRemove);/////////////////////////////////////
            AddBuff(statusData, buffDuration);
        }
    }

    public void UpdateBuffUI(StatusEffectsData statusData, float buffDuration)
    {
        UnityEngine.Debug.Log("Апдейт бафф UI в манагере");
        //List<(InventoryItemData itemData, Buff_UI buff_UI)> buffsToUpdate1 = new List<(InventoryItemData itemData, Buff_UI buff_UI)>();
        List<(StatusEffectsData statusData, Buff_UI buff_UI)> buffsToUpdate = new List<(StatusEffectsData statusData, Buff_UI buff_UI)>();


        foreach (var buff in BuffList.Buffs1)///////////////////////////////////////
        {
            if (buff.Item1 != statusData)
            {
                UnityEngine.Debug.Log("448");
                continue;
            }
            else
            {
                UnityEngine.Debug.Log("449");
                buffsToUpdate.Add(buff);
            }
        }

        foreach (var buffToUpdate in buffsToUpdate)
        {
            buffToUpdate.Item2.CoroutineController(statusData, buffDuration, buffToUpdate.Item2);
        }
    }
}

