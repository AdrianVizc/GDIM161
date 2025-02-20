using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityToPlayer : MonoBehaviour
{

    private Ability onel;
    private Ability two2;
    private Ability three3;

    private GameObject one;
    private GameObject two;
    private GameObject three;

    // Start is called before the first frame update
    public void SetAbilities(GameObject player)
    {
        GameObject play = player;
        Debug.Log("ABILITY 1: " + PlayerPrefs.GetString("1"));
        //one1 = player.FindWithTag(PlayerPrefs.GetString("1"));
        Debug.Log("ABILITY 2: " + PlayerPrefs.GetString("2"));
        Debug.Log("ABILITY 3: " + PlayerPrefs.GetString("3"));

        three = GameObject.FindWithTag(PlayerPrefs.GetString("3"));
        Debug.Log("Object: " + three);
    }
    

}
