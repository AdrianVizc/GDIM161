using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System.Security.Cryptography;
using System.Linq;
using System.Runtime.CompilerServices;

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
    [SerializeField] private GameObject editCharacterUI;
    [SerializeField] private GameObject leaveRoomButton;
    [SerializeField] private GameObject playerListing;
    [SerializeField] private int maxPlayers;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject playButtonUI;
    [SerializeField] private GameObject roomNameErrorText;
    [SerializeField] private string sceneName;
    [SerializeField] private int amountOfPlayersReadyNeeded = 2;


    [SerializeField] private RoomItem roomItemPrefab;
    List<RoomItem> roomItemsList = new List<RoomItem>();

    [SerializeField] private Transform contentObject;

    [HideInInspector] public List<PlayerItem> playerItemsList = new List<PlayerItem>();
    [SerializeField] private PlayerItem playerItemPrefab;
    [SerializeField] private Transform playerItemParent;

    private string noRoomNameEntered;

    private Vector2 frontPos;
    private Vector2 backPos;

    private List<string> namesList = new List<string>();

    private bool playClicked;
    private bool allReady;
    public bool isReady;

    private void Start()
    {
        ResetCustomProperties();
        PhotonNetwork.JoinLobby();
        noRoomNameEntered = enteredRoomName.text;
        loadingScreen.SetActive(false);
        backButton.SetActive(false);
        editCharacterButton.SetActive(false);
        editCharacterUI.SetActive(false);
        leaveRoomButton.SetActive(false);
        lobbyPanel.SetActive(true);
        roomPanel.SetActive(false);

        frontPos = new Vector2(0, -28);
        backPos = new Vector2(0, 1000);

        playClicked = false;
        isReady = false;
    }

    private void Update()
    {
        Debug.Log(isReady);
        if (PhotonNetwork.InRoom)
        {
            int totalPlayersInRoom = PhotonNetwork.CurrentRoom.PlayerCount;
            int amountOfPlayersReady = 0;
            foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
            {
                Player playerObject = player.Value;
                if (playerObject.CustomProperties.ContainsKey("isReady"))
                {
                    int isReady = (int)playerObject.CustomProperties["isReady"];
                    if (isReady == 1)
                    {
                        amountOfPlayersReady++;
                        if (totalPlayersInRoom == amountOfPlayersReady)
                        {
                            allReady = true;
                            //Debug.Log("allReady is True");
                        }                        
                    }
                    else
                    {
                        allReady = false;
                        //Debug.Log("allReady is False");
                    }
                }
            }
        }
        
        if (PhotonNetwork.IsMasterClient && allReady && PhotonNetwork.CurrentRoom.PlayerCount >= amountOfPlayersReadyNeeded)
        {
            playButton.SetActive(true);
            playButtonUI.SetActive(true);
        }
        else
        {
            playButton.SetActive(false);
            playButtonUI.SetActive(false);
        }
    }

    public void OnClickPlayButton()
    {
        playClicked = true;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.LoadLevel(sceneName);
    }

    public void OnClickCreate()
    {
        if (roomNameInputField.text.Length == 0 || enteredRoomName.text == noRoomNameEntered)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.CleanupCacheOnLeave = true;
            PhotonNetwork.CreateRoom(CreateRandomRoomName(), new RoomOptions() { MaxPlayers = maxPlayers, BroadcastPropsChangeToAll = true });
        }
        else
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.CleanupCacheOnLeave = true;
            PhotonNetwork.CreateRoom(enteredRoomName.text, new RoomOptions() { MaxPlayers = maxPlayers, BroadcastPropsChangeToAll = true });
        }
    }
    public override void OnCreatedRoom()
    {
        loadingScreen.SetActive(true);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        if (roomNameErrorText.activeSelf)
        {
            roomNameErrorText.SetActive(false);
            roomNameErrorText.SetActive(true);
        }
        else
        {
            roomNameErrorText.SetActive(true);
        }
        
    }

    private void ResetCustomProperties()
    {
        Hashtable resetProperties = new Hashtable();
        resetProperties["kills"] = 0;
        resetProperties["deaths"] = 0;
        resetProperties["isReady"] = 0;
        resetProperties["playerAvatar"] = 0;
        PhotonNetwork.LocalPlayer.SetCustomProperties(resetProperties);
    }

    public override void OnJoinedRoom()
    {
        ResetCustomProperties();

        lobbyPanel.SetActive(false);
        leaveRoomButton.SetActive(true);
        editCharacterButton.SetActive(true);
        editCharacterUI.SetActive(true);
        roomPanel.SetActive(true);
        loadingScreen.SetActive(false);
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        triPanelSpacing.transform.localPosition = backPos;

        UpdatePlayerList();
        isReady = false;
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(RoomInfo roomInfo in roomList)
        {
            if (!roomInfo.RemovedFromList)
            {
                bool roomExists = false;

                foreach (Transform transform in contentObject)
                {
                    TMP_Text text = transform.GetComponentInChildren<TMP_Text>();
                    if (roomInfo.Name == text.text)
                    {
                        roomExists = true;
                        break;
                    }
                }
                if (roomExists) continue;
                {
                    RoomItem newRoom = Instantiate(roomItemPrefab, contentObject);
                    newRoom.SetRoomName(roomInfo.Name);
                }
                
            }
            if (roomInfo.RemovedFromList)
            {
                foreach (Transform transform in contentObject)
                {
                    TMP_Text text = transform.GetComponentInChildren<TMP_Text>();
                    if (roomInfo.Name == text.text)
                    {
                        Destroy(transform.gameObject);
                    }
                }
            }
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
        ResetCustomProperties();

        loadingScreen.SetActive(true);
        editCharacterButton.SetActive(false);
        editCharacterUI.SetActive(false);
        leaveRoomButton.SetActive(false);

        loadingScreen.SetActive(true);

        

        StartCoroutine(WaitForReconnect());
    }

    private IEnumerator WaitForReconnect() 
    {
        PhotonNetwork.Disconnect();

        yield return new WaitUntil(() => !PhotonNetwork.IsConnected);

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        loadingScreen.SetActive(false);
        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }

    public void AbilityScreenBackButton()
    {
        leaveRoomButton.SetActive(true);
        editCharacterButton.SetActive(true);
        editCharacterUI.SetActive(true);
        backButton.SetActive(false);

        triPanelSpacing.transform.localPosition = backPos;
        playerListing.transform.localPosition = frontPos;
    }

    public void EditCharacter()
    {
        editCharacterButton.SetActive(false);
        editCharacterUI.SetActive(false);
        leaveRoomButton.SetActive(false);
        backButton.SetActive(true);

        triPanelSpacing.transform.localPosition = frontPos;
        playerListing.transform.localPosition = backPos;
    }

    private void UpdatePlayerList()
    {
        editCharacterButton.SetActive(true);
        editCharacterUI.SetActive(true);

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
        if (PhotonNetwork.CurrentRoom.PlayerCount >= PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsVisible = false;
        }
        UpdatePlayerList();
        isReady = false;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount < PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsVisible = true;
        }
        UpdatePlayerList();
        isReady = false;
    }
}
