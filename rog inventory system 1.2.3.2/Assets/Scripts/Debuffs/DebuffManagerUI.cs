using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffManagerUI : MonoBehaviour
{
    [SerializeField] private Debuff_UI _prefabDebuff;
    [SerializeField] private GameObject _debuffContentList;

    private DebuffList _debuffList;

    private void Start()
    {
        _debuffList = GetComponentInChildren<DebuffList>();
    }
    public void AddDebuff(StatusEffectsData statusData, float debuffDuration)
    {
        UnityEngine.Debug.Log("444");
        var debuffSlot = Instantiate(_prefabDebuff, _debuffContentList.transform);
        debuffSlot.Init(statusData, debuffDuration);
        debuffSlot.CoroutineController(statusData, debuffDuration, debuffSlot);

        _debuffList.Debuffs.Add(debuffSlot);
    }

    public void UpdateDebuff(StatusEffectsData statusData, float buffDuration)
    {
        List<Debuff_UI> debuffsToRemove = new List<Debuff_UI>();

        foreach (var debuff in _debuffList.Debuffs)
        {
            if (debuff.StatusData != statusData)
            {
                UnityEngine.Debug.Log("446");
                continue;
            }
            else
            {
                UnityEngine.Debug.Log("447");
                debuffsToRemove.Add(debuff);
            }
        }

        foreach (var debuffToRemove in debuffsToRemove)
        {
            UnityEngine.Debug.Log("448");
            Destroy(debuffToRemove.gameObject);
            _debuffList.Debuffs.Remove(debuffToRemove);
            AddDebuff(statusData, buffDuration);
        }
    }
}
