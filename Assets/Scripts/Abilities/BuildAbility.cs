using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuildAbility : Ability
{
    public override void Activate(GameObject parent)
    {

        Build wallBuild = parent.GetComponentInChildren<Build>();
        wallBuild.PlaceWall();
    }

    public override void BeginCooldown(GameObject parent)
    {

    }
}
