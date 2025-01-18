using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    public GameObject playerShield;

    [SerializeField]
    private int durability;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            durability--;
            if (durability <= 0)
            {
                playerShield.SetActive(false);
            }
        }
    }

    public void SetDurability(int health)
    {
        durability = health;
    }
}
