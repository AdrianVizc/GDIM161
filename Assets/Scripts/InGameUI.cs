using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuGameObject;
    [SerializeField] private GameObject settingsMenuButton;
    [SerializeField] private GameObject mainMenuButton;
    [SerializeField] private GameObject settingsMenuPanel;

    private bool isPauseMenuOn;

    private void Start()
    {
        pauseMenuGameObject.SetActive(false);        
        settingsMenuPanel.SetActive(false);

        isPauseMenuOn = false;
    }

    private void Update()
    {
        detectEscButton();
    }

    private void detectEscButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPauseMenuOn)
            {
                pauseMenuGameObject.SetActive(true);
                isPauseMenuOn = true;

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                pauseMenuGameObject.SetActive(false);
                isPauseMenuOn = false;

                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void OpenSettingsButton()
    {
        settingsMenuButton.SetActive(false);
        mainMenuButton.SetActive(false);

        settingsMenuPanel.SetActive(true);
    }

    public void CloseSettingsPanelButton()
    {
        settingsMenuButton.SetActive(true);
        mainMenuButton.SetActive(true);

        settingsMenuPanel.SetActive(false);
    }

    public void MainMenuButton()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("MainMenu");
    }
}
