using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null && !collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
        //if (collision.gameObject.CompareTag("Monkey"))
        //{
        //    print("Hit Monkey");
        //    //do dmg
        //    Destroy(gameObject);
        //}
    }
}
