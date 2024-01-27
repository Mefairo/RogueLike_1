using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NavMeshPlus.Components;
using TMPro;
using System.Linq;

public class RoundManager : MonoBehaviour
{
    [Header("CountRound")]
    [SerializeField] private int _countRound;
    [SerializeField] private TextMeshProUGUI _countRoundText;

    [Header("Navigation and Transform")]
    [SerializeField] private Transform _rooms;
    [SerializeField] private NavMeshSurface navMeshSurface;

    [Header("Managers and Objects")]
    [SerializeField] private NextLevelTransfer _levelTransfer;
    [SerializeField] private RoundTimer _roundTimer;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private ItemsSpawner _itemsSpawner;

    private Animator _componentAnimator;

    public List<EnemyRoundController> EnemiesOnScene;

    public UnityAction OnNewRoundStart;

    private void Awake()
    {
        _countRound = 1;
    }

    private void Start()
    {
        _componentAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _levelTransfer.OnCountRoundComplete += NewRound;
    }

    private void OnDisable()
    {
        _levelTransfer.OnCountRoundComplete -= NewRound;
    }

    private void NewRound()
    {
        StartCoroutine(NewRoundCoruutine());
    }

    private IEnumerator NewRoundCoruutine()
    {
        _componentAnimator.SetTrigger("sceneClosing");

        foreach (Transform room in _rooms)
        {
            Destroy(room.gameObject);
        }

        yield return null;

        _countRound++;
        _countRoundText.text = $"Round {_countRound}";

        _levelTransfer.gameObject.SetActive(false);
        _enemySpawner.subjectsSpawnPoints.Clear();
        _itemsSpawner.subjectsSpawnPoints.Clear();

        OnNewRoundStart?.Invoke();

        yield return new WaitForSeconds(1f);

        navMeshSurface.BuildNavMesh();

        _componentAnimator.SetTrigger("sceneOpening");
    }

    public void ConditionsForNewRound()
    {
        if (!EnemiesOnScene.Any() && !_roundTimer.TimerActive)
        {
            _levelTransfer.gameObject.SetActive(true);
        }
    }

    public void OnAnimationOver()
    {

    }
}
