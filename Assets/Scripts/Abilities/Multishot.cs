using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multishot : MonoBehaviour
{
    [SerializeField] private float multishotAngle = 15f;
    [SerializeField] private float bulletVelocity = 30f;

    private GameObject bulletPrefab;
    private Shooting shootScript;
    private float bulletLifeTime = 5f;
    private Camera playerCam;
    private Transform shootingPoint;

    private void Start()
    {
        shootScript = this.transform.root.GetComponentInChildren<Shooting>();
        Debug.Log(shootScript);
        bulletPrefab = shootScript.bulletPrefab;
        playerCam = Camera.main;
        shootingPoint = GameObject.Find("ShootingPoint").transform;
    }

    public void DoMultishot()
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
