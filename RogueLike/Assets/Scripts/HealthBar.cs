using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private UnitHealth _unitHealth;
    [SerializeField] private Slider slider;
    [Space]
    [SerializeField] private TextMeshProUGUI _maxHPText;
    [SerializeField] private TextMeshProUGUI _currentHPText;


    private void OnEnable()
    {
        _unitHealth.OnMaxHPChange += ChangeMaxHealthText;
        _unitHealth.OnMaxHPChange += SetMaxHealth;

        _unitHealth.OnCurrentHPChange += ChangeCurrentHealthText;
        _unitHealth.OnCurrentHPChange += SetHealth;
    }

    private void OnDisable()
    {
        _unitHealth.OnMaxHPChange -= ChangeMaxHealthText;
        _unitHealth.OnMaxHPChange -= SetMaxHealth;

        _unitHealth.OnCurrentHPChange -= ChangeCurrentHealthText;
        _unitHealth.OnCurrentHPChange -= SetHealth;
    }

    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;

        ChangeMaxHealthText(maxHealth);
    }

    public void SetHealth(float health)
    {
        slider.value = health;

        ChangeCurrentHealthText(health);
    }

    private void ChangeMaxHealthText(float maxHealth)
    {
        if (_maxHPText != null)
            _maxHPText.text = $"{maxHealth:F1}";
    }

    private void ChangeCurrentHealthText(float health)
    {
        if (_currentHPText != null)
        {
            Debug.Log("hp");
            _currentHPText.text = $"{health:F1}";
        }
    }

   
}
