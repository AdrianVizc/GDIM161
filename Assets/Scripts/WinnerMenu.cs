using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class WinnerMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text username;
    [SerializeField] private GameObject image;
    [SerializeField] private GameObject usernameText;
    [SerializeField] private GameObject winScreenUI;
    [SerializeField] private GameObject mainMenuButton;

    public int amountOfKillsToWin;

    private CameraFollowMouse cameraFollowMouse;    
    private string winnerUsername = "";
    private bool foundCamera = false;

    [HideInInspector]
    public bool gameOver = false;

    private void Start()
    {
        image.SetActive(false);
        usernameText.SetActive(false);
        winScreenUI.SetActive(false);
        mainMenuButton.SetActive(false);       

        gameOver = false;
    }

    public void SetWinnerUsername(string winner)
    {
        winnerUsername = winner;
    }

    private void Update()
    {        
        if (winnerUsername != "" && gameOver)
        {
            if (!foundCamera)
            {
                cameraFollowMouse = GameObject.Find("Main Camera").GetComponent<CameraFollowMouse>();
                cameraFollowMouse.enabled = false;
                foundCamera = true;
            }

            InGameUI.globalInputLock = true;
            username.text = winnerUsername;
            image.SetActive(true);
            usernameText.SetActive(true);
            winScreenUI.SetActive(true);
            mainMenuButton.SetActive(true);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }        
    }
}
