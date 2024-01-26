using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class CraftKeeper : MonoBehaviour, IInteractable
{
    [SerializeField] private CraftItemList _craftItemsHeld;
    [SerializeField] private CraftSystem _craftSystem;

    public static UnityAction<CraftSystem, PlayerInventoryHolder> OnCraftWindowRequested;

    private void Awake()
    {
        _craftSystem = new CraftSystem(_craftItemsHeld.Items.Count);

        foreach (var item in _craftItemsHeld.Items)
        {
            //Debug.Log($"{item.DisplayName}");
            _craftSystem.AddToCraft(item);
        }
    }


    public UnityAction<IInteractable> OnInteractionComplete { get; set; }


    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        var playerInv = interactor.GetComponent<PlayerInventoryHolder>(); 

        if(playerInv != null)
        {
            OnCraftWindowRequested?.Invoke(_craftSystem, playerInv);
            interactSuccessful = true;
        }

        else
        {
            interactSuccessful = false;
            Debug.LogError("Player inventory not found");
        }
    }


    public void EndInteraction()
    {
        throw new System.NotImplementedException();
    }

}
