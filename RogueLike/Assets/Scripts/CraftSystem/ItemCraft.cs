using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemCraft: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private InventoryItemData _itemData;
    [SerializeField] private TextMeshProUGUI _nameComponent;
    [SerializeField] private TextMeshProUGUI _amountComponent;
    [SerializeField] private Image _imageComponent;
    //[SerializeField] private Image _imageBackgroundComponent;
    [SerializeField] private Button _updatePreviewButton;
    [SerializeField] private ItemsShowInfo _panelInfo;

    public InventoryItemData ItemData => _itemData;
    public TextMeshProUGUI NameComponent => _nameComponent;
    public TextMeshProUGUI AmountComponent => _amountComponent;
    public Image ImageComponent => _imageComponent;
    //public Image ImageBackgroundComponent => _imageBackgroundComponent;
    public CraftKeeperDisplay ParentDisplay { get; private set; }

    private void Awake()
    {
        _updatePreviewButton?.onClick.AddListener(UpdateItemPreview);

        ParentDisplay = GetComponentInParent<CraftKeeperDisplay>();
    }

    private void Start()
    {
        _panelInfo = UIManager.Instance.PanelInfo;

        if (_panelInfo != null)
            _panelInfo.gameObject.SetActive(false);
    }

    public void SetItemComponents(InventoryItemData itemData, string newName, string newAmount, Sprite newImage)
    {
        _itemData = itemData;
        _nameComponent.text = newName;
        _amountComponent.text = newAmount;
        _imageComponent.sprite = newImage;
        //_imageBackgroundComponent.sprite = backgroundImage;
    }

    private void UpdateItemPreview()
    {
        ParentDisplay.UpdateItemPreview(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _panelInfo.ShowInfo(this.ItemData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _panelInfo.HideInfo();
    }
}
