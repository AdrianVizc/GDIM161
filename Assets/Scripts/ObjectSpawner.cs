using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Rive;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject objectToSpawn;

    PhotonView photonView;

    private void Start()
    {
        /*BarrierForPhotonView barrierForPhotonView = GetComponentInParent<BarrierForPhotonView>();
        photonView = barrierForPhotonView.GetComponent<PhotonView>();*/
    }

    public void Spawn()
    {
        PhotonNetwork.Instantiate(objectToSpawn.name, player.transform.position, Quaternion.identity); //for photon
        //Instantiate(objectToSpawn, player.transform.position, Quaternion.identity);

        /*if (objectToSpawn.name == "Barrier")
        {
            photonView.RPC("RPCSpawn", RpcTarget.AllBuffered);
        }*/
    }
}
