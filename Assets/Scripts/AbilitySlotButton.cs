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
        //Debug.Log("In Slot: " + abilityButtonManager.targetPosition);
        //Debug.Log("Name: " + collision.tag);

        SetAbilitySlot(abilityButtonManager.GetSlotNum(collision.gameObject), collision.tag);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Left Slot");
        //Debug.Log("Removing: " + abilityButtonManager.GetSlotNum(collision.gameObject).ToString());
        //PlayerPrefs.DeleteKey(abilityButtonManager.GetSlotNum(collision.gameObject).ToString());
    }

    private void SetAbilitySlot(int slot, string tag)
    {
        PlayerPrefs.SetString(slot.ToString(), tag);
       //Debug.Log(PlayerPrefs.GetString(slot.ToString()));
    }
}
