using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<EnemyData> _enemyData = new List<EnemyData>();

    public List<float> _damageEnemy = new List<float>();    

    //public EnemyRedHead _enemyData;
    //public Transform _target;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("spawn ene");
            Instantiate(_enemyData[0].gameObject, transform.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            //_enemyData.enemyInstance.maxHealth += 5;
            //_damageEnemy[0] += 5;
        }
    }
}
