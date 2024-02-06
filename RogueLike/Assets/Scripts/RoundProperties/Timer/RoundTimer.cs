using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class RoundTimer : MonoBehaviour
{
    [SerializeField] private float _totalTime;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private RoundManager _roundManager;

    private float _currentTime;
    private int _minutes;
    private int _seconds;

    private bool _timerActive = false;

    public bool TimerActive => _timerActive;

    public float CurrentTime => _currentTime;

    public UnityAction OnEnemySpawnerActive;

    public void StartTimer()
    {
        _timerActive = true;
        _currentTime = _totalTime;

        StartCoroutine(RoundTimerCoroutine());
        OnEnemySpawnerActive?.Invoke();

        UpdateUIText();
    }

    private void UpdateUIText()
    {
        _minutes = Mathf.FloorToInt(_currentTime / 60);
        _seconds = Mathf.FloorToInt(_currentTime % 60);

        _timerText.text = string.Format("{0:00}:{1:00}", _minutes, _seconds);
    }

    private void EndRound()
    {
        _timerActive = false;
        _roundManager.ConditionsForNewRound();
    }

    private IEnumerator RoundTimerCoroutine()
    {

        while (_currentTime > 0.1 && _timerActive)
        {
            _currentTime -= Time.deltaTime;

            UpdateUIText();

            yield return null;
        }

        _currentTime = 0;
        UpdateUIText();
        EndRound();

        yield return null;
    }
}
