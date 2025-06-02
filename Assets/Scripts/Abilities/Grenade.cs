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

    private PhotonView photonView;

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
        // Spawn explosion effect network-wide
        if (explosionEffect != null)
        {
            GameObject explosionInstance = PhotonNetwork.Instantiate(explosionEffect.name, transform.position, Quaternion.identity);

            // Scale effect based on explosion radius
            float scale = explosionRadius * 1.5f; // diameter
            explosionInstance.transform.localScale = new Vector3(scale, scale, scale);
        }

        // Damage players in range (owner only)
        if (photonView.IsMine)
        {
            Collider[] explosionRange = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach (Collider collider in explosionRange)
            {
                if (collider.CompareTag("Player"))
                {
                    collider.gameObject.GetComponentInParent<IDamageable>()?.Death();
                    Debug.Log("Hit by: " + collider.name);
                }
            }
        }

        // Destroy grenade network-wide
        PhotonNetwork.Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }
}
