using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DoubleJump : Ability
{
    public override void Activate(GameObject parent)
    {
        Movement movement = parent.GetComponent<Movement>();
        movement.Jump();
        //movement.canDoubleJump = true;
        //movement.DoubleJump();
    }
    public override void BeginCooldown(GameObject parent)
    {
        Rigidbody rb = parent.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
    }
}
