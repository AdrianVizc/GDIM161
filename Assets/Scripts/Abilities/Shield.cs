using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shield : MonoBehaviour
{
    [SerializeField]
    public GameObject playerShield;

    [SerializeField]
    private int durability;

    [SerializeField]
    private Ability ability;

    private GameObject owner;

    private void Start()
    {
        owner = this.transform.root.gameObject;
        Debug.Log("OWNER: " + owner);
        StartCoroutine(DestroyOnCD(ability.activeTime));
    }
    private void Update()
    {
        this.transform.parent = owner.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            durability--;
            Debug.Log("Health: " + durability);
            if (durability <= 0)
            {
                //playerShield.SetActive(false);
                Destroy(gameObject);
            }
        }
    }

    public void SetDurability(int health)
    {
        durability = health;
    }

    public int GetDurability()
    {
        return durability;
    }

    private IEnumerator DestroyOnCD(float time)
    {
        yield return new WaitForSeconds(time);

        if (this.gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
