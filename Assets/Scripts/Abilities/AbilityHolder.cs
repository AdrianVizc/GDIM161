using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability;
    private float cdTime;
    private float activeTime;

    private float delay = 2f;
    private bool pressedFirst = false;
    private float lastPressedTime;

    enum AbilityState
    {
        ready,
        active,
        cooldown
    }
    AbilityState state = AbilityState.ready;

    public KeyCode key;

    // Update is called once per frame
    void Update()
    {
        if (InGameUI.globalInputLock)
        {
            return;
        }
        switch (state)
        {
            case AbilityState.ready:
                if (Input.GetKeyDown(key) && ability.name != "Double Jump")
                {
                    ability.Activate(gameObject);
                    state = AbilityState.active;
                    activeTime = ability.activeTime;
                }
                if (Input.GetKeyDown(key) && ability.name == "Double Jump")
                {
                    if (pressedFirst)
                    {
                        bool pressedTwice = Time.time - lastPressedTime <= delay;

                        if (pressedTwice)
                        {
                            ability.Activate(gameObject);
                            state = AbilityState.active;
                            activeTime = ability.activeTime;
                            pressedFirst = false;
                        }
                    }
                    else
                    {
                        pressedFirst = true;
                    }

                    lastPressedTime = Time.time;
                    
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
