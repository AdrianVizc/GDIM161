using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameAbilityCooldownUI : MonoBehaviour
{
    [SerializeField] private AbilityHolder abilityHolderRef;
    [SerializeField] private GameObject player;

    [Space]

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

    [Space]

    [Header("Offensive")]
    [SerializeField] private Sprite multishot;
    [SerializeField] private Sprite mine;
    [SerializeField] private Sprite overload;

    [Space]

    [Header("Utility")]
    [SerializeField] private Sprite wall;
    [SerializeField] private Sprite trackingBot;
    [SerializeField] private Sprite shield;
    [SerializeField] private Sprite invisibility;

    List<AbilityHolder> abilityHolderList;
    AbilityHolder abilityHolder1;
    AbilityHolder abilityHolder2;
    AbilityHolder abilityHolder3;

    PhotonView photonView;

    private void Start()
    {
        photonView = player.GetComponent<PhotonView>();

        if (!photonView.IsMine)
        {
            Destroy(gameObject);
        }
        SetAbilityIcon(PlayerPrefs.GetString("Slot1"), abilityIcon1);
        SetAbilityIcon(PlayerPrefs.GetString("Slot2"), abilityIcon2);
        SetAbilityIcon(PlayerPrefs.GetString("Slot3"), abilityIcon3);

        cooldownGray1.SetActive(false);
        cooldownGray2.SetActive(false);
        cooldownGray3.SetActive(false);

        abilityHolderList = abilityHolderRef.GetComponents<AbilityHolder>().ToList();
        foreach (var abilityHolder in abilityHolderList)
        {
            if (abilityHolder.enabled)
            {
                if (abilityHolder1 == null)
                {
                    abilityHolder1 = abilityHolder;
                }
                else if (abilityHolder2 == null)
                {
                    abilityHolder2 = abilityHolder;
                }
                else if (abilityHolder3 == null)
                {
                    abilityHolder3 = abilityHolder;
                }
            }
        }
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

    private void CheckAbilityHolder(string playerPref, GameObject cooldownObject, TMP_Text timer)
    {
        if (playerPref == abilityHolder1.ability.name)
        {
            if (abilityHolder1.GetCurrentCD() > 0)
            {
                cooldownObject.SetActive(true);
                timer.text = ((int)abilityHolder1.GetCurrentCD()).ToString();
            }
            else
            {
                cooldownObject.SetActive(false);
            }
        }
        else if (playerPref == abilityHolder2.ability.name)
        {
            if (abilityHolder2.GetCurrentCD() > 0)
            {
                cooldownObject.SetActive(true);
                timer.text = ((int)abilityHolder2.GetCurrentCD()).ToString();
            }
            else
            {
                cooldownObject.SetActive(false);
            }
        }
        else if (playerPref == abilityHolder3.ability.name)
        {
            if (abilityHolder3.GetCurrentCD() > 0)
            {
                cooldownObject.SetActive(true);
                timer.text = ((int)abilityHolder3.GetCurrentCD()).ToString();
            }
            else
            {
                cooldownObject.SetActive(false);
            }
        }
    }

    private void CooldownHandler(string playerPref, GameObject cooldownObject, TMP_Text timer)
    {
        switch (playerPref)
        {
            case "Dash":

                CheckAbilityHolder(playerPref, cooldownObject, timer);
                break;

            case "Double Jump":

                CheckAbilityHolder(playerPref, cooldownObject, timer);
                break;

            case "Teleport":

                CheckAbilityHolder(playerPref, cooldownObject, timer);
                break;

            case "Multishot":

                CheckAbilityHolder(playerPref, cooldownObject, timer);
                break;

            case "Mine":

                CheckAbilityHolder(playerPref, cooldownObject, timer);
                break;

            case "Overload":

                CheckAbilityHolder(playerPref, cooldownObject, timer);
                break;

            /*case "Grenade":

                break;*/
            case "Wall":

                CheckAbilityHolder(playerPref, cooldownObject, timer);
                break;

            case "TrackingBot":

                CheckAbilityHolder(playerPref, cooldownObject, timer);
                break;

            case "Shield":

                CheckAbilityHolder(playerPref, cooldownObject, timer);
                break;

            case "Invis":

                CheckAbilityHolder(playerPref, cooldownObject, timer);
                break;

            default:

                CheckAbilityHolder(playerPref, cooldownObject, timer);
                break;

        }
    }   
}
