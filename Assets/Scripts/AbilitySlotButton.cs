using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySlotButton : MonoBehaviour
{
    private AbilityButtonManager abilityButtonManager;

    private void Start()
    {
        abilityButtonManager = this.transform.root.GetComponentInChildren<AbilityButtonManager>();
    }

    public void GetAbilitySlotPosition()
    {
        abilityButtonManager.ClickedAbilitySlot(true);
        abilityButtonManager.SetAbilitySelectPosition(gameObject.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("In Slot: " + abilityButtonManager.targetPosition);
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Left Slot");
    }
}
