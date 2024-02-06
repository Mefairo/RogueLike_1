using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class HotbarDisplay : StaticInventoryDisplay
{
    private int _maxIndexSize = 9;
    private int _currentIndex = 0;

    private Player player;
    private UseItemsManager useItems;

    private KeyCode[] hotkeyKeys;

    protected override void Start()
    {
        base.Start();

        _currentIndex = 0;
        _maxIndexSize = slots.Length - 1;

        slots[_currentIndex].ToggleHighlight();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        useItems = FindObjectOfType<UseItemsManager>().GetComponent<UseItemsManager>();

        hotkeyKeys = new KeyCode[]
        {
            KeyCode.Alpha1,
            KeyCode.Alpha2,
            KeyCode.Alpha3,
            KeyCode.Alpha4,
            KeyCode.Alpha5,
            KeyCode.Alpha6,
            KeyCode.Alpha7,
            KeyCode.Alpha8,
            KeyCode.Alpha9,
            KeyCode.Alpha0
        };
    }


    private void SetIndex(int newIndex)
    {
        slots[_currentIndex].ToggleHighlight();
        if (newIndex < 0)
            _currentIndex = 0;

        if (newIndex > _maxIndexSize)
            _currentIndex = _maxIndexSize;

        _currentIndex = newIndex;
        slots[_currentIndex].ToggleHighlight();

    }

    private void UseItem()
    {
        var invSlot_UI = slots[_currentIndex];

        if (invSlot_UI.AssignedInventorySlot.ItemData != null)
        {
            //invSlot_UI.AssignedInventorySlot.ItemData.useItems.UseItem(player, invSlot_UI);
            useItems.UseItem(player, invSlot_UI);
        }
    }

    private void Update()
    {
        for (int i = 0; i < hotkeyKeys.Length; i++)
        {
            if (Input.GetKeyDown(hotkeyKeys[i]))
            {
                SetIndex(i);
                UseItem();
                break;
            }
        }
    }
}
