using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShieldAbility : Ability
{
    public override void Activate(GameObject parent)
    {
        Shield barrier = parent.GetComponentInChildren<Shield>();
        barrier.playerShield.SetActive(true);
        barrier.SetDurability(3);
    }

    public override void BeginCooldown(GameObject parent)
    {
        Shield barrier = parent.GetComponentInChildren<Shield>();
        barrier.playerShield.SetActive(false);
    }
}
