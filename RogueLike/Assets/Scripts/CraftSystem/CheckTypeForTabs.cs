using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckTypeForTabs : MonoBehaviour
{
    [SerializeField] private ItemType _itemType;

    private Button _button;

    public CraftKeeperDisplay CraftKeeper { get; private set; }
    public ShopKeeperDisplay ShopKeeper { get; private set; }

    public ItemType ItemType => _itemType;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button?.onClick.AddListener(OnTabClick);

        ShopKeeper = GetComponentInParent<ShopKeeperDisplay>();
        CraftKeeper = GetComponentInParent<CraftKeeperDisplay>();
    }


    public void OnTabClick()
    {
        if (ShopKeeper != null)
            ShopKeeper?.TabClicked(this);

        if (CraftKeeper != null)
            CraftKeeper.TabClicked(this);
    }
}
