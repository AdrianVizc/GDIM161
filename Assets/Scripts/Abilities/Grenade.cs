using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField]
    private float explosionRadius;

    [SerializeField]
    private float explosionDelay;

    [SerializeField]
    private GameObject explosionEffect;

    private bool hasExploded;
    private float countdown;

    PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        countdown = explosionDelay;
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0.0f && !hasExploded)
        {
            Explode();
            Debug.Log("Kaboom");
            hasExploded = true;
        }
    }

    private void Explode()
    {
        // Instantiate the explosion effect across the network
        if (explosionEffect != null)
        {
            PhotonNetwork.Instantiate(explosionEffect.name, transform.position, Quaternion.identity);
        }

        // Damage players in range (only done by owner)
        Collider[] explosionRange = Physics.OverlapSphere(transform.position, explosionRadius);

        if (photonView.IsMine)
        {
            foreach (Collider collider in explosionRange)
            {
                if (collider.CompareTag("Player"))
                {
                    collider.gameObject.GetComponentInParent<IDamageable>()?.Death();
                    Debug.Log("Hit by: " + collider.name);
                }
            }
        }

        // Destroy the grenade across the network
        PhotonNetwork.Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }
}
