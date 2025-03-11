using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerItem : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text playerName;

    private Image backgroundImage;
    [SerializeField] private Color highlightColor;
    [SerializeField] private GameObject leftArrowButton;
    [SerializeField] private GameObject rightArrowButton;
    [SerializeField] private GameObject readyButton;

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    [SerializeField] private Image playerAvatar;    
    [SerializeField] private Sprite[] avatars;

    [SerializeField] private Image readyImage;
    [SerializeField] private Sprite[] readySprites;

    Player player;

    public bool isReady;

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
    }

    private void Start()
    {
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        isReady = false;
        PlayerPrefs.SetString("Ready", "false");
    }

    public void SetPlayerInfo(Player _player)
    {        
        playerName.text = _player.NickName;
        player = _player;
        UpdatePlayerItem(player);
    }

    public void ApplyLocalChanges()
    {
        backgroundImage.color = highlightColor;
        leftArrowButton.SetActive(true);
        rightArrowButton.SetActive(true);
        readyButton.SetActive(true);
    }

    public void OnClickLeftArrow()
    {
        if ((int)playerProperties["playerAvatar"] == 0)
        {
            playerProperties["playerAvatar"] = avatars.Length - 1;
        }
        else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] - 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void OnClickRightArrow()
    {
        if ((int)playerProperties["playerAvatar"] == avatars.Length - 1)
        {
            playerProperties["playerAvatar"] = 0;
        }
        else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] + 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void OnClickReadyButton()
    {
        if (!isReady)
        {
            playerProperties["isReady"] = 1;
            isReady = true;
            //PlayerPrefs.SetString("Ready", "true");
        }
        else if (isReady)
        {
            playerProperties["isReady"] = 0;
            isReady = false;
            //PlayerPrefs.SetString("Ready", "false");
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (targetPlayer == player)
        {
            UpdatePlayerItem(player);
        }
    }

    void UpdatePlayerItem(Player player)
    {
        if (player.CustomProperties.ContainsKey("playerAvatar"))
        {
            playerAvatar.sprite = avatars[(int)player.CustomProperties["playerAvatar"]];
            playerProperties["playerAvatar"] = (int)player.CustomProperties["playerAvatar"];
        }
        else
        {
            playerProperties["playerAvatar"] = 0;
        }

        if (player.CustomProperties.ContainsKey("isReady"))
        {
            readyImage.sprite = readySprites[(int)player.CustomProperties["isReady"]];
            playerProperties["isReady"] = (int)player.CustomProperties["isReady"];
        }
        else
        {
            playerProperties["isReady"] = 0;
        }
        //PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }
}
