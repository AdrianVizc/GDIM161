using Photon.Pun;
using Photon.Pun.Demo.Asteroids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private Transform shootingPoint;

    [SerializeField]
    public GameObject bulletPrefab;

    [SerializeField]
    private float reloadTimer;

    [SerializeField]
    private float bulletVelocity = 30f;

    private float bulletLifeTime = 5f;

    private float timer;

    private Camera playerCam;

    PhotonView view;


    private void Start()
    {
        view = GetComponentInParent<PhotonView>();
        playerCam = Camera.main;
        timer = reloadTimer;
    }
    // Update is called once per frame
    void Update()
    {
        if (!view.IsMine)
        {
            return;
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();

                // if (GetComponent<Multishot>() != null && GetComponent<Multishot>().isActiveAndEnabled)
                // {
                //     Multishot();
                // }
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
        Debug.Log(targetPoint + " " + shootingPoint);
        Vector3 direction = targetPoint - shootingPoint.position;

        //Make bullet
        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, shootingPoint.position, shootingPoint.rotation);

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
