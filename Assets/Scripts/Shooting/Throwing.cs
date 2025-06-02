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
        //offset bomb spawn a bit so it doesnt go through ground
        Vector3 spawnPos = throwingPoint.position + Vector3.up * 0.5f;
        GameObject nade = PhotonNetwork.Instantiate(grenadePrefab.name, spawnPos, throwingPoint.rotation);
        Rigidbody rb = nade.GetComponent<Rigidbody>();
        rb.velocity = throwingPoint.forward * throwingForce;
    }
}
