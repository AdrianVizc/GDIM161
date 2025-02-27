using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject objectToSpawn;

    public void Spawn()
    {
        //PhotonNetwork.Instantiate(objectToSpawn.name, player.transform.position, Quaternion.identity); //for photon
        Instantiate(objectToSpawn, player.transform.position, Quaternion.identity);
    }
}
