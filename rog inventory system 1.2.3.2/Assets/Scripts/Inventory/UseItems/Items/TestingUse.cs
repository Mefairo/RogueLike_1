using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingUse : MonoBehaviour
{
    private RemoveItems removeItem = new RemoveItems();

    [SerializeField] private BuffManagerUI _buffManagerUI;
    [SerializeField] private float _duration;
    [SerializeField] private Coroutine _handleShieldBuff = null;

    public Coroutine HandleShieldBuff => _handleShieldBuff;


    public void UseItemShield(Player player, InventorySlot_UI invSlot_UI)
    {
        if (_handleShieldBuff == null)
        {
            //_buffManagerUI.AddBuff(invSlot_UI, _duration);
            _handleShieldBuff = StartCoroutine(HandleShield(player, invSlot_UI));

        }
        else
        {
            StopCoroutine(_handleShieldBuff);
            _handleShieldBuff = null;

            //_buffManagerUI.UpdateBuff(invSlot_UI, _duration);
            _handleShieldBuff = StartCoroutine(HandleShield(player, invSlot_UI));
        }

        removeItem.RemoveItemsFromSlot(invSlot_UI);
    }

    public IEnumerator HandleShield(Player player, InventorySlot_UI invSlot_UI)
    {
        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _handleShieldBuff = null;
    }
}
