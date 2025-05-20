using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class OverloadAbility : Ability
{
    [SerializeField]
    private float buffMultiplier;

    public override void Activate(GameObject parent)
    {
        Shooting shooter = parent.GetComponent<Shooting>();
        shooter.ReduceShootRate(buffMultiplier);
    }

    public override void BeginCooldown(GameObject parent)
    {
        Shooting shooter = parent.GetComponent<Shooting>();
        shooter.ResetBulletSpeed(buffMultiplier);
    }
}
