using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
//using static UnityEngine.Rendering.DynamicArray<T>;

public class CraftKeeper : MonoBehaviour, IInteractable
{
    [Header("Craft Settings")]
    [SerializeField] private CraftItemList _craftItemsHeld;
    [SerializeField] private CraftSystem _craftSystem;
    [Space]
    [Header("Other Settings")]
    [SerializeField] private RoundManager _roundManager;
    [SerializeField] private int _amountRandomItems;
    [Space]
    [Header("List Items")]
    [SerializeField] private List<CraftItemData> _randomItems;

    public static UnityAction<CraftSystem, PlayerInventoryHolder> OnCraftWindowRequested;
    public static UnityAction OnCraftWindowClosed;

    private void Awake()
    {
        _craftSystem = new CraftSystem(_craftItemsHeld.Items.Count);

        foreach (var item in _craftItemsHeld.Items)
        {
            _craftSystem.AddToCraft(item);
        }

        SetRandomItems();
    }

    private void OnEnable()
    {
        _roundManager.OnNewRoundStart += SetRandomItems;
    }

    private void OnDisable()
    {
        _roundManager.OnNewRoundStart -= SetRandomItems;
    }

    private void SetRandomItems()
    {
        int itemsToAdd = Mathf.Min(_amountRandomItems, _randomItems.Count);

        for (int i = 0; i < itemsToAdd; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, _randomItems.Count);

            _craftSystem.AddToCraft(_randomItems[randomIndex]);
            _randomItems.RemoveAt(randomIndex);
        }
    }


    public UnityAction<IInteractable> OnInteractionComplete { get; set; }


    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        var playerInv = interactor.GetComponent<PlayerInventoryHolder>();

        if (playerInv != null)
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
        OnCraftWindowClosed?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            EndInteraction();
    }

}
