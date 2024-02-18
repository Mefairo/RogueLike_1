using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class InventorySlot_UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Image itemSprite;
    //[SerializeField] protected Image _backgroundSprite;
    [SerializeField] protected TextMeshProUGUI itemCount;
    [SerializeField] protected InventorySlot assignedInventorySlot;
    [SerializeField] protected GameObject _slotHighlight;

    [SerializeField] protected ItemsShowInfo _panelInfo;

    protected Button button;

    public InventorySlot AssignedInventorySlot => assignedInventorySlot;
    public InventoryDisplay ParentDisplay { get; private set; }

    private void Awake()
    {
        ClearSlot();

        itemSprite.preserveAspect = true;
        //_backgroundSprite.preserveAspect = true;

        button = GetComponent<Button>();
        button?.onClick.AddListener(OnUISlotClick);

        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }

    private void Start()
    {
        _panelInfo = UIManager.Instance.PanelInfo;

        if (_panelInfo != null)
            _panelInfo.gameObject.SetActive(false);
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

            //if(slot.ItemData.IconBackground != null)
            //{
            //    _backgroundSprite.sprite = slot.ItemData.IconBackground;
            //    _backgroundSprite.color = Color.white;
            //}

            //else
            //    _backgroundSprite.color = _backgroundSprite.color.WithAlpha(0);

            if (slot.StackSize > 1)
            {
                itemCount.text = slot.StackSize.ToString();
            }
            else
            {
                itemCount.text = "";
            }
        }

        else
        {
            ClearSlot();
        }

    }

    public void UpdateUISlot()
    {
        if (assignedInventorySlot != null)
        {
            UpdateUISlot(assignedInventorySlot);
        }
    }

    public void ClearSlot()
    {
        //Debug.Log("33");
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;

        //_backgroundSprite.sprite = null;
        //_backgroundSprite.color = Color.clear;

        itemCount.text = "";
    }

    public virtual void OnUISlotClick()
    {
        ParentDisplay?.SlotClicked(this);
    }

    internal void ToggleHighlight()
    {
        _slotHighlight.SetActive(!_slotHighlight.activeInHierarchy);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _panelInfo.ShowInfo(this.AssignedInventorySlot.ItemData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _panelInfo.HideInfo();
    }
}
