using Photon.Pun.Demo.Asteroids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private Transform shootingPoint;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float reloadTimer;

    [SerializeField]
    private float bulletVelocity = 30f;

    [SerializeField]
    private float multishotAngle = 15f;
    private bool hasMultishot;

    private float bulletLifeTime = 5f;

    private float timer;

    private Camera playerCam;


    private void Start()
    {
        playerCam = Camera.main;
        timer = reloadTimer;
    }
    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();

                if (GetComponent<Multishot>() != null && GetComponent<Multishot>().isActiveAndEnabled)
                {
                    Multishot();
                }
                timer = reloadTimer;
            }
        }
    }

    public void Shoot()
    {
        //Raytracing
        Ray trace = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Through center of screen
        RaycastHit hit;

        //Checking if raycast hit something in front
        Vector3 targetPoint;
        if (Physics.Raycast(trace, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = trace.GetPoint(100);
        }

        //Calculate direction from shootingPoint to targetPoint
        Vector3 direction = targetPoint - shootingPoint.position;

        //Make bullet
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);

        //Rotating bullet to shoot direction
        //bullet.transform.forward = direction.normalized;

        //Give bullet force/movement to "shoot"
        bullet.GetComponent<Rigidbody>().AddForce(direction.normalized * bulletVelocity, ForceMode.Impulse);

        //Despawn
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletLifeTime));
    }

    public void Multishot()
    {
        Ray leftTrace = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Ray rightTrace = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        leftTrace.direction = Quaternion.AngleAxis(multishotAngle, playerCam.transform.up) * playerCam.transform.forward;
        rightTrace.direction = Quaternion.AngleAxis(-multishotAngle, playerCam.transform.up) * playerCam.transform.forward;

        Vector3 leftTargetPoint;
        Vector3 rightTargetPoint;

        if (Physics.Raycast(leftTrace, out RaycastHit hit))
        {
            leftTargetPoint = hit.point;
        }
        else
        {
            leftTargetPoint = leftTrace.GetPoint(100);
        }

        if (Physics.Raycast(rightTrace, out hit))
        {
            rightTargetPoint = hit.point;
        }
        else
        {
            rightTargetPoint = rightTrace.GetPoint(100);
        }

        Vector3 leftDirection = leftTargetPoint - shootingPoint.position;
        Vector3 rightDirection = rightTargetPoint - shootingPoint.position;

        GameObject leftBullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        GameObject rightBullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);

        leftBullet.GetComponent<Rigidbody>().AddForce(leftDirection.normalized * bulletVelocity, ForceMode.Impulse);
        rightBullet.GetComponent<Rigidbody>().AddForce(rightDirection.normalized * bulletVelocity, ForceMode.Impulse);

        StartCoroutine(DestroyBulletAfterTime(leftBullet, bulletLifeTime));
        StartCoroutine(DestroyBulletAfterTime(rightBullet, bulletLifeTime));

    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(bullet);
    }

}
