using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject usernameCanvas;

    public void ClickPlay()
    {
        usernameCanvas.SetActive(true);
    }

    public void BackButton()
    {
        usernameCanvas.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
