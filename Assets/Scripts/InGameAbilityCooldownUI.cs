using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameAbilityCooldownUI : MonoBehaviour
{
    [SerializeField] private GameObject abilityIcon1;
    [SerializeField] private GameObject abilityIcon2;
    [SerializeField] private GameObject abilityIcon3;

    [Space]

    [Header("Movement")]
    [SerializeField] private Sprite dash;
    [SerializeField] private Sprite doubleJump;
    [SerializeField] private Sprite teleport;
    //[SerializeField] private Sprite ;

    [Space]

    [Header("Offensive")]
    [SerializeField] private Sprite multishot;
    [SerializeField] private Sprite mine;
    [SerializeField] private Sprite overload;
    [SerializeField] private Sprite grenade;

    [Space]

    [Header("Utility")]
    [SerializeField] private Sprite wall;
    [SerializeField] private Sprite trackingBot;
    [SerializeField] private Sprite shield;
    [SerializeField] private Sprite invisibility;

    private void Start()
    {
        SetAbilityIcon(PlayerPrefs.GetString("Slot1"), abilityIcon1);
        SetAbilityIcon(PlayerPrefs.GetString("Slot2"), abilityIcon2);
        SetAbilityIcon(PlayerPrefs.GetString("Slot3"), abilityIcon3);
    }

    private void SetAbilityIcon(string playerPref, GameObject abilityIconGameObject)
    {
        switch (playerPref)
        {
            case "Dash":
                abilityIconGameObject.GetComponent<Image>().sprite = dash; 
                break;
            case "Double Jump":
                abilityIconGameObject.GetComponent<Image>().sprite = doubleJump;
                break;
            case "Teleport":
                abilityIconGameObject.GetComponent<Image>().sprite = teleport;
                break;
            case "Multishot":
                abilityIconGameObject.GetComponent<Image>().sprite = multishot;
                break;
            case "Mine":
                abilityIconGameObject.GetComponent<Image>().sprite = mine;
                break;
            case "Overload":
                abilityIconGameObject.GetComponent<Image>().sprite = overload;
                break;
            case "Grenade":
                abilityIconGameObject.GetComponent<Image>().sprite = grenade;
                break;
            case "Wall":
                abilityIconGameObject.GetComponent<Image>().sprite = wall;
                break;
            case "TrackingBot":
                abilityIconGameObject.GetComponent<Image>().sprite = trackingBot;
                break;
            case "Shield":
                abilityIconGameObject.GetComponent<Image>().sprite = shield;
                break;
            case "Invis":
                abilityIconGameObject.GetComponent<Image>().sprite = invisibility;
                break;
            default:
                Destroy(abilityIconGameObject); 
                break;
        }
    }
}
