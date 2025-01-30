using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySlotButton : MonoBehaviour
{
    AbilityButtonManager abilityButtonManager = new AbilityButtonManager();

    public void GetAbilitySlotPosition()
    {
        abilityButtonManager.SetAbilitySelectPosition(gameObject.transform.position);
    }
}
