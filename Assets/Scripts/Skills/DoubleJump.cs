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

    public override void Apply(PlayerStatus player)
    {
        if (Input.GetKey(KeyCode.Space) && cdTimer != 0 && person.getGrounded() == false)
        {

        }
    }
}
