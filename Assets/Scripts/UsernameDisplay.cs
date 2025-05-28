using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class UsernameDisplay : MonoBehaviour
{
    [SerializeField] private PhotonView playerPV;
    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_Text textFacecam;

    private void Start()
    {
        text.text = playerPV.Owner.NickName;
        textFacecam.text = playerPV.Owner.NickName;
    }
}
