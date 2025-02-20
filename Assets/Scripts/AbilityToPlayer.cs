using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityToPlayer : MonoBehaviour
{
    private GameObject playerRef;
    private GameObject wallRef;
    private GameObject mineRef;
    private GameObject multishotRef;
    private GameObject trackerRef;
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

    // Start is called before the first frame update
    private void Start()
    {
        playerRef = gameObject.transform.root.gameObject;

        wallRef = GameObject.Find("WallBuilder");
        wallRef.SetActive(false);

        mineRef = GameObject.Find("MinePlacer");
        mineRef.SetActive(false);

        multishotRef = GameObject.Find("MultishotHolder");
        multishotRef.SetActive(false);

        trackerRef = GameObject.Find("Tracker");
        trackerRef.SetActive(false);

        abilityHolderList = transform.root.GetComponentsInChildren<AbilityHolder>();

        foreach (AbilityHolder ability in abilityHolderList)
        {
            //Debug.Log(ability.name);
            ability.enabled = false;
        }
        //Debug.Log(playerRef);
    }

    public void SetAbilities(GameObject player)
    {
        Debug.Log("ABILITY 1: " + PlayerPrefs.GetString("1"));
        Debug.Log("ABILITY 2: " + PlayerPrefs.GetString("2"));
        Debug.Log("ABILITY 3: " + PlayerPrefs.GetString("3"));
        Debug.Log("OWNER: " + player.name);
        
        //if (PlayerPrefs.GetString("1") == wallRef.tag || PlayerPrefs.GetString("2") == wallRef.tag || PlayerPrefs.GetString("3") == wallRef.tag)
        //{
        //    Debug.Log("WALL CHOSEN AND FOUND");
        //}

        //three = GameObject.FindWithTag(PlayerPrefs.GetString("3"));
        //three.SetActive(true);
        //Debug.Log("Object: " + three.name);
    }
    

}
