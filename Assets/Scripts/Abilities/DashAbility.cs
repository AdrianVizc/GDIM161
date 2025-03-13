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

        if (PlayerPrefs.HasKey("Dash"))
        {
            int Dash = PlayerPrefs.GetInt("Dash");
            Dash++;
            PlayerPrefs.SetInt("Dash", Dash);
        }
        else
        {
            PlayerPrefs.SetInt("Dash", 1);
        }
        PlayerPrefs.Save();
    }

    public override void BeginCooldown(GameObject parent)
    {
        Movement movement = parent.GetComponent<Movement>();
        movement.accelSpeed = movement.normAccel;
    }
        
}
