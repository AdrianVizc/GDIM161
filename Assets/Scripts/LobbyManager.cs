﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField roomNameInputField;
    [SerializeField] private TMP_Text enteredRoomName;
    [SerializeField] private TMP_Text roomNameText;
    [SerializeField] private GameObject lobbyPanel;
    [SerializeField] private GameObject roomPanel;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject triPanelSpacing;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject editCharacterButton;
    [SerializeField] private GameObject leaveRoomButton;
    [SerializeField] private GameObject playerListing;
    [SerializeField] private int maxPlayers;
    [SerializeField] private GameObject playButton;
    [SerializeField] private string sceneName;


    [SerializeField] private RoomItem roomItemPrefab;
    List<RoomItem> roomItemsList = new List<RoomItem>();

    [SerializeField] private Transform contentObject;

    [HideInInspector] public List<PlayerItem> playerItemsList = new List<PlayerItem>();
    [SerializeField] private PlayerItem playerItemPrefab;
    [SerializeField] private Transform playerItemParent;

    private string noRoomNameEntered;

    private float timeBetweenUpdates = 1.5f;
    private float nextUpdateTime;

    private List<string> namesList = new List<string>();
    public bool playClicked;

    private void Start()
    {
        PhotonNetwork.JoinLobby();
        noRoomNameEntered = enteredRoomName.text;
        loadingScreen.SetActive(false);
        backButton.SetActive(false);
        editCharacterButton.SetActive(false);
        leaveRoomButton.SetActive(false);
        triPanelSpacing.SetActive(false);
        lobbyPanel.SetActive(true);
        roomPanel.SetActive(false);

        playClicked = false;
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 1)
        {
            playButton.SetActive(true);
        }
        else
        {
            playButton.SetActive(false);
        }
    }

    public void OnClickPlayButton()
    {
        playClicked = true;
        PhotonNetwork.LoadLevel(sceneName);
    }

    public void OnClickCreate()
    {
        if (roomNameInputField.text.Length == 0 || enteredRoomName.text == noRoomNameEntered)
        {
            PhotonNetwork.CreateRoom(CreateRandomRoomName(), new RoomOptions() { MaxPlayers = maxPlayers, BroadcastPropsChangeToAll = true });
        }
        else
        {
            PhotonNetwork.CreateRoom(enteredRoomName.text, new RoomOptions() { MaxPlayers = maxPlayers, BroadcastPropsChangeToAll = true });
        }
        loadingScreen.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false);
        leaveRoomButton.SetActive(true);
        editCharacterButton.SetActive(true);
        roomPanel.SetActive(true);
        loadingScreen.SetActive(false);
        roomNameText.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;

        UpdatePlayerList();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBetweenUpdates;
        }        
    }

    private void UpdateRoomList(List<RoomInfo> list) //Need to fix. Not updating correctly
    {
        foreach (RoomItem item in roomItemsList)
        {
            Destroy(item.gameObject);
        }
        roomItemsList.Clear();

        foreach (RoomInfo room in list)
        {           
            RoomItem newRoom = Instantiate(roomItemPrefab, contentObject);
            newRoom.SetRoomName(room.Name);
            roomItemsList.Add(newRoom);
        }
    }

    private string CreateRandomRoomName()
    {
        int randomNumber = Random.Range(0, 102);

        namesList.Add("Usual Room ");
        namesList.Add("Funny Farm ");
        namesList.Add("Lobby ");
        namesList.Add("Game Room ");
        namesList.Add("Waiting Area ");
        namesList.Add("Lounge ");
        namesList.Add("Session ");
        namesList.Add("Common Area ");
        namesList.Add("Group Session ");
        namesList.Add("Meeting Point ");
        namesList.Add("Room ");
        namesList.Add("Universal Hub ");
        namesList.Add("Hub ");
        namesList.Add("Hangout ");
        namesList.Add("Retreat ");
        namesList.Add("Chamber ");
        namesList.Add("Torture Room ");
        namesList.Add("Dom's Basement ");
        namesList.Add("Nick's Basement ");
        namesList.Add("Adrian's Basement ");
        namesList.Add("Jacky's Basement ");
        namesList.Add("Jack's Basement ");
        namesList.Add("Thomas's Basement ");
        namesList.Add("Courtyard ");
        namesList.Add("Hall ");
        namesList.Add("Closet ");
        namesList.Add("Den ");
        namesList.Add("Parlor ");
        namesList.Add("Saloon ");
        namesList.Add("Secret Hideout ");
        namesList.Add("Rendezvous  ");


        return namesList[Random.Range(0, namesList.Count)] + randomNumber.ToString();
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();    
    }

    public void OnClickLeaveLobby()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("MainMenu");
    }

    public override void OnLeftRoom()
    {
        loadingScreen.SetActive(true);
        editCharacterButton.SetActive(false);
        leaveRoomButton.SetActive(false);

        PhotonNetwork.Disconnect();

        StartCoroutine(WaitForReconnect());
    }

    private IEnumerator WaitForReconnect() 
    {
        loadingScreen.SetActive(true);
        while (PhotonNetwork.IsConnectedAndReady)
        {
            loadingScreen.SetActive(true);
            yield return null;
        }

        PhotonNetwork.ConnectUsingSettings();

        while (!PhotonNetwork.IsConnectedAndReady)
        {
            loadingScreen.SetActive(true);
            yield return null;
        }
        if (PhotonNetwork.IsConnectedAndReady)
        {
            yield return new WaitForSeconds(2f); //fix so you dont need delay (after loading back into lobby you should be able to immediately click create room without it crashing).
            loadingScreen.SetActive(false);
            roomPanel.SetActive(false);
            lobbyPanel.SetActive(true);
            yield return null;
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public void AbilityScreenBackButton()
    {
        leaveRoomButton.SetActive(true);
        editCharacterButton.SetActive(true);
        backButton.SetActive(false);
        triPanelSpacing.SetActive(false);
        playerListing.SetActive(true);
    }

    public void EditCharacter()
    {
        editCharacterButton.SetActive(false);
        leaveRoomButton.SetActive(false);
        triPanelSpacing.SetActive(true);
        backButton.SetActive(true);
        playerListing.SetActive(false);
    }

    private void UpdatePlayerList()
    {
        foreach (PlayerItem item in playerItemsList)
        {
            Destroy(item.gameObject);
        }
        playerItemsList.Clear();

        if (PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
            newPlayerItem.SetPlayerInfo(player.Value);
            if (player.Value == PhotonNetwork.LocalPlayer)
            {
                newPlayerItem.ApplyLocalChanges();
            }

            playerItemsList.Add(newPlayerItem);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }
}
