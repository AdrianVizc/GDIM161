using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : AbilityAbstract
{
    [SerializeField]
    private float cooldown = 10f;

    [SerializeField]
    private Movement person;

    private float cdTimer;

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Apply(Movement player)
    {
        person = player;
        cdTimer = Time.time;

        if (cdTimer != 0 && player.getGrounded() == false)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                person.Jump();
            }
        }
    }
}
