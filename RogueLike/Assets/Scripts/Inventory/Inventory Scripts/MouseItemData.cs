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
    //public Image BackgroundSprite;
    public TextMeshProUGUI ItemCount;
    public InventorySlot AssignedInventorySlot;

    [SerializeField] private int _dropOffset;
    [SerializeField] private Transform _pointDrop;


    private void Awake()
    {
        ItemSprite.color = Color.clear;
        ItemSprite.preserveAspect = true;

        //BackgroundSprite.color = Color.clear;
        //BackgroundSprite.preserveAspect = true;

        ItemCount.text = "";
    }

    public void UpdateMouseSlot(InventorySlot invSlot)
    {
        //Debug.Log("23");
        AssignedInventorySlot.AssignItem(invSlot);
        UpdateMouseSlot();
    } 
    
    public void UpdateMouseSlot()
    {
        //Debug.Log("23");
        ItemSprite.sprite = AssignedInventorySlot.ItemData.Icon;
        ItemSprite.color = Color.white;

        //if(AssignedInventorySlot.ItemData.IconBackground != null)
        //{
        //    BackgroundSprite.sprite = AssignedInventorySlot.ItemData.IconBackground;
        //    BackgroundSprite.color = Color.white;
        //}
       
        //else
        //    BackgroundSprite.color = BackgroundSprite.color.WithAlpha(0);

        ItemCount.text = AssignedInventorySlot.StackSize.ToString();

    }


    private void Update()
    {
        if (AssignedInventorySlot.ItemData != null)
        {
            transform.position = Mouse.current.position.ReadValue();

            if (Mouse.current.leftButton.wasPressedThisFrame && !IsPointerOverUIObject())
            {
                if (AssignedInventorySlot.ItemData.ItemPrefab != null)
                    Instantiate(AssignedInventorySlot.ItemData.ItemPrefab, _pointDrop.position, Quaternion.identity);

                if(AssignedInventorySlot.StackSize > 1)
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

        //BackgroundSprite.sprite = null;
        //BackgroundSprite.color = Color.clear;
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
