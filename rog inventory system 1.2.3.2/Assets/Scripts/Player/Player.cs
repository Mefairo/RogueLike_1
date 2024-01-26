using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerHealth _playerHealth;

    public PlayerController PlayerController => _playerController;
    public PlayerHealth PlayerHealth => _playerHealth;  

    public void LifeStealDamage(float damage)
    {
        //if (playerStats.currentHealth >= playerStats.maxHealth)
        //    playerStats.currentHealth = playerStats.maxHealth;

        //else
        //{
        //    playerStats.currentHealth += damage * playerStats.lifeSteal;
        //    healthDisplay.text = "HP: " + playerStats.currentHealth.ToString("F1");
        //    healthBar.SetHealth(playerStats.currentHealth);
        //}
    }
}
