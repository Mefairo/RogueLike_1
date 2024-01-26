using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public abstract class DebuffData : MonoBehaviour
{
    [Header("General Parameters")]
    [SerializeField] private DebuffManagerUI _debuffManagerUI;
    [SerializeField] private float _duration;
    [SerializeField] private EnemyData _enemy;
    public Coroutine coroutineDebuff;

    [Header("Debuff Parameters")]
    [SerializeField] private StatusEffectsData _statusData;
    [SerializeField] private float _damage;
    [SerializeField] private float _timeBtwTick;
    [SerializeField] private int _tickAmount;

    public virtual void AddDebuff(Player player, StatusEffectsData statusData)
    {
        if (statusData.Stackable)
        {
            Debug.Log("cccc");
            //_debuffManagerUI.AddDebuff(statusData, _duration);
            _debuffManagerUI.AddDebuff(statusData, statusData.Duration);

            coroutineDebuff = StartCoroutine(HandleDebuff(player, statusData));
        }
        else
        {
            UnStackDebuff(player, statusData);
        }
    }

    protected void UnStackDebuff(Player player, StatusEffectsData statusData)
    {
        if(coroutineDebuff == null)
        {
            //_debuffManagerUI.AddDebuff(statusData, _duration);
            _debuffManagerUI.AddDebuff(statusData, statusData.Duration);
            coroutineDebuff = StartCoroutine(HandleDebuff(player, statusData));
        }
        else
        {
            StopCoroutine(coroutineDebuff);
            coroutineDebuff = null;

            //_debuffManagerUI.UpdateDebuff(statusData, _duration);
            _debuffManagerUI.UpdateDebuff(statusData, statusData.Duration);
            coroutineDebuff = StartCoroutine(HandleDebuff(player, statusData));
        }
    }

    public virtual IEnumerator HandleDebuff(Player player, StatusEffectsData statusData)
    {
        for (int i = 0; i < statusData.TickAmount; i++)
        {
            yield return new WaitForSecondsRealtime(statusData.TickSpeed);

            //player.TakeDamage(statusData.VOTAmount, 0);

            if (player.TryGetComponent(out IHealthChangeable healthChangeable))
            {
                healthChangeable.ChangeCurrentHealth(statusData.VOTAmount);
            }

            //yield return new WaitForSecondsRealtime(_timeBtwTick);

        }
        coroutineDebuff = null;
    }
}
