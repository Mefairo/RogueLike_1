using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(UniqueID))]
public class ItemPickUp : MonoBehaviour
{
    public InventoryItemData ItemData;

    private ItemPrefabData _itemPrefab;
    private bool canPickUp = true;

    [SerializeField] private ItemPickUpSaveData itemSaveData;
    private string id;

    private void Awake()
    {
        SaveLoad.OnLoadGame += LoadGame;
        itemSaveData = new ItemPickUpSaveData(ItemData, transform.position, transform.rotation);

        _itemPrefab = GetComponentInParent<ItemPrefabData>();
    }

    private void Start()
    {
        id = GetComponent<UniqueID>().ID;
        SaveGameManager.data.activeItems.Add(id, itemSaveData);
    }

    private void LoadGame(SaveData data)
    {
        if (data.collectedItems.Contains(id))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        if (SaveGameManager.data.activeItems.ContainsKey(id))
        {
            SaveGameManager.data.activeItems.Remove(id);
        }

        SaveLoad.OnLoadGame -= LoadGame;
    }

    private void Update()
    {
        canPickUp = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canPickUp && other.CompareTag("Player"))
        {
            FixCollider(gameObject);
            var inventory = other.transform.GetComponent<PlayerInventoryHolder>();

            if (!inventory) return;

            if (_itemPrefab == null)
            {
                if (inventory.AddToInventory(ItemData, 1))
                {
                    SaveGameManager.data.collectedItems.Add(id);

                    Destroy(this.gameObject);
                    //Destroy(this._itemPrefab.gameObject);
                }
            }

            else
            {
                if (_itemPrefab.EquipSlot.ItemData.ItemType == ItemType.Equipment)
                {
                    if (inventory.AddToInventory(_itemPrefab, 1))
                    {
                        SaveGameManager.data.collectedItems.Add(id);

                        Destroy(this.gameObject);
                        Destroy(this._itemPrefab.gameObject);
                    }
                }
            }
        }
    }

    private void FixCollider(GameObject item)
    // Исправление бага с подбором предметов с двойным тригером коллайдера
    {
        CapsuleCollider2D itemCollider = item.GetComponent<CapsuleCollider2D>();
        //itemCollider.isTrigger = false;
        //Destroy(item);
        canPickUp = false;
        //itemCollider.isTrigger = true;
    }
}

[System.Serializable]
public struct ItemPickUpSaveData
{
    public InventoryItemData ItemData;
    public Vector3 Position;
    public Quaternion Rotation;

    public ItemPickUpSaveData(InventoryItemData _data, Vector3 _position, Quaternion _rotation)
    {
        ItemData = _data;
        Position = _position;
        Rotation = _rotation;
    }
}
