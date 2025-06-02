using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShieldAbility : Ability
{
    public override void Activate(GameObject parent)
    {
        //Movement player = parent.GetComponent<Movement>();

        //player.playerShield.SetActive(true);

        //Shield barrier = parent.GetComponentInChildren<Shield>();
        //barrier.playerShield.SetActive(true);
        //barrier.Show();
        //barrier.SetDurability(3);
        //Debug.Log("Health: " + 3);

        ObjectSpawner spawnShield = GameObject.Find("Shield").GetComponent<ObjectSpawner>();
        PhotonView photonView;
        photonView = parent.gameObject.GetComponentInParent<PhotonView>();
        if (photonView != null)
        {
            if (photonView.IsMine)
            {
                GameObject shield = GameObject.Find("BarrierOverlay");
                if (shield != null)
                {
                    CanvasGroup canvasGroup = shield.GetComponent<CanvasGroup>();
                    canvasGroup.alpha = 1;
                }
                else
                {
                    Debug.Log("shield overlay null");
                }
            }
        }
        else
        {
            Debug.Log("photon null");
        }
        
        spawnShield.Spawn();

        
    }

    public override void BeginCooldown(GameObject parent)
    {
        //Movement player = parent.GetComponent<Movement>();
        //player.playerShield.SetActive(false);

        //Shield barrier = parent.GetComponentInChildren<Shield>();
        //barrier.playerShield.SetActive(false);
        //barrier.Hide();
        //shield.gameObject.SetActive(true);

        PhotonView photonView;
        photonView = parent.gameObject.GetComponentInParent<PhotonView>();
        if (photonView != null)
        {
            if (photonView.IsMine)
            {
                GameObject shield = GameObject.Find("BarrierOverlay");
                if (shield != null)
                {
                    CanvasGroup canvasGroup = shield.GetComponent<CanvasGroup>();
                    canvasGroup.alpha = 0;
                }
                else
                {
                    Debug.Log("shield overlay null");
                }
            }
        }
        else
        {
            Debug.Log("photon null");
        }
    }
}
