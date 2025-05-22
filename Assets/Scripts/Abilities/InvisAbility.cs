using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InvisAbility : Ability
{
    public override void Activate(GameObject parent)
    {
        Invis doInvis = parent.GetComponentInChildren<Invis>();
        doInvis.Invisible();
    }

    public override void BeginCooldown(GameObject parent)
    {
        Invis doInvis = parent.GetComponentInChildren<Invis>();
        doInvis.StopInvisible();
    }
}
