using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySlotButton : MonoBehaviour
{
    private AbilityButtonManager abilityButtonManager;
    private LobbyManager lobbyManager;

    private void Start()
    {
        abilityButtonManager = this.transform.root.GetComponentInChildren<AbilityButtonManager>();
        lobbyManager = GameObject.Find("LobbyManager").GetComponent<LobbyManager>();
    }

    public void GetAbilitySlotPosition()
    {
        abilityButtonManager.ClickedAbilitySlot(true);
        abilityButtonManager.SetAbilitySelectPosition(gameObject.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("In Slot: " + abilityButtonManager.targetPosition);
        //Debug.Log("Collision Position: " + collision.gameObject.transform.position);
        //Debug.Log("Name: " + collision.tag);
        //Debug.Log(this.tag);

        SetAbilitySlot(this.tag, collision.tag);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Left Slot");
        //Debug.Log("Removing: " + abilityButtonManager.GetSlotNum(collision.gameObject).ToString());
        //PlayerPrefs.DeleteKey(this.tag.ToString());
        //PlayerPrefs.SetString(this.tag, null);
        //Debug.Log("DELETING KEY: " + this.tag.ToString());
        if (lobbyManager.playClicked == false)
        {
            PlayerPrefs.DeleteKey(this.tag.ToString());
            //PlayerPrefs.SetString(this.tag, null);
        }
    }

    private void SetAbilitySlot(string slot, string tag)
    {
        PlayerPrefs.SetString(slot.ToString(), tag);
        //Debug.Log("KEY: " + slot);
        //Debug.Log("SAVED: " + PlayerPrefs.GetString(slot.ToString()));
    }
}
