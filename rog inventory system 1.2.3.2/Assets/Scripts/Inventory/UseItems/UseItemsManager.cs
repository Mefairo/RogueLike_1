using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class UseItemsManager : MonoBehaviour
{
    [SerializeField] private UseHealthPotion _useHealthPotion;
    [SerializeField] private UseShield _useShield;
    [SerializeField] private UseStar _useStar;
    [SerializeField] private UseBottle _useBottle;
    [SerializeField] private UseFruit _useFruit;
    [SerializeField] private UseTest _useTest;

    public void UseItem(Player player, InventorySlot_UI invSlot_UI)
    {
        var itemID = invSlot_UI.AssignedInventorySlot.ItemData.ID;
        var statusData = invSlot_UI.AssignedInventorySlot.ItemData.StatusEffects;

        switch (itemID)
        {
            case 0:
                //_useTest.UseItem(player, invSlot_UI);
                _useFruit.UseItem(player, statusData, invSlot_UI);
                break;
            case 1:
                _useHealthPotion.UseHealth(player, invSlot_UI);
                break;

            case 2:
                _useShield.UseItem(player, statusData, invSlot_UI);
                break;

            case 3:
                _useStar.UseItemStar(player, invSlot_UI);
                break;

            case 10:
                _useBottle.UseItem(player, statusData, invSlot_UI);
                break;
        }

    }
}
