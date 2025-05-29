using Photon.Pun;
using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Shooting Setup")]
    [SerializeField]
    private Transform shootingPoint;

    [SerializeField]
    public float reloadTimer = 0.5f;

    public float timer;

    private Camera playerCam;
    private PhotonView view;

    [Header("Effects")]
    [SerializeField]
    public ParticleSystem ShootingSystem;

    [SerializeField]
    public ParticleSystem ImpactParticleSystem;

    [SerializeField]
    public TrailRenderer BulletTrail;

    private void Awake()
    {
        view = GetComponentInParent<PhotonView>();
    }

    private void Start()
    {
        playerCam = Camera.main;
        timer = reloadTimer;
    }

    private void Update()
    {
        if (!view.IsMine || InGameUI.globalInputLock)
            return;

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
            timer = reloadTimer;
        }
    }

    public void Shoot()
    {
        // Play muzzle flash
        ShootingSystem?.Play();

        // Raycast from center of screen
        Ray trace = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(trace, out hit, Mathf.Infinity, ~0, QueryTriggerInteraction.Collide))
        {
            targetPoint = hit.point;

            // Damage logic
            if (hit.collider.CompareTag("Player"))
            {
                hit.collider.GetComponentInParent<IDamageable>()?.Death();
            }

            // Offset impact particle slightly away from the wall
            Vector3 spawnPosition = hit.point + hit.normal * 0.1f;
            Quaternion rotation = Quaternion.LookRotation(hit.normal);
            PhotonNetwork.Instantiate(ImpactParticleSystem.name, spawnPosition, rotation);

            // Trail to hit
            GameObject trailObject = PhotonNetwork.Instantiate(BulletTrail.name, shootingPoint.position, Quaternion.identity);
            TrailRenderer trail = trailObject.GetComponent<TrailRenderer>();
            StartCoroutine(SpawnTrail(trail, hit.point));
        }
        else
        {
            // Missed — shoot trail into the distance
            targetPoint = trace.GetPoint(100f);
            GameObject trailObject = PhotonNetwork.Instantiate(BulletTrail.name, shootingPoint.position, Quaternion.identity);
            TrailRenderer trail = trailObject.GetComponent<TrailRenderer>();
            StartCoroutine(SpawnTrail(trail, targetPoint));
        }
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 hitPoint)
    {
        float time = 0;
        Vector3 startPos = trail.transform.position;

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPos, hitPoint, time);
            time += Time.deltaTime / trail.time;
            yield return null;
        }

        trail.transform.position = hitPoint;
        PhotonNetwork.Destroy(trail.gameObject);
    }

    public void ReduceShootRate(float rate)
    {
        reloadTimer /= rate;
    }

    public void ResetBulletSpeed(float rate)
    {
        reloadTimer *= rate;
    }
}
