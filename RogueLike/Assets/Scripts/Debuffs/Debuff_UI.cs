using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debuff_UI : MonoBehaviour
{
    [SerializeField] private Image _debuffIcon;
    [SerializeField] private Image _staticIcon;
    [SerializeField] private StatusEffectsData _statusData;

    private float startTime;
    private float duration;
    private Coroutine _handleDebuffCoroutine = null;
    private DebuffList _debuffList;

    public Image DebuffIcon => _debuffIcon;
    public Image StaticIcon => _staticIcon;
    public StatusEffectsData StatusData => _statusData;

    private void Start()
    {
        _debuffList = GetComponentInParent<DebuffList>();
    }

    public void Init(StatusEffectsData statusData, float debuffDuration)
    {
        startTime = Time.time;
        duration = debuffDuration;

        _debuffIcon.sprite = statusData.Icon;
        _staticIcon.sprite = statusData.Icon;
        _statusData = statusData;
    }

    public void CoroutineController(StatusEffectsData statusData, float debuffDuration, Debuff_UI debuffSlot)
    {
        if (_handleDebuffCoroutine == null)
        {
            _handleDebuffCoroutine = StartCoroutine(HandleDebuff(debuffDuration, debuffSlot));
        }
    }
    private IEnumerator HandleDebuff(float debuffDuration, Debuff_UI debuffSlot)
    {
        float elapsedTime = 0f;

        while (elapsedTime < debuffDuration)
        {
            _debuffIcon.fillAmount -= 1 / duration * Time.deltaTime;
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        if (_debuffList != null)
        {
            _debuffList.Debuffs.Remove(debuffSlot);
        }
        else
        {
            Debug.Log("1111");
        }
        Destroy(gameObject);
    }
}
