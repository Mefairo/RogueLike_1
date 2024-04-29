using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] protected MouseItemData mouseInventoryItem;
    [SerializeField] protected DynamicInventoryDisplay _inventoryDisplay;
    [SerializeField] protected EquipDisplay _equipDisplay;
    [SerializeField] protected Player _player;
    [SerializeField] protected StaticInventoryDisplay _playerInvDisplay;

    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary;

    public InventorySystem InventorySystem => inventorySystem;

    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;


    public abstract void AssignSlot(InventorySystem invToDisplay, int offset);

    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        foreach (var slot in SlotDictionary)
        {
            if (slot.Value == updatedSlot) // Значение слота - "под худом" слота инвентаря
                slot.Key.UpdateUISlot(updatedSlot); // Ключ слота - UI представление значения
        }
    }

    public virtual void SlotClicked(InventorySlot_UI clickedUISlot)
    {

        // Если кликнуть на слот,в котором есть предмет,а у мыши нет элемента, тогда нужно поднять предмет
        if (clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlot.ItemData == null)
        {
            if (Input.GetKey(KeyCode.LeftControl) && _inventoryDisplay.gameObject.activeSelf && clickedUISlot.AssignedInventorySlot.ItemData != null)
            {
                SwapItemsBtwInventories(clickedUISlot);
            }

            else if (Input.GetKey(KeyCode.LeftControl) && _playerInvDisplay != null)
            {
                TakeItemFromChests(clickedUISlot);
            }

            else if (Input.GetKey(KeyCode.LeftControl) && _playerInvDisplay != null && _equipDisplay.gameObject.activeSelf)
            {
                TakeItemFromChests(clickedUISlot);
            }

            else if (Input.GetKey(KeyCode.LeftControl) && _equipDisplay.gameObject.activeSelf && clickedUISlot.AssignedInventorySlot.ItemData != null)
            {
                FastEquipItem(clickedUISlot);
            }          

            // Если игрок держит SHIFT, то нужно разделить количество предметов в слоте
            else if (Input.GetKey(KeyCode.LeftShift) && clickedUISlot.AssignedInventorySlot.SplitStack(out InventorySlot halfStackSlot))
            {
                mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                clickedUISlot.UpdateUISlot();
                return;
            }
            else
            {
                mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
                clickedUISlot.AssignedInventorySlot.ClearSlot();
                clickedUISlot.UpdateUISlot();
                return;
            }

        }

        // Если слот не содержит предмет, а мышь содержит предмет, тогда нужно положить предмет на курсоре мышки в этот пустой слот
        if (clickedUISlot.AssignedInventorySlot.ItemData == null && mouseInventoryItem.AssignedInventorySlot.ItemData != null)
        {
            clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
            clickedUISlot.UpdateUISlot();

            mouseInventoryItem.ClearSlot();
            return;
        }

        // Если оба слота содержат предметы, то нужно решить...
        if (clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlot.ItemData != null)
        {
            bool isSameItem = clickedUISlot.AssignedInventorySlot.ItemData == mouseInventoryItem.AssignedInventorySlot.ItemData;

            // Если предмет в инвентаре и на курсоре мыши одинаковый, то объединяем их
            if (isSameItem && clickedUISlot.AssignedInventorySlot.EnoughRoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.StackSize))
            {
                clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
                clickedUISlot.UpdateUISlot();

                mouseInventoryItem.ClearSlot();
                return;
            }

            else if (isSameItem && !clickedUISlot.AssignedInventorySlot.RoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.StackSize, out int leftInStack))
            {
                if (leftInStack < 1)
                {
                    SwapSlots(clickedUISlot);
                }

                else
                {
                    int remainingOnMouse = mouseInventoryItem.AssignedInventorySlot.StackSize - leftInStack;
                    clickedUISlot.AssignedInventorySlot.AddToStack(leftInStack);
                    clickedUISlot.UpdateUISlot();

                    var newItem = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.ItemData, remainingOnMouse);
                    mouseInventoryItem.ClearSlot();
                    mouseInventoryItem.UpdateMouseSlot(newItem);

                    return;
                }
            }

            // Если предметы разные, то меняем их местами
            else if (!isSameItem)
            {
                SwapSlots(clickedUISlot);
                return;
            }



        }
    }

    private void SwapSlots(InventorySlot_UI clickedUISlot)
    {
        // Получаем предметы в слотах
        InventorySlot itemInClickedSlot = clickedUISlot.AssignedInventorySlot;
        InventorySlot itemOnMouse = mouseInventoryItem.AssignedInventorySlot;

        // Если предметы одинаковые (тот же тип)
        if (itemInClickedSlot.ItemData == itemOnMouse.ItemData)
        {
            int spaceLeftInClickedSlot = itemInClickedSlot.ItemData.MaxStackSize - itemInClickedSlot.StackSize;
            int remainingOnMouse = itemOnMouse.StackSize - spaceLeftInClickedSlot;

            // Если предмет в слоте и рна мышке "ЭКИПИРОВКА"
            if(itemInClickedSlot.ItemData.ItemType == ItemType.Equipment && itemOnMouse.ItemData.ItemType == ItemType.Equipment)
            {
                if (itemInClickedSlot.EquipSlot.ItemTier != itemOnMouse.EquipSlot.ItemTier)
                {
                    InventorySlot clonedClickedSlot = new InventorySlot(itemInClickedSlot.EquipSlot, itemInClickedSlot.StackSize);
                    clickedUISlot.ClearSlot();
                    clickedUISlot.AssignedInventorySlot.AssignEquipItem(itemOnMouse);
                    clickedUISlot.UpdateUISlot();

                    mouseInventoryItem.UpdateMouseEquipItem(clonedClickedSlot);
                }
            }

            // Если можно объединить стаки полностью
            if (spaceLeftInClickedSlot >= itemOnMouse.StackSize)
            {
                itemInClickedSlot.AddToStack(itemOnMouse.StackSize);
                mouseInventoryItem.ClearSlot();
            }


            else if (itemInClickedSlot.StackSize == itemInClickedSlot.ItemData.MaxStackSize)
            {
                int slotSize = itemInClickedSlot.StackSize + 1;
                var clonedClickedSlot = new InventorySlot(itemInClickedSlot.ItemData, itemInClickedSlot.StackSize);

                itemInClickedSlot.SwapStack(itemOnMouse.StackSize);
                clickedUISlot.UpdateUISlot();

                mouseInventoryItem.ClearSlot();
                itemOnMouse.AddToStack(slotSize);
                mouseInventoryItem.UpdateMouseSlot(clonedClickedSlot);
            }
        }

        // Если предметы разные, меняем их местами
        else if (itemInClickedSlot.ItemData != itemOnMouse.ItemData)
        {
            if(itemInClickedSlot.ItemData.ItemType == ItemType.Equipment)
            {
                InventorySlot clonedClickedSlot = new InventorySlot(itemInClickedSlot.EquipSlot, itemInClickedSlot.StackSize);
                clickedUISlot.ClearSlot();
                clickedUISlot.AssignedInventorySlot.AssignEquipItem(itemOnMouse);
                clickedUISlot.UpdateUISlot();

                mouseInventoryItem.UpdateMouseEquipItem(clonedClickedSlot);
            }

            else
            {
                InventorySlot clonedClickedSlot = new InventorySlot(itemInClickedSlot.ItemData, itemInClickedSlot.StackSize);
                clickedUISlot.ClearSlot();
                clickedUISlot.AssignedInventorySlot.AssignItem(itemOnMouse);
                clickedUISlot.UpdateUISlot();

                mouseInventoryItem.UpdateMouseSlot(clonedClickedSlot);
            }
        }
    }

    private void FastEquipItem(InventorySlot_UI clickedUISlot)
    {
        if (clickedUISlot.AssignedInventorySlot.ItemData is CraftItemData)
        {
            CraftItemData equipItem = (CraftItemData)clickedUISlot.AssignedInventorySlot.ItemData;

            foreach (Transform childTransform in _equipDisplay.transform)
            {
                EquipSlot_UI slot = childTransform.GetComponent<EquipSlot_UI>();

                if (slot.AssignedInventorySlot.ItemData == null)
                {
                    if (equipItem.EquipType == slot.ItemType)
                    {
                        slot.AssignedInventorySlot.AssignItem(clickedUISlot.AssignedInventorySlot);
                        slot.UpdateUISlot();

                        clickedUISlot.AssignedInventorySlot.ClearSlot();
                        clickedUISlot.UpdateUISlot();

                        _equipDisplay.OnPlayerEquip?.Invoke(_player, slot);
                    }

                    else
                        continue;
                }

                else
                {
                    if (equipItem.EquipType == slot.ItemType)
                    {
                        _equipDisplay.OnPlayerTakeOfEquip?.Invoke(_player, slot);

                        SwapEquipSlots(clickedUISlot, slot);
                    }

                    else
                        continue;
                }
            }
        }
    }

    private void SwapItemsBtwInventories(InventorySlot_UI clickedUISlot)
    {
        foreach (Transform childTransform in _inventoryDisplay.transform)
        {
            InventorySlot_UI slot = childTransform.GetComponent<InventorySlot_UI>();

            bool isSameItem = clickedUISlot.AssignedInventorySlot.ItemData == slot.AssignedInventorySlot.ItemData;

            if (slot.AssignedInventorySlot.ItemData == null)
            {
                _inventoryDisplay.InventorySystem.AddToInventory(clickedUISlot.AssignedInventorySlot.ItemData, 1);

                if (clickedUISlot.AssignedInventorySlot.StackSize == 1)
                {
                    clickedUISlot.AssignedInventorySlot.ClearSlot();
                    clickedUISlot.UpdateUISlot();
                }

                else
                {
                    clickedUISlot.AssignedInventorySlot.RemoveFromStack(1);
                    clickedUISlot.UpdateUISlot();
                }

                return;
            }

            else
            {
                if (isSameItem && slot.AssignedInventorySlot.EnoughRoomLeftInStack(clickedUISlot.AssignedInventorySlot.StackSize))
                {
                    _inventoryDisplay.InventorySystem.AddToInventory(clickedUISlot.AssignedInventorySlot.ItemData, 1);
                    clickedUISlot.AssignedInventorySlot.RemoveFromStack(1);
                    clickedUISlot.UpdateUISlot();

                    return;
                }

                else
                    continue;

            }
        }
    }

    private void SwapEquipSlots(InventorySlot_UI clickedUISlot, EquipSlot_UI slot)
    {
        var clonedEquipSlot = new InventorySlot(slot.AssignedInventorySlot.ItemData, slot.AssignedInventorySlot.StackSize);

        slot.ClearSlot();
        slot.AssignedInventorySlot.AssignItem(clickedUISlot.AssignedInventorySlot);
        slot.UpdateUISlot();

        clickedUISlot.AssignedInventorySlot.AssignItem(clonedEquipSlot);
        clickedUISlot.UpdateUISlot();

        _equipDisplay.OnPlayerEquip?.Invoke(_player, slot);
    }

    private void TakeItemFromChests(InventorySlot_UI clickedUISlot)
    {
        foreach (Transform childTransform in _playerInvDisplay.transform)
        {
            InventorySlot_UI slot = childTransform.GetComponent<InventorySlot_UI>();

            bool isSameItem = clickedUISlot.AssignedInventorySlot.ItemData == slot.AssignedInventorySlot.ItemData;

            if (slot.AssignedInventorySlot.ItemData == null)
            {
                _playerInvDisplay.InventorySystem.AddToInventory(clickedUISlot.AssignedInventorySlot.ItemData, 1);

                if (clickedUISlot.AssignedInventorySlot.StackSize == 1)
                {
                    clickedUISlot.AssignedInventorySlot.ClearSlot();
                    clickedUISlot.UpdateUISlot();
                }

                else
                {
                    clickedUISlot.AssignedInventorySlot.RemoveFromStack(1);
                    clickedUISlot.UpdateUISlot();
                }

                return;
            }

            else
            {
                if (isSameItem && slot.AssignedInventorySlot.EnoughRoomLeftInStack(clickedUISlot.AssignedInventorySlot.StackSize))
                {
                    _playerInvDisplay.InventorySystem.AddToInventory(clickedUISlot.AssignedInventorySlot.ItemData, 1);
                    clickedUISlot.AssignedInventorySlot.RemoveFromStack(1);
                    clickedUISlot.UpdateUISlot();

                    return;
                }

                else
                    continue;
            }
        }
    }
}

