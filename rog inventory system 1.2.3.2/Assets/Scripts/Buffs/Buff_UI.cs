using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Buff_UI : MonoBehaviour
{
    [SerializeField] private Image _buffIcon;
    [SerializeField] private Image _staticIcon;
    [SerializeField] private InventoryItemData _itemData;
    [SerializeField] private StatusEffectsData _statusData;
    [SerializeField] private TextMeshProUGUI buffStacks;

    private float startTime;
    private float duration;
    private BuffList _buffList;
    public int _buffCount;

    private Coroutine _handleBuffCoroutine = null;
    //private Coroutine _countStackCoroutine = null;
    private List<IEnumerator> _enumerators = new List<IEnumerator>();
    private IEnumerator _enumer;

    public Image BuffIcon => _buffIcon;
    public Image StaticIcon => _staticIcon;
    public InventoryItemData ItemData => _itemData;
    public StatusEffectsData StatusData => _statusData;



    private void Awake()
    {
        _buffList = GetComponentInParent<BuffList>();
    }

    private void Start()
    {
        //Application.targetFrameRate = 60;
    }

    public void Init(StatusEffectsData statusData, float buffDuration)
    {
        Debug.Log("Инициализация бафа");
        startTime = Time.time;
        duration = buffDuration;

        _buffIcon.sprite = statusData.Icon;
        _staticIcon.sprite = statusData.Icon;
        _statusData = statusData;
        //_itemData = invSlot_UI.AssignedInventorySlot.ItemData;
    }

    public void CoroutineController(StatusEffectsData statusData, float buffDuration, Buff_UI buffSlot)
    {
        if (_handleBuffCoroutine == null)
        {
            Debug.Log("Corutina UI start");
            _handleBuffCoroutine = StartCoroutine(HandleBuff(statusData, buffDuration, buffSlot));

            //_countStackCoroutine = StartCoroutine(CountStack(invSlot_UI, buffDuration));
            CoroutineAdd(statusData, buffDuration);
        }
        else
        {
            Debug.Log("Corutina UI update");
            StopCoroutine(_handleBuffCoroutine);
            _handleBuffCoroutine = null;
            _handleBuffCoroutine = StartCoroutine(HandleBuff(statusData, buffDuration, buffSlot));

            if (_buffCount < statusData.MaxStackEffect)
            {
                //_countStackCoroutine = StartCoroutine(CountStack(invSlot_UI, buffDuration));
                CoroutineAdd(statusData, buffDuration);
            }
            else
            {
                //return;

                //StopCoroutine(_countStackCoroutine);
                //_countStackCoroutine = null;

                //_countStackCoroutine = StartCoroutine(CountStack(invSlot_UI, buffDuration));

                CoroutineRemove();
                CoroutineAdd(statusData, buffDuration);

            }
        }
    }

    private IEnumerator HandleBuff(StatusEffectsData statusData, float buffDuration, Buff_UI buffSlot)
    {
        float elapsedTime = 0f;

        _buffIcon.fillAmount = 1;

        while (elapsedTime < buffDuration)
        {
            _buffIcon.fillAmount -= 1 / duration * Time.deltaTime;
            elapsedTime += Time.deltaTime;

            buffStacks.text = statusData.AmountStackEffect.ToString();
            yield return null;
        }

        if (_buffList.Buffs1.Count != 0) //////////////////////////////////////////////////
        {
            RemoveBuffFromList(buffSlot);
        }
        else
        {
            Debug.Log("1111");
        }

        Destroy(gameObject);
        _handleBuffCoroutine = null;
        Debug.Log("Corutina UI null");
    }

    public void RemoveBuffFromList(Buff_UI buffSlot)
    {
        Debug.Log("remove buffs");
        //List<(InventoryItemData itemData, Buff_UI buff_UI)> buffsToRemove = new List<(InventoryItemData itemData, Buff_UI buff_UI)>();
        List<(StatusEffectsData statusData, Buff_UI buff_UI)> buffsToRemove = new List<(StatusEffectsData statusData, Buff_UI buff_UI)>();

        foreach (var buff in _buffList.Buffs1)/////////////////////////////////////////
        {
            if (buff.Item1 != buffSlot.StatusData)///////////////////////////////////////
            {
                continue;
            }
            else
            {
                buffsToRemove.Add(buff);
            }
        }

        foreach (var buffToRemove in buffsToRemove)
        {
            _buffList.Buffs1.Remove(buffToRemove);//////////////////////////////
        }
    }

    private IEnumerator CountStack(StatusEffectsData statusData, float buffDuration)
    {

        if (_buffCount < statusData.MaxStackEffect)
        {
            Debug.Log("++++++");
            _buffCount++;
        }

        float elapsedTime = 0f;

        while (elapsedTime < buffDuration)
        {
            buffStacks.text = _buffCount.ToString();
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        _buffCount--;
    }

    public void CoroutineAdd(StatusEffectsData statusData, float buffDuration)
    {
        _enumer = CountStack(statusData, buffDuration);
        _enumerators.Add(_enumer);

        StartCoroutine(_enumer);
    }

    public void CoroutineRemove()
    {
        StopCoroutine(_enumerators[0]);
        _enumerators[0] = null;
        _enumerators.RemoveAt(0);
    }
}
