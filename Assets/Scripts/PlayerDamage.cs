using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        Debug.Log("before rpcdeath");
    }
    
    public void OnChildCollision(Collision collision)
    {
        view.RPC(nameof(RPC_FellOff), view.Owner);
    }

    [PunRPC]
    void RPC_FellOff()
    {
        Die();
    }

    [PunRPC]
    void RPC_Death(PhotonMessageInfo info)
    {
        Die();
        if(view.Owner != info.Sender)
        {
            PlayerDamage.Find(info.Sender).GetKill();
            Debug.Log(info.Sender);
        }
        

        Debug.Log("rpcdeath");
    }

    public void Die()
    {
        playerSpawner.Respawn();
        deaths++;

        Hashtable hash = new Hashtable();
        hash.Add("deaths", deaths);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

        if (PlayerPrefs.HasKey("Deaths"))
        {
            int Deaths = PlayerPrefs.GetInt("Deaths");
            Deaths++;
            PlayerPrefs.SetInt("Deaths", Deaths);
        }
        else
        {
            PlayerPrefs.SetInt("Deaths", 1);
        }
        PlayerPrefs.Save();
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

        if (PlayerPrefs.HasKey("Kills"))
        {
            int Kills = PlayerPrefs.GetInt("Kills");
            Kills++;
            PlayerPrefs.SetInt("Kills", Kills);
        }
        else
        {
            PlayerPrefs.SetInt("Kills", 1);
        }
        PlayerPrefs.Save();
    }

    public static PlayerDamage Find(Player player)
    {
        return FindObjectsOfType<PlayerDamage>().SingleOrDefault(x => x.view.Owner == player);
    }
}
