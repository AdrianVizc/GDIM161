using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerDamage : MonoBehaviourPunCallbacks, IDamageable
{
    PhotonView view;
    GameObject playerSpawnerObject;
    PlayerSpawner playerSpawner;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        playerSpawnerObject = GameObject.Find("PlayerSpawner");
        playerSpawner = playerSpawnerObject.GetComponent<PlayerSpawner>();
        //playerSpawner = PhotonView.Find((int)view.InstantiationData[0]).GetComponent<PlayerSpawner>();
    }

    public void Death()
    {
        view.RPC("RPC_Death", RpcTarget.All);   
    }


    [PunRPC]
    void RPC_Death()
    {
        if (!view.IsMine)
        {
            return;
        }
        Die();
    }

    private void Die()
    {
        playerSpawner.Respawn();
    }
}
