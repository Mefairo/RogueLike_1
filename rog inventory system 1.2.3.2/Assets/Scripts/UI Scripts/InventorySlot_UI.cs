using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InventorySlot_UI : MonoBehaviour
{
    [SerializeField] protected Image itemSprite;
    [SerializeField] protected TextMeshProUGUI itemCount;
    [SerializeField] protected InventorySlot assignedInventorySlot;
    [SerializeField] protected GameObject _slotHighlight;

    protected Button button;

    public InventorySlot AssignedInventorySlot => assignedInventorySlot;
    public InventoryDisplay ParentDisplay { get; private set; }

    private void Awake()
    {
        ClearSlot();

        itemSprite.preserveAspect = true;

        button = GetComponent<Button>();
        button?.onClick.AddListener(OnUISlotClick);

        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }

    public void Init(InventorySlot slot)
    {
        //Debug.Log("26");
        assignedInventorySlot = slot;
        UpdateUISlot(slot);
    }

    public void UpdateUISlot(InventorySlot slot)
    {
        //Debug.Log("27");
        if (slot.ItemData != null)
        {
            //Debug.Log("28");
            itemSprite.sprite = slot.ItemData.Icon;
            itemSprite.color = Color.white;

            if (slot.StackSize > 1)
            {
                //Debug.Log("29");
                itemCount.text = slot.StackSize.ToString();
            }
            else
            {
                //Debug.Log("30");
                itemCount.text = "";
            }
        }

        else
        {
            //Debug.Log("31");
            ClearSlot();
        }

    }

    public void UpdateUISlot()
    {
        if (assignedInventorySlot != null)
        {
            //Debug.Log("32");
            UpdateUISlot(assignedInventorySlot);
        }
    }

    public void ClearSlot()
    {
        //Debug.Log("33");
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;
        itemCount.text = "";
    }

    public virtual void OnUISlotClick()
    {
        Debug.Log("34");
        ParentDisplay?.SlotClicked(this);
    }

    internal void ToggleHighlight()
    {
        _slotHighlight.SetActive(!_slotHighlight.activeInHierarchy);
    }
}
