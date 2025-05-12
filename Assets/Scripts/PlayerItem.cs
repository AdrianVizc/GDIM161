using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerItem : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text playerName;

    private Image backgroundImage;
    [SerializeField] private Color highlightColor;
    [SerializeField] private GameObject leftArrowButton;
    [SerializeField] private GameObject rightArrowButton;
    [SerializeField] private GameObject readyButton;

    Hashtable playerProperties = new Hashtable();
    [SerializeField] private Image playerAvatar;    
    [SerializeField] private Sprite[] avatars;

    [SerializeField] private Image readyImage;
    [SerializeField] private Sprite[] readySprites;

    Player player;

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
    }

    private void Start()
    {
        StartCoroutine(InitializeAfterSync());
    }

    private IEnumerator InitializeAfterSync()
    {
        while (!player.CustomProperties.ContainsKey("isReady") ||
               !player.CustomProperties.ContainsKey("playerAvatar"))
        {
            yield return null;
        }

        playerProperties["isReady"] = player.CustomProperties["isReady"];
        playerProperties["playerAvatar"] = player.CustomProperties["playerAvatar"];

        UpdatePlayerItem(player);
    }

    private void Update()
    {
        Debug.Log("Avatar: " + playerProperties["playerAvatar"]);
        Debug.Log("Ready: " + playerProperties["isReady"]);
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

    public int GetPlayerActorNumber()
    {
        return player.ActorNumber;
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
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
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
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
    }

    public void OnClickReadyButton()
    {
        if ((int)playerProperties["isReady"] == 0)
        {
            playerProperties["isReady"] = 1;
        }
        else if ((int)playerProperties["isReady"] == 1)
        {
            playerProperties["isReady"] = 0;
        }
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
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
        }

        if (player.CustomProperties.ContainsKey("isReady"))
        {
            readyImage.sprite = readySprites[(int)player.CustomProperties["isReady"]];
        }
    }
}
