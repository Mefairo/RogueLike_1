using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquipDisplay : InventoryDisplay
{
    //[Header("Equipment Slots")]
    //[SerializeField] private EquipSlot_UI _helmetSlot;
    //[SerializeField] private EquipSlot_UI _chestSlot;
    //[SerializeField] private EquipSlot_UI _bootsSlot;

    //[Header("Options")]
    //[SerializeField] private Player _player;
    //[SerializeField] private PlayerEquipmentHolder _playerEquip;
    //[SerializeField] private EquipmentManager _equipManager;

    //public EquipSlot_UI HelmetSlot => _helmetSlot;

    public UnityAction<EquipSlot_UI> OnPlayerEquip;
    public UnityAction<EquipSlot_UI> OnPlayerTakeOfEquip;


    public override void AssignSlot(InventorySystem invToDisplay, int offset)
    {
        Debug.Log("Assign Slot override");
    }


    public void SlotClicked(EquipSlot_UI equipSlot_UI)
    {
        Debug.Log("equip click");
        var equipSlot = equipSlot_UI.AssignedInventorySlot.ItemData;
        var mouseSlot = mouseInventoryItem.AssignedInventorySlot.ItemData;

        // Если кликнуть на слот,в котором есть предмет,а у мыши нет элемента, тогда нужно поднять предмет
        if (equipSlot != null && mouseSlot == null)
        {
            OnPlayerTakeOfEquip?.Invoke(equipSlot_UI);

            mouseInventoryItem.UpdateMouseSlot(equipSlot_UI.AssignedInventorySlot);
            equipSlot_UI.AssignedInventorySlot.ClearSlot();
            equipSlot_UI.UpdateUISlot();

            return;
        }

        // Если слот не содержит предмет, а мышь содержит предмет, тогда нужно положить предмет на курсоре мышки в этот пустой слот
        if (mouseSlot != null)
        {
            Debug.Log("mouse no null");

            EquipItemPlayer(equipSlot_UI);
        }



    }
    private void EquipItemPlayer(EquipSlot_UI equipSlot_UI)
    {
        var equipSlot = equipSlot_UI.AssignedInventorySlot.ItemData;
        var mouseSlot = mouseInventoryItem.AssignedInventorySlot.ItemData;

        if (equipSlot_UI.ItemType == mouseSlot.ItemType)
        {
            Debug.Log("equip item");

            if (equipSlot == null && mouseSlot != null)
            {
                Debug.Log("slot null");
                equipSlot_UI.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
                equipSlot_UI.UpdateUISlot();

                OnPlayerEquip?.Invoke(equipSlot_UI);
                //_playerEquip.EquipItem(_player, equipSlot_UI);

                mouseInventoryItem.ClearSlot();
                return;
            }

            // Если оба слота содержат предметы, то нужно решить...
            else if (equipSlot != null && mouseSlot != null)
            {
                Debug.Log("slot ne null");
                SwapSlots(equipSlot_UI);
                return;
            }
        }
    }

    private void SwapSlots(EquipSlot_UI equipSlot_UI)
    {
        var itemInClickedSlot = equipSlot_UI.AssignedInventorySlot;
        var itemOnMouse = mouseInventoryItem.AssignedInventorySlot;

        // Если предметы одинаковые (тот же тип)
        if (itemInClickedSlot.ItemData == itemOnMouse.ItemData)
        {
            //Debug.Log("121");
            int spaceLeftInClickedSlot = itemInClickedSlot.ItemData.MaxStackSize - itemInClickedSlot.StackSize;
            int remainingOnMouse = itemOnMouse.StackSize - spaceLeftInClickedSlot;

            // Если можно объединить стаки полностью
            if (spaceLeftInClickedSlot >= itemOnMouse.StackSize)
            {
                //Debug.Log("122");
                itemInClickedSlot.AddToStack(itemOnMouse.StackSize);
                mouseInventoryItem.ClearSlot();
            }


            else if (itemInClickedSlot.StackSize == itemInClickedSlot.ItemData.MaxStackSize)
            {
                //Debug.Log("123123");

                int slotSize = itemInClickedSlot.StackSize + 1;
                var clonedClickedSlot = new InventorySlot(itemInClickedSlot.ItemData, itemInClickedSlot.StackSize);

                itemInClickedSlot.SwapStack(itemOnMouse.StackSize);
                equipSlot_UI.UpdateUISlot();

                mouseInventoryItem.ClearSlot();
                itemOnMouse.AddToStack(slotSize);
                mouseInventoryItem.UpdateMouseSlot(clonedClickedSlot);
            }
        }

        // Если предметы разные, меняем их местами
        else if (itemInClickedSlot.ItemData != itemOnMouse.ItemData)
        {
            //Debug.Log("124");
            OnPlayerTakeOfEquip?.Invoke(equipSlot_UI);
            
            var clonedClickedSlot = new InventorySlot(itemInClickedSlot.ItemData, itemInClickedSlot.StackSize);
            equipSlot_UI.ClearSlot();
            equipSlot_UI.AssignedInventorySlot.AssignItem(itemOnMouse);
            equipSlot_UI.UpdateUISlot();

            OnPlayerEquip?.Invoke(equipSlot_UI);

            mouseInventoryItem.UpdateMouseSlot(clonedClickedSlot);
        }
    }

}
