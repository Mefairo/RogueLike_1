using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public TextMeshProUGUI loadingPercentage;
    public Image loadingProgressBar;

    private static SceneTransition _instance;
    private static bool _shouldPlayOpeningAnimation = false;

    private Animator _componentAnimator;
    private AsyncOperation _loadingSceneOperation;

    public static void SwitchToScene(string sceneName)
    {
        _instance._componentAnimator.SetTrigger("sceneClosing");

        _instance._loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        _instance._loadingSceneOperation.allowSceneActivation = false;
    }

    private void Start()
    {
        _instance = this;

        _componentAnimator = GetComponent<Animator>();

        if (_shouldPlayOpeningAnimation)
            _componentAnimator.SetTrigger("sceneOpening");
    }

    private void Update()
    {
        if (_loadingSceneOperation != null)
        {
            loadingPercentage.text = Mathf.RoundToInt(_loadingSceneOperation.progress * 100) + "%";
            loadingProgressBar.fillAmount = _loadingSceneOperation.progress;
        }
    }

    public void OnAnimationOver()
    {
        _shouldPlayOpeningAnimation = true;
        _loadingSceneOperation.allowSceneActivation = true;
    }
}
