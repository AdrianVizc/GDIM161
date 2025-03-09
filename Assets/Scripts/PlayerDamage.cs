using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerDamage : MonoBehaviourPunCallbacks, IDamageable
{
    PhotonView view;
    PlayerSpawner playerSpawner;

    private void Awake()
    {
        view = GetComponent<PhotonView>();

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

        Debug.Log("died");

        Die();
    }

    private void Die()
    {
        playerSpawner.Die();
    }
}
