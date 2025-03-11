using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShieldAbility : Ability
{
    public override void Activate(GameObject parent)
    {
        Movement player = parent.GetComponent<Movement>();

        player.playerShield.SetActive(true);

        Shield barrier = parent.GetComponentInChildren<Shield>();
        //barrier.playerShield.SetActive(true);
        //barrier.Show();
        barrier.SetDurability(3);
        Debug.Log("Health: " + 3);
    }

    public override void BeginCooldown(GameObject parent)
    {
        Movement player = parent.GetComponent<Movement>();
        player.playerShield.SetActive(false);

        //Shield barrier = parent.GetComponentInChildren<Shield>();
        //barrier.playerShield.SetActive(false);
        //barrier.Hide();
    }
}
