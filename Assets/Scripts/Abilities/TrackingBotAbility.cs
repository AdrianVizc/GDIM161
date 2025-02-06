using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TrackingBotAbility : Ability
{
    public override void Activate(GameObject parent)
    {
        Spawner spawnBot = GameObject.Find("Tracker").GetComponent<Spawner>();
        spawnBot.Spawn();
    }

    public override void BeginCooldown(GameObject parent)
    {
        
    }
}
