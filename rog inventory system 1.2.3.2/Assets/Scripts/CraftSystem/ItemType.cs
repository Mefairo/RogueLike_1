using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemType
{
    public enum ItemsType
    {
        None,
        HealthPotion,
        Shield,
        Star,
        Bottle,
        Helmet,
        Chest,
        Belt,
        Boots
    }

    public ItemsType itemIype;

    public enum ItemsTier
    {
        None,
        White,
        Green,
        Blue,
        Violet,
        Red
    }

    public ItemsTier itemTier;
}
