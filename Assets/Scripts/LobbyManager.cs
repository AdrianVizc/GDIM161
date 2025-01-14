using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField roomNameInputField;
    [SerializeField] private TMP_Text enteredRoomName;
    [SerializeField] private TMP_Text roomNameText;
    [SerializeField] private GameObject lobbyPanel;
    [SerializeField] private GameObject roomPanel;
    [SerializeField] private int maxPlayers;

    [SerializeField] private RoomItem roomItemPrefab;
    List<RoomItem> roomItemsList = new List<RoomItem>();

    [SerializeField] private Transform contentObject;

    private string noRoomNameEntered;

    private List<string> namesList = new List<string>();

    private void Start()
    {
        PhotonNetwork.JoinLobby();
        noRoomNameEntered = enteredRoomName.text;
    }

    public void OnClickCreate()
    {
        if (roomNameInputField.text.Length == 0 || enteredRoomName.text == noRoomNameEntered)
        {
            PhotonNetwork.CreateRoom(CreateRandomRoomName(), new RoomOptions() { MaxPlayers = maxPlayers});
        }
        else
        {
            PhotonNetwork.CreateRoom(enteredRoomName.text, new RoomOptions() { MaxPlayers = maxPlayers });
        }
    }

    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        roomNameText.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateRoomList(roomList);
    }

    private void UpdateRoomList(List<RoomInfo> list)
    {
        foreach (RoomItem item in  roomItemsList)
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
}
