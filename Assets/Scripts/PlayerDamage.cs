using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Photon.Realtime;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerDamage : MonoBehaviourPunCallbacks, IDamageable
{
    private PhotonView view;

    GameObject playerSpawnerObject;
    PlayerSpawner playerSpawner;

    private int kills;
    private int deaths;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        playerSpawnerObject = GameObject.Find("PlayerSpawner");
        playerSpawner = playerSpawnerObject.GetComponent<PlayerSpawner>();
    }

    public void Death()
    {
        view.RPC(nameof(RPC_Death), view.Owner);   
    }


    [PunRPC]
    void RPC_Death(PhotonMessageInfo info)
    {
        Die();
        PlayerDamage.Find(info.Sender).GetKill();
    }

    private void Die()
    {
        playerSpawner.Respawn();
        deaths++;

        Hashtable hash = new Hashtable();
        hash.Add("deaths", deaths);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }

    private void GetKill()
    {
        view.RPC(nameof(RPC_GetKill), view.Owner);
    }

    [PunRPC]
    private void RPC_GetKill()
    {
        kills++;

        Hashtable hash = new Hashtable();
        hash.Add("kills", kills);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }

    public static PlayerDamage Find(Player player)
    {
        return FindObjectsOfType<PlayerDamage>().SingleOrDefault(x => x.view.Owner == player);
    }
}
