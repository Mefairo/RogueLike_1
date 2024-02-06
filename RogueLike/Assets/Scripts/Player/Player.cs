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
    [Space]
    [SerializeField] private RoundManager _roundManager;

    public PlayerController PlayerController => _playerController;
    public PlayerHealth PlayerHealth => _playerHealth;  
    public PlayerStats PlayerStats => _playerStats;
    public RoundManager RoundManager => _roundManager;
}
