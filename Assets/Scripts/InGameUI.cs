using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class InGameUI : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject settingsMenuButton;
    [SerializeField] private GameObject mainMenuButton;
    [SerializeField] private GameObject resumeButton;

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject settingsMenuUI;
    [SerializeField] private GameObject loadingScreenUI;
    [SerializeField] private GameObject winScreenUI;

    private CameraFollowMouse cameraFollowMouse;
    private WinnerMenu winnerMenu;
    private CanvasGroup pauseMenuCanvasGroup;
    private CanvasGroup settingsMenuPanelCanvasGroup;

    private bool isPauseMenuOn;

    public static bool globalInputLock;

    private void Start()
    {
        pauseMenuCanvasGroup = GameObject.Find("PauseMenu").GetComponent<CanvasGroup>();
        settingsMenuPanelCanvasGroup = GameObject.Find("SettingsMenuPanel").GetComponent<CanvasGroup>();
        pauseMenuCanvasGroup.alpha = 0;
        pauseMenuCanvasGroup.interactable = false;
        pauseMenuCanvasGroup.blocksRaycasts = false;
        settingsMenuPanelCanvasGroup.alpha = 0;
        settingsMenuPanelCanvasGroup.interactable = false;
        settingsMenuPanelCanvasGroup.blocksRaycasts = false; 
        globalInputLock = false;
        isPauseMenuOn = false;
        settingsMenuUI.SetActive(false);
        loadingScreenUI.SetActive(false);
    }

    private void Update()
    {
        detectEscButton();
    }

    private void detectEscButton()
    {
        winnerMenu = GameObject.Find("WinnerMenu").GetComponent<WinnerMenu>();
        if (Input.GetKeyDown(KeyCode.Escape) && !winnerMenu.gameOver)
        {
            cameraFollowMouse = GameObject.Find("Main Camera").GetComponent<CameraFollowMouse>();
            if (!isPauseMenuOn)
            {
                pauseMenuCanvasGroup.alpha = 1;
                pauseMenuCanvasGroup.interactable = true;
                pauseMenuCanvasGroup.blocksRaycasts = true;
                isPauseMenuOn = true;

                globalInputLock = true;
                cameraFollowMouse.enabled = false;

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                pauseMenuCanvasGroup.alpha = 0;
                pauseMenuCanvasGroup.interactable = false;
                pauseMenuCanvasGroup.blocksRaycasts = false;
                isPauseMenuOn = false;

                globalInputLock = false;
                cameraFollowMouse.enabled = true;

                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void ResumeButton()
    {
        pauseMenuCanvasGroup.alpha = 0;
        pauseMenuCanvasGroup.interactable = false;
        pauseMenuCanvasGroup.blocksRaycasts = false;
        isPauseMenuOn = false;

        globalInputLock = false;
        cameraFollowMouse.enabled = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OpenSettingsButton()
    {
        settingsMenuButton.SetActive(false);
        mainMenuButton.SetActive(false);
        resumeButton.SetActive(false);
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);

        settingsMenuPanelCanvasGroup.alpha = 1;
        settingsMenuPanelCanvasGroup.interactable = true;
        settingsMenuPanelCanvasGroup.blocksRaycasts = true;
    }

    public void CloseSettingsPanelButton()
    {
        settingsMenuButton.SetActive(true);
        mainMenuButton.SetActive(true);
        resumeButton.SetActive(true);
        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);

        settingsMenuPanelCanvasGroup.alpha = 0;
        settingsMenuPanelCanvasGroup.interactable = false;
        settingsMenuPanelCanvasGroup.blocksRaycasts = false;
    }

    public void MainMenuButton()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        winScreenUI.SetActive(false);
        loadingScreenUI.SetActive(true);
        PhotonNetwork.Disconnect();        
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("MainMenu");
    }
}
