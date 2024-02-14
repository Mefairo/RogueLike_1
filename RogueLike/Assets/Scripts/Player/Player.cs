using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private PlayerGun _playerGun;
    [SerializeField] private PlayerInventoryHolder _playerInv;
    [SerializeField] private GunManager _gunManager;
    [Space]
    [SerializeField] private RoundManager _roundManager;

    public PlayerController PlayerController => _playerController;
    public PlayerHealth PlayerHealth => _playerHealth;  
    public PlayerStats PlayerStats => _playerStats;
    public PlayerGun PlayerGun => _playerGun;
    public PlayerInventoryHolder PlayerInv => _playerInv;
    public GunManager GunManager => _gunManager;

    public RoundManager RoundManager => _roundManager;
}
