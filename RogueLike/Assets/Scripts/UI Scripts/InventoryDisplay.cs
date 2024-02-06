using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] protected MouseItemData mouseInventoryItem;

    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary;

    public InventorySystem InventorySystem => inventorySystem;

    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;


    public abstract void AssignSlot(InventorySystem invToDisplay, int offset);

    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        //Debug.Log("35");
        foreach (var slot in SlotDictionary)
        {
            if (slot.Value == updatedSlot) // �������� ����� - "��� �����" ����� ���������
            {
                //Debug.Log("36");
                slot.Key.UpdateUISlot(updatedSlot); // ���� ����� - UI ������������� ��������
            }
        }
    }

    public virtual void SlotClicked(InventorySlot_UI clickedUISlot)
    {

        // ���� �������� �� ����,� ������� ���� �������,� � ���� ��� ��������, ����� ����� ������� �������
        if (clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlot.ItemData == null)
        {
            //Debug.Log("37");
            // ���� ����� ������ SHIFT, �� ����� ��������� ���������� ��������� � �����
            if (Input.GetKey(KeyCode.LeftShift) && clickedUISlot.AssignedInventorySlot.SplitStack(out InventorySlot halfStackSlot))
            {
                //Debug.Log("111");
                mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                clickedUISlot.UpdateUISlot();
                return;
            }
            else
            {
                //Debug.Log("112");
                mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
                clickedUISlot.AssignedInventorySlot.ClearSlot();
                clickedUISlot.UpdateUISlot();
                return;
            }

        }

        // ���� ���� �� �������� �������, � ���� �������� �������, ����� ����� �������� ������� �� ������� ����� � ���� ������ ����
        if (clickedUISlot.AssignedInventorySlot.ItemData == null && mouseInventoryItem.AssignedInventorySlot.ItemData != null)
        {
            //Debug.Log("113");
            clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
            clickedUISlot.UpdateUISlot();

            mouseInventoryItem.ClearSlot();
            return;
        }



        // ���� ��� ����� �������� ��������, �� ����� ������...
        if (clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlot.ItemData != null)
        {
            //Debug.Log("38");
            bool isSameItem = clickedUISlot.AssignedInventorySlot.ItemData == mouseInventoryItem.AssignedInventorySlot.ItemData;



            // ���� ������� � ��������� � �� ������� ���� ����������, �� ���������� ��
            if (isSameItem && clickedUISlot.AssignedInventorySlot.EnoughRoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.StackSize))
            {
                //Debug.Log("114");
                clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
                clickedUISlot.UpdateUISlot();

                mouseInventoryItem.ClearSlot();
                return;
            }

            else if (isSameItem && !clickedUISlot.AssignedInventorySlot.RoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.StackSize, out int leftInStack))
            {
                //Debug.Log("39");
                if (leftInStack < 1)
                {
                    //Debug.Log("40");
                    SwapSlots(clickedUISlot);
                }
                else
                {
                    //Debug.Log("116");
                    int remainingOnMouse = mouseInventoryItem.AssignedInventorySlot.StackSize - leftInStack;
                    clickedUISlot.AssignedInventorySlot.AddToStack(leftInStack);
                    clickedUISlot.UpdateUISlot();

                    var newItem = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.ItemData, remainingOnMouse);
                    mouseInventoryItem.ClearSlot();
                    mouseInventoryItem.UpdateMouseSlot(newItem);

                    return;
                }
            }

            // ���� �������� ������, �� ������ �� �������
            else if (!isSameItem)
            {
                //Debug.Log("117");
                SwapSlots(clickedUISlot);
                return;
            }



        }
    }

    private void SwapSlots(InventorySlot_UI clickedUISlot)
    {
        //Debug.Log("120");
        // �������� �������� � ������
        var itemInClickedSlot = clickedUISlot.AssignedInventorySlot;
        var itemOnMouse = mouseInventoryItem.AssignedInventorySlot;

        // ���� �������� ���������� (��� �� ���)
        if (itemInClickedSlot.ItemData == itemOnMouse.ItemData)
        {
            //Debug.Log("121");
            int spaceLeftInClickedSlot = itemInClickedSlot.ItemData.MaxStackSize - itemInClickedSlot.StackSize;
            int remainingOnMouse = itemOnMouse.StackSize - spaceLeftInClickedSlot;

            // ���� ����� ���������� ����� ���������
            if (spaceLeftInClickedSlot >= itemOnMouse.StackSize)
            {
                //Debug.Log("122");
                itemInClickedSlot.AddToStack(itemOnMouse.StackSize);
                mouseInventoryItem.ClearSlot();
            }


            else if(itemInClickedSlot.StackSize == itemInClickedSlot.ItemData.MaxStackSize)
            {
                //Debug.Log("123123");

                int slotSize = itemInClickedSlot.StackSize + 1;
                var clonedClickedSlot = new InventorySlot(itemInClickedSlot.ItemData, itemInClickedSlot.StackSize);

                itemInClickedSlot.SwapStack(itemOnMouse.StackSize);
                clickedUISlot.UpdateUISlot();

                mouseInventoryItem.ClearSlot();
                itemOnMouse.AddToStack(slotSize);
                mouseInventoryItem.UpdateMouseSlot(clonedClickedSlot);
            }
        }

        // ���� �������� ������, ������ �� �������
        else if (itemInClickedSlot.ItemData != itemOnMouse.ItemData)
        {
            //Debug.Log("124");
            var clonedClickedSlot = new InventorySlot(itemInClickedSlot.ItemData, itemInClickedSlot.StackSize);
            clickedUISlot.ClearSlot();
            clickedUISlot.AssignedInventorySlot.AssignItem(itemOnMouse);
            clickedUISlot.UpdateUISlot();

            mouseInventoryItem.UpdateMouseSlot(clonedClickedSlot);
        }
    }
}
