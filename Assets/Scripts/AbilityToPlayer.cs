using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityToPlayer : MonoBehaviour
{
    private AbilityHolder[] abilityHolderList;

    private void Awake()
    {
        abilityHolderList = this.transform.root.GetComponentsInChildren<AbilityHolder>();

        foreach (AbilityHolder ability in abilityHolderList)
        {
            //Debug.Log("ABILITY: " + ability.GetType().GetField("ability").GetValue(ability).ToString());
            //Debug.Log("LOOKING FOR: " + PlayerPrefs.GetString("Slot1"));
            //Debug.Log("LOOKING FOR: " + PlayerPrefs.GetString("Slot2"));
            //Debug.Log("LOOKING FOR: " + PlayerPrefs.GetString("Slot3"));
            //Debug.Log("FOREACH: " + (ability.GetType().GetField("ability").GetValue(ability).ToString().Contains(PlayerPrefs.GetString("Slot1"))));

            if ((ability.GetType().GetField("ability").GetValue(ability).ToString().Contains("Double Jump"))) //Special Case for Double Jump
            {
                if (ability.GetType().GetField("ability").GetValue(ability).ToString().Contains(PlayerPrefs.GetString("Slot1")) && PlayerPrefs.HasKey("Slot1") ||
                    ability.GetType().GetField("ability").GetValue(ability).ToString().Contains(PlayerPrefs.GetString("Slot2")) && PlayerPrefs.HasKey("Slot2") ||
                    ability.GetType().GetField("ability").GetValue(ability).ToString().Contains(PlayerPrefs.GetString("Slot3")) && PlayerPrefs.HasKey("Slot3") )
                {
                    //Debug.Log("PICKED DOUBLE JUMP, RESETTING KEY " + ability.GetType().GetField("ability").GetValue(ability).ToString());
                    ability.key = KeyCode.Space;
                }
                else
                {
                    ability.enabled = false;
                }
            }
            else if (ability.GetType().GetField("ability").GetValue(ability).ToString().Contains(PlayerPrefs.GetString("Slot1")) && PlayerPrefs.HasKey("Slot1")) //Slot 1
            {
                //Debug.Log("PLAYER PREFS SLOT 1: " + PlayerPrefs.GetString("Slot1"));
                //Debug.Log("FOUND IN SLOT 1: " + ability.GetType().GetField("ability").GetValue(ability).ToString());
                ability.key = KeyCode.Q;
            }
            else if (ability.GetType().GetField("ability").GetValue(ability).ToString().Contains(PlayerPrefs.GetString("Slot2")) && PlayerPrefs.HasKey("Slot2")) //Slot 2
            {
                //Debug.Log("PLAYER PREFS SLOT 2: " + PlayerPrefs.GetString("Slot2"));
                //Debug.Log("FOUND IN SLOT 2: " + ability.GetType().GetField("ability").GetValue(ability).ToString());
                ability.key = KeyCode.E;
            }
            else if (ability.GetType().GetField("ability").GetValue(ability).ToString().Contains(PlayerPrefs.GetString("Slot3")) && PlayerPrefs.HasKey("Slot3")) //Slot 3
            {
                //Debug.Log("PLAYER PREFS SLOT 3: " + PlayerPrefs.GetString("Slot3"));
                //Debug.Log("FOUND IN SLOT 3: " + ability.GetType().GetField("ability").GetValue(ability).ToString());
                ability.key = KeyCode.R;
            }
            else //Disable the rest
            {
                //Debug.Log("TURNING OFF: " + ability.GetType().GetField("ability").GetValue(ability).ToString());
                ability.enabled = false;
            }
            //Debug.Log(ability.GetType().GetField("ability").ToString());
            //ability.enabled = false;
        }
    }

    public void SetAbilities(GameObject player) //this honestly does nothing LOL
    {
        //Debug.Log("ABILITY 1: " + PlayerPrefs.GetString("Slot1"));
        //Debug.Log("ABILITY 2: " + PlayerPrefs.GetString("Slot2"));
        //Debug.Log("ABILITY 3: " + PlayerPrefs.GetString("Slot3"));
        //Debug.Log("OWNER: " + player.name);

        if (PlayerPrefs.HasKey("Slot1"))
        {
            PlayerPrefs.DeleteKey("Slot1");
        }
        if (PlayerPrefs.HasKey("Slot2"))
        {
            PlayerPrefs.DeleteKey("Slot2");
        }
        if (PlayerPrefs.HasKey("Slot3"))
        {
            PlayerPrefs.DeleteKey("Slot3");
        }
        if (PlayerPrefs.HasKey("Ready"))
        {
            PlayerPrefs.DeleteKey("Ready");
        }
    }
}
