using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomItem : MonoBehaviour
{
    [SerializeField] private TMP_Text roomName;

    public void SetRoomName(string _roomName)
    {
        roomName.text = _roomName;
    }
}
