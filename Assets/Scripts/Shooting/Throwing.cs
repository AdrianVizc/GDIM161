using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Throwing : MonoBehaviour
{
    [SerializeField]
    private Transform throwingPoint; //same as shootingPoint object

    [SerializeField]
    private GameObject grenadePrefab;

    [SerializeField]
    private float throwingForce;

    public void ThrowGrenade()
    {
        GameObject nade = PhotonNetwork.Instantiate(grenadePrefab.name, throwingPoint.position, throwingPoint.rotation);
        Rigidbody rb = nade.GetComponent<Rigidbody>();
        rb.velocity = throwingPoint.forward * throwingForce;
    }
}
