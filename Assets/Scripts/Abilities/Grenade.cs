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

    // Start is called before the first frame update
    void Start()
    {
        countdown = explosionDelay;
    }

    // Update is called once per frame
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
        //Insert Explosion Effect logic...

        Collider[] explosionRange = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in explosionRange)
        {
            if (collider.CompareTag("Player"))
            {          
                collider.gameObject.GetComponentInParent<IDamageable>()?.Death();
                Debug.Log("Hit by: " + collider.name);
            }
        }

        PhotonNetwork.Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }
}
