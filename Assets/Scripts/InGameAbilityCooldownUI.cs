using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameAbilityCooldownUI : MonoBehaviour
{
    [SerializeField] private GameObject abilityIcon1;
    [SerializeField] private GameObject abilityIcon2;
    [SerializeField] private GameObject abilityIcon3;
    [SerializeField] private GameObject cooldownGray1;
    [SerializeField] private GameObject cooldownGray2;
    [SerializeField] private GameObject cooldownGray3;
    [SerializeField] private TMP_Text timerNumber1;
    [SerializeField] private TMP_Text timerNumber2;
    [SerializeField] private TMP_Text timerNumber3;

    [Space]

    [Header("Movement")]
    [SerializeField] private Sprite dash;
    [SerializeField] private Sprite doubleJump;
    [SerializeField] private Sprite teleport;
    [SerializeField] private Ability dashAbility;
    [SerializeField] private Ability doubleJumpAbility;
    [SerializeField] private Ability teleportAbility;

    [Space]

    [Header("Offensive")]
    [SerializeField] private Sprite multishot;
    [SerializeField] private Sprite mine;
    [SerializeField] private Sprite overload;
    [SerializeField] private Ability multishotAbility;
    [SerializeField] private Ability mineAbility;
    [SerializeField] private Ability overloadAbility;

    [Space]

    [Header("Utility")]
    [SerializeField] private Sprite wall;
    [SerializeField] private Sprite trackingBot;
    [SerializeField] private Sprite shield;
    [SerializeField] private Sprite invisibility;
    [SerializeField] private Ability wallAbility;
    [SerializeField] private Ability trackingBotAbility;
    [SerializeField] private Ability shieldAbility;
    [SerializeField] private Ability invisibilityAbility;

    private void Start()
    {
        SetAbilityIcon(PlayerPrefs.GetString("Slot1"), abilityIcon1);
        SetAbilityIcon(PlayerPrefs.GetString("Slot2"), abilityIcon2);
        SetAbilityIcon(PlayerPrefs.GetString("Slot3"), abilityIcon3);

        cooldownGray1.SetActive(false);
        cooldownGray2.SetActive(false);
        cooldownGray3.SetActive(false);
    }

    private void Update()
    {
        CooldownHandler(PlayerPrefs.GetString("Slot1"), cooldownGray1, timerNumber1);
        CooldownHandler(PlayerPrefs.GetString("Slot2"), cooldownGray2, timerNumber2);
        CooldownHandler(PlayerPrefs.GetString("Slot3"), cooldownGray3, timerNumber3);
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
            /*case "Grenade":
                abilityIconGameObject.GetComponent<Image>().sprite = grenade;
                break;*/
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

    private void CooldownHandler(string playerPref, GameObject cooldownObject, TMP_Text timer)
    {
        switch (playerPref)
        {
            case "Dash":
                if (dashAbility.cooldownTime != 0)
                {
                    cooldownObject.SetActive(true);
                    timer.text = dashAbility.cooldownTime.ToString();
                }
                else
                {
                    cooldownObject.SetActive(false);
                }
                break;
            case "Double Jump":

                break;
            case "Teleport":

                break;
            case "Multishot":

                break;
            case "Mine":

                break;
            case "Overload":

                break;
            /*case "Grenade":

                break;*/
            case "Wall":

                break;
            case "TrackingBot":

                break;
            case "Shield":

                break;
            case "Invis":

                break;
            default:

                break;
        }
    }   
}
