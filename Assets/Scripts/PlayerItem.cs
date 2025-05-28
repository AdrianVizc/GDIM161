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
    [SerializeField] private Material[] gnomeMaterials;

    [SerializeField] private Image readyImage;
    [SerializeField] private Sprite[] readySprites;

    Player player;
    private GameObject selectAbilityButton;
    private GameObject selectAbilityButtonUI;
    LobbyManager lobbyManager;

    private bool firstTimeReady;

    private GameObject gnomeModel;
    private GameObject gnomeModelMaterial;
    private Material[] currMaterials;
    private int gnomeIndex = 0;

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
    }

    private void Start()
    {
        StartCoroutine(InitializeAfterSync());

        gnomeModel = GameObject.FindGameObjectWithTag("ui_gnome_model");
        foreach(Transform child in gnomeModel.transform)
        {
            if(child.name == "GnomeIdle")
            {
                foreach(Transform subchild in child)
                {
                    if(subchild.name == "Icosphere.001")
                    {
                        currMaterials = subchild.gameObject.GetComponent<SkinnedMeshRenderer>().materials;
                        gnomeModelMaterial = subchild.gameObject;
                        // currMaterials[3] = gnomeMaterials[0];
                        // subchild.gameObject.GetComponent<SkinnedMeshRenderer>().materials = gnomeMaterials;
                        break;
                    }
                }
                break;
            }
        }
        firstTimeReady = false;
        playerProperties["isReady"] = 0;
        selectAbilityButton = GameObject.Find("EditCharacterUI");
        selectAbilityButtonUI = GameObject.Find("EditCharacterButton");
        lobbyManager = GameObject.Find("LobbyManager").GetComponent<LobbyManager>();
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
    }

    private void Update()
    {
        if (firstTimeReady) 
        {
            if ((int)playerProperties["isReady"] == 1)
            {
                leftArrowButton.SetActive(false);
                rightArrowButton.SetActive(false);
                selectAbilityButton.SetActive(false);
                selectAbilityButtonUI.SetActive(false);
                lobbyManager.isReady = true;
            }
            else if ((int)playerProperties["isReady"] == 0)
            {
                leftArrowButton.SetActive(true);
                rightArrowButton.SetActive(true);
                if (!lobbyManager.triPanelSpacingIsShown)
                {
                    selectAbilityButton.SetActive(true);
                    selectAbilityButtonUI.SetActive(true);
                }                
                lobbyManager.isReady = false;
            }
        }        
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
            gnomeIndex = gnomeMaterials.Length - 1;
            currMaterials[3] = gnomeMaterials[gnomeIndex];
            gnomeModelMaterial.gameObject.GetComponent<SkinnedMeshRenderer>().materials = currMaterials;
        }
        else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] - 1;
            currMaterials[3] = gnomeMaterials[--gnomeIndex];
            gnomeModelMaterial.gameObject.GetComponent<SkinnedMeshRenderer>().materials = currMaterials;
        }
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
    }

    public void OnClickRightArrow()
    {
        if ((int)playerProperties["playerAvatar"] == avatars.Length - 1)
        {
            playerProperties["playerAvatar"] = 0;
            gnomeIndex = 0;
            currMaterials[3] = gnomeMaterials[gnomeIndex];
            gnomeModelMaterial.gameObject.GetComponent<SkinnedMeshRenderer>().materials = currMaterials;
        }
        else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] + 1;
            currMaterials[3] = gnomeMaterials[++gnomeIndex];
            gnomeModelMaterial.gameObject.GetComponent<SkinnedMeshRenderer>().materials = currMaterials;
        }
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
    }

    public void OnClickReadyButton()
    {
        firstTimeReady = true;
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
