using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Multishot : MonoBehaviour
{
    [SerializeField] private float multishotAngle = 10f;

    private Shooting shootScript;
    private Camera playerCam;
    private Transform shootingPoint;

    private void Start()
    {
        shootScript = this.transform.root.GetComponentInChildren<Shooting>();
        playerCam = Camera.main;
        shootingPoint = GameObject.Find("ShootingPoint")?.transform;

        if (shootingPoint == null)
            Debug.LogError("Multishot: ShootingPoint not found.");
    }

    public void DoMultishot()
    {
        // Play muzzle flash once
        shootScript.ShootingSystem?.Play();

        // Prepare ray directions for multishot
        Ray centerRay = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Ray leftRay = new Ray(centerRay.origin, Quaternion.AngleAxis(-multishotAngle, playerCam.transform.up) * playerCam.transform.forward);
        Ray rightRay = new Ray(centerRay.origin, Quaternion.AngleAxis(multishotAngle, playerCam.transform.up) * playerCam.transform.forward);

        FireRay(centerRay);
        FireRay(leftRay);
        FireRay(rightRay);
    }

    private void FireRay(Ray ray)
    {
        Vector3 hitPoint;

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~0, QueryTriggerInteraction.Collide))
        {
            // Damage
            hit.collider.gameObject.GetComponentInParent<IDamageable>()?.Death();

            // Impact effect
            PhotonNetwork.Instantiate(shootScript.ImpactParticleSystem.name, hit.point, Quaternion.LookRotation(hit.normal));
            hitPoint = hit.point;
        }
        else
        {
            hitPoint = ray.GetPoint(100f); // arbitrary long distance if no hit
        }

        // Bullet trail
        GameObject trailObject = PhotonNetwork.Instantiate(shootScript.BulletTrail.name, shootingPoint.position, Quaternion.identity);
        TrailRenderer trail = trailObject.GetComponent<TrailRenderer>();
        shootScript.StartCoroutine(SpawnTrail(trail, hitPoint));
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 hitPoint)
    {
        float time = 0f;
        Vector3 startPosition = trail.transform.position;

        while (time < 1f)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hitPoint, time);
            time += Time.deltaTime / trail.time;
            yield return null;
        }

        trail.transform.position = hitPoint;
        Destroy(trail.gameObject, trail.time);
    }
}
