using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarPlayer : MonoBehaviour
{
    public Transform player;

    private void LateUpdate()
    {
        if (player.localScale.x < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        }
        else if (player.localScale.x > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

}
