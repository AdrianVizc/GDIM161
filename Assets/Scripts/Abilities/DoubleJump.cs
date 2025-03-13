using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DoubleJump : Ability
{ 
    public override void Activate(GameObject parent)
    {
        Movement movement = parent.GetComponent<Movement>();
        Rigidbody rb = parent.GetComponent<Rigidbody>();

        if (movement.canDoubleJump && !movement.grounded)
        {
            movement.canDoubleJump = false;
            rb.AddForce(rb.transform.up * 16f, ForceMode.Impulse);
            if (PlayerPrefs.HasKey("DoubleJump"))
            {
                int DoubleJump = PlayerPrefs.GetInt("DoubleJump");
                DoubleJump++;
                PlayerPrefs.SetInt("DoubleJump", DoubleJump);
            }
            else
            {
                PlayerPrefs.SetInt("DoubleJump", 1);
            }
            PlayerPrefs.Save();
        }

        
    }
    public override void BeginCooldown(GameObject parent)
    {

    }
}
