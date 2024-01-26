using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(200)]
public class CraftUIController : MonoBehaviour
{
    [SerializeField] private CraftKeeperDisplay _craftKeeperDisplay;

    private void Awake()
    {
        _craftKeeperDisplay.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        CraftKeeper.OnCraftWindowRequested += DisplayCraftWindow;
    }

    private void OnDisable()
    {
        CraftKeeper.OnCraftWindowRequested -= DisplayCraftWindow;
    }

    private void DisplayCraftWindow(CraftSystem craftSystem, PlayerInventoryHolder playerInventory)
    {
        Debug.Log("xzxcxczcx");
        _craftKeeperDisplay.gameObject.SetActive(true);
        _craftKeeperDisplay.DisplayCraftWindow(craftSystem, playerInventory);
    }
}
