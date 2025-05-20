using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierFollowPlayer : MonoBehaviour
{
    PhotonView photonView;
    GameObject objectCoords;

    private void Start()
    {
        GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in playerList)
        {
            if (player.GetComponentInParent<PhotonView>().IsMine)
            {
                objectCoords = player;
            }
        }
    }

    private void Update()
    {
        if (objectCoords != null && GetComponent<PhotonView>().IsMine)
        {
            transform.position = objectCoords.transform.position;
        }
    }
}
