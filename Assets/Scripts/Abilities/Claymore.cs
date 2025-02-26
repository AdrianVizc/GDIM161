using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Claymore : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //blow up do dmg
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
