using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot_UI : InventorySlot_UI
{
    [SerializeField] private EquipType _itemType;

    public EquipType ItemType  => _itemType;

    public EquipDisplay ParentEquipmentDisplay { get; private set; }

    private void Awake()
    {
        ClearSlot();

        itemSprite.preserveAspect = true;

        button = GetComponent<Button>();
        button?.onClick.AddListener(OnUISlotClick);

        ParentEquipmentDisplay = transform.parent.GetComponent<EquipDisplay>();  
    }

    public override void OnUISlotClick()
    {
        ParentEquipmentDisplay?.SlotClicked(this);
    }
}
