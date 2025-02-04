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
    }

    public override void BeginCooldown(GameObject parent)
    {

    }
}
