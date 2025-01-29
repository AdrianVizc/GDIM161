using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MultishotAbility : Ability
{
    /* i kinda wrote all the functionalities in the Shooting.cs script hehe
     * but this script still needed as a placeholder on the player hehe
     * - dom
     */
    public override void Activate(GameObject parent)
    {
        Multishot ms = parent.GetComponentInChildren<Multishot>();
        ms.DoMultishot();
    }

    public override void BeginCooldown(GameObject parent)
    {

    }
}
