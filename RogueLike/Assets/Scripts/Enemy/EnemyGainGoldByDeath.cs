using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGainGoldByDeath : MonoBehaviour
{
    [SerializeField] private int _minGold;
    [SerializeField] private int _maxGold;

    private void OnDestroy()
    {
        int randomGold = Random.Range(_minGold, _maxGold);

        EnemyManager.Instance.Player.PlayerInv.PrimaryInventorySystem.GainGold(randomGold);
    }
}
