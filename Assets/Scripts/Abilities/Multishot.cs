using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multishot : MonoBehaviour
{
    [SerializeField] private float multishotAngle = 10f;
    [SerializeField] private float bulletVelocity = 30f;

    private GameObject bulletPrefab;
    private Shooting shootScript;
    private float bulletLifeTime = 5f;
    private Camera playerCam;
    private Transform shootingPoint;

    private void Start()
    {
        shootScript = this.transform.root.GetComponentInChildren<Shooting>();
        //Debug.Log(shootScript);
        bulletPrefab = shootScript.bulletPrefab;
        playerCam = Camera.main;
        shootingPoint = GameObject.Find("ShootingPoint").transform;
    }

    public void DoMultishot()
    {
        Ray trace = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Ray leftTrace = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Ray rightTrace = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        leftTrace.direction = Quaternion.AngleAxis(multishotAngle, playerCam.transform.up) * playerCam.transform.forward;
        rightTrace.direction = Quaternion.AngleAxis(-multishotAngle, playerCam.transform.up) * playerCam.transform.forward;

        Vector3 targetPoint;
        Vector3 leftTargetPoint;
        Vector3 rightTargetPoint;

        targetPoint = FindHitPoint(trace);
        leftTargetPoint = FindHitPoint(leftTrace);
        rightTargetPoint = FindHitPoint(rightTrace);

        Vector3 middleDirection = targetPoint - shootingPoint.position;
        Vector3 leftDirection = leftTargetPoint - shootingPoint.position;
        Vector3 rightDirection = rightTargetPoint - shootingPoint.position;

        GameObject middleBullet = CreateBullet();
        GameObject leftBullet = CreateBullet();
        GameObject rightBullet = CreateBullet();

        middleBullet.GetComponent<Rigidbody>().AddForce(middleDirection.normalized * bulletVelocity, ForceMode.Impulse);
        leftBullet.GetComponent<Rigidbody>().AddForce(leftDirection.normalized * bulletVelocity, ForceMode.Impulse);
        rightBullet.GetComponent<Rigidbody>().AddForce(rightDirection.normalized * bulletVelocity, ForceMode.Impulse);

        StartCoroutine(DestroyBulletAfterTime(middleBullet, bulletLifeTime));
        StartCoroutine(DestroyBulletAfterTime(leftBullet, bulletLifeTime));
        StartCoroutine(DestroyBulletAfterTime(rightBullet, bulletLifeTime));
    }

    private Vector3 FindHitPoint(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            hit.collider.gameObject.GetComponentInParent<IDamageable>()?.Death();
            return hit.point;
        }
        return ray.GetPoint(100);
    }

    private GameObject CreateBullet()
    {
        return Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(bullet);
    }
}
