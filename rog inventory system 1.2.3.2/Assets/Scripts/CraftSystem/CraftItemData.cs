using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Craft System/Craft Item")]
public class CraftItemData : InventoryItemData
{
    public List<CraftRecipe> Recipes;
}
