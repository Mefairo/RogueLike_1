using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseHealthPotion : MonoBehaviour
{
    private RemoveItems removeItem = new RemoveItems();
    [SerializeField] private float healthAmount;

    public void UseHealth(Player player, InventorySlot_UI invSlot_UI)
    {
        if (player.PlayerHealth.CurrentHealth == player.PlayerHealth.MaxHealth)
        {
            return;
        }

        else
        {
            //player.ChangeHealth(healthAmount, 0);
            //player.PlayerHealth.HealUnitDamage(healthAmount);
            if (player.TryGetComponent(out IHealthChangeable healthChangeable))
                healthChangeable.HealUnitDamage(healthAmount);

            removeItem.RemoveItemsFromSlot(invSlot_UI);
        }
    }


}
