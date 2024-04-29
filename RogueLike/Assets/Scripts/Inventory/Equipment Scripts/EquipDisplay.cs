using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquipDisplay : InventoryDisplay
{
    [SerializeField] protected StaticInventoryDisplay _invDisplay;

    public UnityAction<Player, EquipSlot_UI> OnPlayerEquip;
    public UnityAction<Player, EquipSlot_UI> OnPlayerTakeOfEquip;

    private EquipItem _equipSlot = new EquipItem();


    private void Awake()
    {
        _equipSlot.Subscribe();
    }

    private void OnDestroy()
    {
        _equipSlot.Unsubscribe();
    }

    public override void AssignSlot(InventorySystem invToDisplay, int offset)
    {
        Debug.Log("Assign Slot override");
    }


    public void SlotClicked(EquipSlot_UI equipSlot_UI)
    {
        var equipSlot = equipSlot_UI.AssignedInventorySlot.ItemData;
        var mouseSlot = mouseInventoryItem.AssignedInventorySlot.ItemData;

        if (equipSlot != null && mouseSlot == null && Input.GetKey(KeyCode.LeftControl))
        {
            Debug.Log("takeoff");
            OnPlayerTakeOfEquip?.Invoke(_player, equipSlot_UI);

            _invDisplay.InventorySystem.AddToInventory(equipSlot_UI.AssignedInventorySlot.ItemData, 1);

            equipSlot_UI.AssignedInventorySlot.ClearSlot();
            equipSlot_UI.UpdateUISlot();
        }


        // Если кликнуть на слот,в котором есть предмет,а у мыши нет элемента, тогда нужно поднять предмет
        else if (equipSlot != null && mouseSlot == null)
        {
            OnPlayerTakeOfEquip?.Invoke(_player, equipSlot_UI);

            mouseInventoryItem.UpdateMouseSlot(equipSlot_UI.AssignedInventorySlot);
            equipSlot_UI.AssignedInventorySlot.ClearSlot();
            equipSlot_UI.UpdateUISlot();

            return;
        }

        // Если слот не содержит предмет, а мышь содержит предмет, тогда нужно положить предмет на курсоре мышки в этот пустой слот
        if (mouseSlot != null)
        {
            EquipItemPlayer(equipSlot_UI);
        }



    }
    private void EquipItemPlayer(EquipSlot_UI equipSlot_UI)
    {
        CraftItemData equipSlot = (CraftItemData)equipSlot_UI.AssignedInventorySlot.ItemData;
        CraftItemData mouseSlot = (CraftItemData)mouseInventoryItem.AssignedInventorySlot.ItemData;

        if (equipSlot_UI.ItemType == mouseSlot.EquipType)
        {
            if (equipSlot == null && mouseSlot != null)
            {
                equipSlot_UI.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
                equipSlot_UI.UpdateUISlot();

                OnPlayerEquip?.Invoke(_player, equipSlot_UI);

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
            OnPlayerTakeOfEquip?.Invoke(_player, equipSlot_UI);

            var clonedClickedSlot = new InventorySlot(itemInClickedSlot.ItemData, itemInClickedSlot.StackSize);
            equipSlot_UI.ClearSlot();
            equipSlot_UI.AssignedInventorySlot.AssignItem(itemOnMouse);
            equipSlot_UI.UpdateUISlot();

            OnPlayerEquip?.Invoke(_player, equipSlot_UI);

            mouseInventoryItem.UpdateMouseSlot(clonedClickedSlot);
        }
    }

}
