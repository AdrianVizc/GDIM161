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
            PhotonNetwork.Destroy(gameObject);
            if (!view.IsMine)
            {
                other.gameObject.GetComponentInParent<IDamageable>()?.Death();
            }
        }
    }
}
