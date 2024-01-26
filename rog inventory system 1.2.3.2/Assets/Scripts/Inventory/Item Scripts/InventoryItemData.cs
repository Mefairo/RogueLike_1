using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Inventory System/Inventory Item")]
public class InventoryItemData : ScriptableObject
{
    [Header("Display Parametres")]
    public int ID = -1;
    public string DisplayName;
    [TextArea(4, 4)]
    public string Description;
    public Sprite Icon;
    public GameObject ItemPrefab;

    [Header("Game Parametres")]
    public int MaxStackSize;
    public float GoldValue;

    [Header("Buff Parametres")]
    public StatusEffectsData StatusEffects;

    [Header("Equip Parametres")]
    public ItemType.ItemsType ItemType;
    public ItemType.ItemsTier ItemTier;
    public int ItemTierCount;

}
