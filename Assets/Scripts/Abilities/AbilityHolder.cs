using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability;
    private float cdTime;
    private float activeTime;

    PhotonView photonView;

    enum AbilityState
    {
        ready,
        active,
        cooldown
    }
    AbilityState state = AbilityState.ready;

    public KeyCode key;

    private void Start()
    {
        photonView = GetComponentInParent<PhotonView>();

        if (!photonView.IsMine)
        {
            this.enabled = false;
        }
    }

    void Update()
    {
        if (InGameUI.globalInputLock)
        {
            return;
        }
        switch (state)
        {
            case AbilityState.ready:
                if (Input.GetKeyDown(key))
                {
                    ability.Activate(gameObject);
                    state = AbilityState.active;
                    activeTime = ability.activeTime;
                }
                break;
             case AbilityState.active:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    ability.BeginCooldown(gameObject);
                    state = AbilityState.cooldown;
                    cdTime = ability.cooldownTime;
                }
                break;
            case AbilityState.cooldown:
                if (cdTime > 0)
                {
                    cdTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.ready;
                }
                break;
        }
    }
}
