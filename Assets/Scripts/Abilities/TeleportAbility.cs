using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TeleportAbility : Ability
{
    [SerializeField]
    private float teleportDistance;
    public override void Activate(GameObject parent)
    {
        Movement move = parent.GetComponent<Movement>();
        move.Teleport(teleportDistance);
    }
}
