using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class InGameUI : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject pauseMenuGameObject;
    [SerializeField] private GameObject settingsMenuButton;
    [SerializeField] private GameObject mainMenuButton;
    [SerializeField] private GameObject settingsMenuPanel;

    private CameraFollowMouse cameraFollowMouse;

    private bool isPauseMenuOn;

    public static bool globalInputLock;

    private void Start()
    {
        pauseMenuGameObject.SetActive(false);        
        settingsMenuPanel.SetActive(false);
        globalInputLock = false;
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
            cameraFollowMouse = GameObject.Find("Main Camera").GetComponent<CameraFollowMouse>();
            if (!isPauseMenuOn)
            {
                pauseMenuGameObject.SetActive(true);
                isPauseMenuOn = true;

                globalInputLock = true;
                cameraFollowMouse.enabled = false;

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                pauseMenuGameObject.SetActive(false);
                isPauseMenuOn = false;

                globalInputLock = false;
                cameraFollowMouse.enabled = true;

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
        
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("MainMenu");
    }
}
