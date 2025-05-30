using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TrackingBotAbility : Ability
{
    public override void Activate(GameObject parent)
    {
        ObjectSpawner spawnBot = GameObject.Find("Tracker").GetComponent<ObjectSpawner>();
        spawnBot.Spawn();

        if (PlayerPrefs.HasKey("TrackingBot"))
        {
            int TrackingBot = PlayerPrefs.GetInt("TrackingBot");
            TrackingBot++;
            PlayerPrefs.SetInt("TrackingBot", TrackingBot);
        }
        else
        {
            PlayerPrefs.SetInt("TrackingBot", 1);
        }
        PlayerPrefs.Save();
    }

    public override void BeginCooldown(GameObject parent)
    {
        
    }
}
