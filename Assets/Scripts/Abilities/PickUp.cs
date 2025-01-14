using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour //FOR TESTING PURPOSES ONLY
{
    [SerializeField]
    private AbilityAbstract ability;

    // Update is called once per frame
    public void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Picked Up");
        Movement player = collision.GetComponent<Movement>();

        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            Debug.Log("by Player");
            ability.Apply(player);
        }
    }
}
