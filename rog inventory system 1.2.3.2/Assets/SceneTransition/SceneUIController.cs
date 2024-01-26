using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class SceneUIController : MonoBehaviour
{
    [SerializeField] private MenuManager _menuManager;
    [SerializeField] private List<GameObject> _gameObjectActive;

    private void Awake()
    {
        _menuManager.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("Close window menu");
            DisplayMenuManager();
        }

    }

    private void DisplayMenuManager()
    {
        bool allInactive = true;

        for (int i = 0; i < _gameObjectActive.Count; i++)
        {
            var obj = _gameObjectActive[i];

            if (obj.gameObject.activeSelf)
            {
                allInactive = false;
                obj.gameObject.SetActive(false);
            }
        }

        if (allInactive)
        {
            _menuManager.gameObject.SetActive(!_menuManager.gameObject.activeSelf);
        }
    }
}
