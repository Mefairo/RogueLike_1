using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private ItemsShowInfo _panelInfo;
    [SerializeField] private EquipDisplay _equipDisplay;

    public ItemsShowInfo PanelInfo => _panelInfo;
    public EquipDisplay EquipDisplay => _equipDisplay;
 
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }

        else
            Destroy(gameObject);
    }
}
