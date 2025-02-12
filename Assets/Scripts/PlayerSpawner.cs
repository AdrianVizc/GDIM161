using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] playerPrefabs;
    [SerializeField] Transform[] spawnPoints;

    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.LocalPlayer.IsLocal)
        {
            int randomNumber = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomNumber];
            GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
            GameObject player = PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);

            PhotonView playerPhotonView = player.GetComponent<PhotonView>();
            playerPhotonView.TransferOwnership(PhotonNetwork.LocalPlayer);
        }
    }
}
