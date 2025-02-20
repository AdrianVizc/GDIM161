using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityToPlayer : MonoBehaviour
{
    [SerializeField] private GameObject playerRef;
    [SerializeField] private GameObject wallRef;
    [SerializeField] private GameObject mineRef;
    [SerializeField] private GameObject multishotRef;
    [SerializeField] private GameObject trackerRef;
    //private GameObject playerRef;
    private AbilityHolder[] abilityHolderList;
    private enum Abilities
    {
        Wall,
        Mine,
        Multishot,
        Tracker,
    }

    private Abilities abilityName;

    private Ability onel;
    private Ability two2;
    private Ability three3;

    private GameObject one;
    private GameObject two;
    private GameObject three;

    private void Awake()
    {
        //playerRef = gameObject.transform.root.gameObject;

        //wallRef = GameObject.Find("WallBuilder");
        //Debug.Log("Wall Ref Tag: " + wallRef.tag);
        //wallRef.SetActive(false);
        //Debug.Log("WALL FALSE");

        //mineRef = GameObject.Find("MinePlacer");
        //Debug.Log("Mine Ref Tag: " + mineRef.tag);
        //mineRef.SetActive(false);
        //Debug.Log("MINE FALSE");

        // multishotRef = GameObject.Find("MultishotHolder");
        //Debug.Log("MS Ref Tag: " + multishotRef.tag);
        //multishotRef.SetActive(false);
        //Debug.Log("MULTISHOT FALSE");

        //trackerRef = GameObject.Find("Tracker");
        //Debug.Log("Tracker Tag: " + trackerRef.tag);
        //trackerRef.SetActive(false);
        //Debug.Log("TRACKER FALSE");

        //abilityHolderList = transform.root.GetComponentsInChildren<AbilityHolder>();

        //foreach (AbilityHolder ability in abilityHolderList)
        //{
        //    Debug.Log(ability.GetType().GetField("ability").ToString());
        //    ability.enabled = false;
        //}
        //Debug.Log(playerRef);
        //Debug.Log("DISABLED ALL");

        abilityHolderList = transform.root.GetComponentsInChildren<AbilityHolder>();

        foreach (AbilityHolder ability in abilityHolderList)
        {
            //Debug.Log("ABILITY: " + ability.GetType().GetField("ability").GetValue(ability).ToString());
            //Debug.Log("LOOKING FOR: " + PlayerPrefs.GetString("Slot1"));
            //Debug.Log("LOOKING FOR: " + PlayerPrefs.GetString("Slot2"));
            //Debug.Log("LOOKING FOR: " + PlayerPrefs.GetString("Slot3"));
            //Debug.Log("FOREACH: " + (ability.GetType().GetField("ability").GetValue(ability).ToString().Contains(PlayerPrefs.GetString("Slot1"))));

            if ((ability.GetType().GetField("ability").GetValue(ability).ToString().Contains(PlayerPrefs.GetString("Slot1"))) ||
                (ability.GetType().GetField("ability").GetValue(ability).ToString().Contains(PlayerPrefs.GetString("Slot2"))) ||
                (ability.GetType().GetField("ability").GetValue(ability).ToString().Contains(PlayerPrefs.GetString("Slot3"))))
            {
                Debug.Log("FOUND: " + ability.GetType().GetField("ability").GetValue(ability).ToString());
            }
            else
            {
                Debug.Log("TURNING OFF: " + ability.GetType().GetField("ability").GetValue(ability).ToString());
                ability.enabled = false;
            }
            //Debug.Log(ability.GetType().GetField("ability").ToString());
            //ability.enabled = false;
        }
    }

    public void SetAbilities(GameObject player)
    {
        //Initializing();

        Debug.Log("ABILITY 1: " + PlayerPrefs.GetString("Slot1"));
        Debug.Log("ABILITY 2: " + PlayerPrefs.GetString("Slot2"));
        Debug.Log("ABILITY 3: " + PlayerPrefs.GetString("Slot3"));
        Debug.Log("OWNER: " + player.name);

        if (player.name == playerRef.name)
        {
            Debug.Log("Same Player");
        }
        else
        {
            Debug.Log("Different");
        }
        //Debug.Log("Wall Ref Tag: " + wallRef.tag);
        //Debug.Log(PlayerPrefs.GetString("1"));

        //if (PlayerPrefs.GetString("Slot1") == "Wall" || PlayerPrefs.GetString("Slot2") == "Wall" || PlayerPrefs.GetString("Slot3") == "Wall")
        //{
        //    if (wallRef != null)
        //    {
        //        Debug.Log(wallRef.name);
        //        wallRef.SetActive(true);
        //    }
        //    wallRef.SetActive(true);
        //    Debug.Log("WALL CHOSEN FOUND ENABLED");
        //}
        //else
        //{
        //    Debug.Log("NOT FOUND");
        //}

        //three = GameObject.FindWithTag(PlayerPrefs.GetString("3"));
        //three.SetActive(true);
        //Debug.Log("Object: " + three.name);
        //wallRef.SetActive(true);
    }
    

}
