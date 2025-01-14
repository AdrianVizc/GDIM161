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

    private float bulletLifeTime = 5f;

    private bool reloading; 

    private Camera playerCam;

    private Vector3 lookDir;

    private void Start()
    {
        playerCam = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        lookDir = playerCam.transform.forward;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void Shoot()
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

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(bullet);
    }

}
