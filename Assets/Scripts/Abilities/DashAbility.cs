using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DashAbility : Ability
{
    [SerializeField]
    private float dashVelocity;
    public override void Activate(GameObject parent)
    {
        Movement movement = parent.GetComponent<Movement>();
        movement.accelSpeed = dashVelocity;
    }

    public override void BeginCooldown(GameObject parent)
    {
        Movement movement = parent.GetComponent<Movement>();
        movement.accelSpeed = movement.normAccel;
    }
        
}
