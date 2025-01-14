using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour //FOR TESTING PURPOSES ONLY
{
    AbilityAbstract ability;

    public void Initializing(AbilityAbstract skill)
    {
        ability = skill;
    }
    // Update is called once per frame
    public void OnTriggerEnter(Collider collision)
    {
        Movement player = collision.GetComponent<Movement>();

        if (player != null)
        {
            ability.Apply(player);
            Destroy(gameObject);
        }
    }
}
