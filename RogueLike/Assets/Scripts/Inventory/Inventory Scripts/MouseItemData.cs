using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;

public class MouseItemData : MonoBehaviour
{
    public Image ItemSprite;
    public Image BackgroundSprite;
    public TextMeshProUGUI ItemCount;
    public InventorySlot AssignedInventorySlot;

    [SerializeField] private int _dropOffset;
    [SerializeField] private Transform _pointDrop;


    private void Awake()
    {
        ItemSprite.color = Color.clear;
        ItemSprite.preserveAspect = true;

        BackgroundSprite.color = Color.clear;
        BackgroundSprite.preserveAspect = true;

        ItemCount.text = "";
    }

    public void UpdateMouseSlot(InventorySlot invSlot)
    {
        //Debug.Log("23");
        AssignedInventorySlot.AssignItem(invSlot);

        UpdateMouseSlot();
    }

    public void UpdateMouseEquipItem(InventorySlot invSlot)
    {
        //Debug.Log("23");
        AssignedInventorySlot.AssignEquipItem(invSlot);

        UpdateMouseSlot();
    }

    public void UpdateMouseSlot()
    {
        ItemSprite.sprite = AssignedInventorySlot.ItemData.Icon;
        ItemSprite.color = Color.white;

        if (AssignedInventorySlot.ItemData.IconBackground != null)
            ChangeBackgroundColor();

        else
            BackgroundSprite.color = BackgroundSprite.color.WithAlpha(0);

        ItemCount.text = AssignedInventorySlot.StackSize.ToString();

    }

    private void ChangeBackgroundColor()
    {
        if (AssignedInventorySlot.ItemData.IconBackground != null)
        {
            BackgroundSprite.sprite = AssignedInventorySlot.ItemData.IconBackground;

            if (AssignedInventorySlot.EquipSlot.ItemTier == 2)
            {
                BackgroundSprite.color = Color.blue;
            }

            else if (AssignedInventorySlot.EquipSlot.ItemTier == 1)
            {
                BackgroundSprite.color = Color.green;
            }

            else if (AssignedInventorySlot.EquipSlot.ItemTier == 0)
            {
                BackgroundSprite.color = Color.white;
            }
        }

        else
            BackgroundSprite.color = BackgroundSprite.color.WithAlpha(0);
    }


    private void Update()
    {
        DropItem();
    }

    private void DropItem()
    {
        if (AssignedInventorySlot.ItemData != null)
        {
            transform.position = Mouse.current.position.ReadValue();

            if (Mouse.current.leftButton.wasPressedThisFrame && !IsPointerOverUIObject())
            {
                //if (AssignedInventorySlot.ItemData.ItemPrefab != null)
                //{
                //    Instantiate(AssignedInventorySlot.ItemData.ItemPrefab, _pointDrop.position, Quaternion.identity);
                //}

                if (AssignedInventorySlot.ItemData.ItemPrefab != null)
                {
                    if (AssignedInventorySlot.ItemData.ItemType == ItemType.Equipment)
                    {
                        var dropItem = AssignedInventorySlot;
                        dropItem.ItemData.ItemPrefab1.EquipSlot.ItemTier = dropItem.EquipSlot.ItemTier;
                        Instantiate(dropItem.ItemData.ItemPrefab1, _pointDrop.position, Quaternion.identity);
                    }

                    else
                        Instantiate(AssignedInventorySlot.ItemData.ItemPrefab, _pointDrop.position, Quaternion.identity);

                }

                if (AssignedInventorySlot.StackSize > 1)
                {
                    AssignedInventorySlot.AddToStack(-1);
                    UpdateMouseSlot();
                }
                else
                {
                    ClearSlot();
                }
            }
        }
    }

    public void ClearSlot()
    {
        AssignedInventorySlot.ClearSlot();

        ItemCount.text = "";

        ItemSprite.sprite = null;
        ItemSprite.color = Color.clear;

        BackgroundSprite.sprite = null;
        BackgroundSprite.color = Color.clear;
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Mouse.current.position.ReadValue();
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
