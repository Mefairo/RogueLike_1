using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    //public float cooldown;

    //[HideInInspector] public bool isCooldown;

    //private Image shieldImage;
    //private Player player;

    //private void Start()
    //{
    //    shieldImage = GetComponent<Image>();
    //    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    //    isCooldown = true;
    //}

    //private void Update()
    //{
    //    if (isCooldown)
    //    {
    //        shieldImage.fillAmount -= 1 / cooldown * Time.deltaTime;
    //        if (shieldImage.fillAmount <= 0)
    //        {
    //            shieldImage.fillAmount = 1;
    //            isCooldown = false;
    //            player.shield.SetActive(false);
    //            gameObject.SetActive(false);
    //        }
    //    }
    //}

    //public void ResetTimer()
    //{
    //    shieldImage.fillAmount = 1;
    //}

    //public void ReduceTime(float damage)
    //{
    //    shieldImage.fillAmount += damage / 5f;
    //}

    //public void ShieldEffect(Player player)
    //{
    //    player.shield.SetActive(true);
    //    player.shieldTimer.gameObject.SetActive(true);
    //    player.shieldTimer.isCooldown = true;
    //}
}
