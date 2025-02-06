using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TrackingBotAbility : Ability
{
    public override void Activate(GameObject parent)
    {
        Build placeBot = GameObject.Find("Tracker").GetComponent<Build>();
        placeBot.Place();
    }

    public override void BeginCooldown(GameObject parent)
    {
        
    }
}
