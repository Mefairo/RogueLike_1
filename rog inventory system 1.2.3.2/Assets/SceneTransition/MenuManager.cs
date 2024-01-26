using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _gameObjectActive;
    

    public void GoToGame()
    {
        Debug.Log("Main Game");
        SceneTransition.SwitchToScene("MainScene");
    }

    public void GoToMainMenu()
    {
        if(SceneManager.GetActiveScene().name == "MainScene")
        {
            this.gameObject.SetActive(false);
            SceneTransition.SwitchToScene("MainMenu");
        }
    }

    public void ClosePanelMenu()
    {
        this.gameObject.SetActive(false);
    }

}
