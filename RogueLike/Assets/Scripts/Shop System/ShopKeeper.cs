using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UniqueID))]
public class ShopKeeper : MonoBehaviour, IInteractable
{
    [Header("Shop Settings")]
    [SerializeField] private ShopItemList _shopItemsHeld;
    [SerializeField] private ShopSystem _shopSystem;
    [Space]
    [Header("Other Settings")]
    [SerializeField] private RoundManager _roundManager;
    [SerializeField] private int _amountRandomItems;
    [Space]
    [Header("List Items")]
    [SerializeField] private List<ShopInventoryItem> _randomItems;

    private string _id;

    public static UnityAction<ShopSystem, PlayerInventoryHolder> OnShopWindowRequested;
    public static UnityAction OnShopWindowClosed;

    private ShopSaveData _shopSaveData;

    private void Awake()
    {
        _shopSystem = new ShopSystem(_shopItemsHeld.Items.Count, _shopItemsHeld.MaxAllowedGold, _shopItemsHeld.BuyMarkUp, _shopItemsHeld.SellMarkUp);

        foreach (var item in _shopItemsHeld.Items)
        {
            _shopSystem.AddToShop(item.ItemData, item.Amount);
        }

        SetRandomItems();

        _id = GetComponent<UniqueID>().ID;
        _shopSaveData = new ShopSaveData(_shopSystem);
    }

    private void Start()
    {
        if (!SaveGameManager.data._shopKeeperDictionary.ContainsKey(_id))
            SaveGameManager.data._shopKeeperDictionary.Add(_id, _shopSaveData);
    }

    private void OnEnable()
    {
        SaveLoad.OnLoadGame += LoadInventory;
        _roundManager.OnNewRoundStart += SetRandomItems;
    }

    private void OnDisable()
    {
        SaveLoad.OnLoadGame -= LoadInventory;
        _roundManager.OnNewRoundStart -= SetRandomItems;
    }

    private void SetRandomItems()
    {
        int itemsToAdd = Mathf.Min(_amountRandomItems, _randomItems.Count);

        for (int i = 0; i < itemsToAdd; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, _randomItems.Count);

            _shopSystem.AddToShop(_randomItems[randomIndex].ItemData, _randomItems[randomIndex].Amount);
            _randomItems.RemoveAt(randomIndex);
        }
    }

    private void LoadInventory(SaveData data)
    {
        if (data._shopKeeperDictionary.TryGetValue(_id, out ShopSaveData shopSaveData))
        {
            _shopSaveData = shopSaveData;
            _shopSystem = _shopSaveData.ShopSystem;
        }
        else
        {
            _shopSaveData = new ShopSaveData(_shopSystem);
            SaveGameManager.data._shopKeeperDictionary.Add(_id, _shopSaveData);
        }
    }

    public UnityAction<IInteractable> OnInteractionComplete { get; set; }

    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        var playerInv = interactor.GetComponent<PlayerInventoryHolder>();

        if (playerInv != null)
        {
            OnShopWindowRequested?.Invoke(_shopSystem, playerInv);
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
        OnShopWindowClosed?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            EndInteraction();
    }
}

[System.Serializable]
public class ShopSaveData
{
    public ShopSystem ShopSystem;

    public ShopSaveData(ShopSystem shopSystem)
    {
        ShopSystem = shopSystem;
    }
}
