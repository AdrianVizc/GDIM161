using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Claymore : MonoBehaviour
{
    PhotonView view;
    private void Awake()
    {
        view = GetComponentInParent<PhotonView>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //blow up do dmg
            Debug.Log($"Mine triggered by {other.name}, ownership: {view.Owner.NickName}");
            if (!view.IsMine) // If this player is not the owner, transfer ownership first
            {
                view.TransferOwnership(PhotonNetwork.LocalPlayer);
            }
            PhotonNetwork.Destroy(gameObject);
            /*view.RPC(nameof(RPC_DestroyMine), RpcTarget.MasterClient);
            if (view.Owner == null)
            {
                Debug.LogError("Mine has no owner!");
            }
            else
            {
                Debug.LogError("Mine has owner!");
            }
            if (view.IsMine)
            {
                other.GetComponentInParent<IDamageable>()?.Death();
            }*/
        }
    }

    /*[PunRPC]
    void RPC_DestroyMine()
    {
        Debug.Log($"RPC Received by: {PhotonNetwork.NickName}. Is MasterClient: {PhotonNetwork.IsMasterClient}");
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }*/
    }
