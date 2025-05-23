using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GrenadeAbility : Ability
{
    public override void Activate(GameObject parent)
    {
        Throwing player = parent.GetComponentInChildren<Throwing>();
        player.ThrowGrenade();
    }

    public override void BeginCooldown(GameObject parent)
    {
        
    }
}
