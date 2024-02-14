using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckTypeForTabs : MonoBehaviour
{
    [SerializeField] private ItemType _itemType;

    private Button _button;
   
    public CraftKeeperDisplay ParentDisplay { get; private set; }

    public ItemType ItemType => _itemType;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button?.onClick.AddListener(OnTabClick);

        ParentDisplay = transform.parent.GetComponent<CraftKeeperDisplay>();
    }


    public void OnTabClick()
    {
        Debug.Log("click");
        ParentDisplay?.TabClicked(this);
    }
}
