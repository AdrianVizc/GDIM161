using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;
using Photon.Realtime;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] playerPrefabs;
    [SerializeField] Transform[] spawnPoints;
    GameObject player;

    private void Start()
    {
        int randomNumber = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomNumber];
        GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        player = PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);             
        playerToSpawn.GetComponentInChildren<AbilityToPlayer>().SetAbilities(playerToSpawn);
        Debug.Log("Player Spawn: " + playerToSpawn.name);
    }

    public void Respawn()
    {
        int randomNumber = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomNumber];
        player.transform.Find("PlayerCapsule").position = spawnPoint.position;
    }
}
