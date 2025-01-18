using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITestShooting : MonoBehaviour
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

    private float timer;

    private void Start()
    {
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
            Shoot();
            timer = reloadTimer;
        }
    }

    private void Shoot()
    {
        //Make bullet
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);

        //Rotating bullet to shoot direction
        //bullet.transform.forward = direction.normalized;

        //Give bullet force/movement to "shoot"
        bullet.GetComponent<Rigidbody>().AddForce(Vector3.forward * bulletVelocity, ForceMode.Impulse);

        //Despawn
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletLifeTime));
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(bullet);
    }

}
