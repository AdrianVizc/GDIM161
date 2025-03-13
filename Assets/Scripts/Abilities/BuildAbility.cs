using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuildAbility : Ability
{
    public override void Activate(GameObject parent)
    {
        //Build wallBuild = parent.GetComponentInChildren<Build>();
        Build wallBuild = GameObject.Find("WallBuilder").GetComponent<Build>();
        wallBuild.Place();

        if (PlayerPrefs.HasKey("Wall"))
        {
            int Wall = PlayerPrefs.GetInt("Wall");
            Wall++;
            PlayerPrefs.SetInt("Wall", Wall);
        }
        else
        {
            PlayerPrefs.SetInt("Wall", 1);
        }
        PlayerPrefs.Save();
    }
    

    public override void BeginCooldown(GameObject parent)
    {

    }
}
